using Acb.Plugin.PrivilegeManage.Models.Entities;
using Acb.MiddleWare.Data.DB;
using Dynamic.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User;
using AutoMapper;
using Acb.Plugin.PrivilegeManage.Common;
using Dynamic.Core.Extensions;
using Dynamic.Core;
using Acb.Plugin.PrivilegeManage.Models.View.User;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization;
using Acb.Plugin.PrivilegeManage.Constract.Models.Enum;

namespace Acb.Plugin.PrivilegeManage.Models.Repository
{
    /// <summary>
    /// 用户数据库操作
    /// </summary>
    public class RepositoryUser : DBBase<TUser>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dBConfig"></param>
        /// <returns></returns>
        public RepositoryUser(DBCfgViewModel dBConfig) : base(dBConfig) { }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        public int Insert(TUser user)
        {
            return this.DapperRepository.Insert(user, excepts:new[] { nameof(TUser.CreateTime)});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Update<T>(T t)
        {
            return this.DapperRepository.Update(t);
        }
        /// <summary>
        /// 根据用户信息查询用户
        /// </summary>
        /// <param name="userQueryDto">用户信息</param>
        /// <param name="page">页码</param>
        /// <param name="size">每页个数</param>
        /// <returns></returns>
       
        public PagedList<UserInfoDto> QueryInfo(UserQueryDto userQueryDto, int page, int size)
        {
            var type = typeof(TUser);
            var typeR = typeof(TRelationUserOrganization);
            var typeO = typeof(TOrganization);
            int offset = (page - 1) * size;
            UserQueryNewDto userQueryNewDto = userQueryDto.MapTo<UserQueryNewDto>();
            string where = StringManage.GenerateSqlFromEntity(userQueryDto);
            if (where.Contains("[Id]"))
                where = where.Replace("[Id]", "t1.[Id]");
            if (where.Contains("[State]"))
                where = where.Replace("[State]", "t1.[State]");
            if (where.Contains("[Name]"))
                where = where.Replace("[Name]", "t1.[Name]");
            string sqlCount = $@"select count(distinct t1.[Id]) from {type.PropName()} t1 left join {typeR.PropName()} t2 on t1.[Id]=t2.[UserId] 
                                left join {typeO.PropName()} t3 on t2.[OrganizationIdO]=t3.[Id]
                                {where}";
            string sqlQuery = $@"select t.*,
                                t3.[Name] as [OrganizationName],
                                t4.[Name] as [DepartmentName],
                                t5.[Name] as [PositionName]
                                from 
                                (select tt1.*,tt2.[OrganizationId],tt2.[DepartmentId],tt2.[PositionId],tt2.[OrganizationCode],tt2.[DepartmentCode],tt2.[PositionCode],tt2.[UserType] from 
                                (select t1.* from {type.PropName()}
                                t1 left join {typeR.PropName()}  
                                t2 on t1.[Id]=t2.[UserId] left join {typeO.PropName()} t3 on t2.[OrganizationIdO]=t3.[Id] {where} 
                                group by t1.[Id])tt1 
                                left join {typeR.PropName()}  
                                tt2 on tt1.[Id]=tt2.[UserId]   
                                
                                )t
                                left join {typeO.PropName()} t3 on t.[OrganizationId]=t3.[Id] 
                                left join {typeO.PropName()} t4 on t.[DepartmentId]=t4.[Id] 
                                left join {typeO.PropName()} t5 on t.[PositionId]=t5.[Id] 
                                limit {size} offset {offset}";
            int count = this.DapperRepository.QueryFirstOrDefault<int>(sqlCount, userQueryNewDto);
            var r = this.DapperRepository.QueryOriCommand<UserInfoView>(sqlQuery, true, userQueryNewDto).ToList();
            var data = StringManage.UserViewToUserDto(r.ToList());
            return new PagedList<UserInfoDto> { Index = page, Size = size, DataList = data, Total = count };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public void VerifyUser(string userId, int userType)
        {
            var type = typeof(TOrganization);
            var typeR = typeof(TRelationUserOrganization);
            string sql =
                $@"select count(*) from (select [OrganizationIdO] from {typeR.PropName()} where [UserId]=@userId) t1
                                left join {type.PropName()} t2 on t1.[OrganizationIdO]=t2.[Id] where [IsHasChildOrganization]=";
            if (userType == (int) UserTypeEnum.Factory)
            {
                sql += "true";
                int count = this.DapperRepository.QueryFirstOrDefault<int>(sql, new { userId });
                if (count == 0)
                    throw new Exception("该账号非厂商端用户！请从经销商端登录！");
            }
            else
            {
                sql += "false";
                int count = this.DapperRepository.QueryFirstOrDefault<int>(sql, new { userId });
                if (count == 0)
                    throw new Exception("该账号非经销商端用户！请从厂商端登录！");
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="Key"></param>
        /// <param name="SystemCode"></param>
        /// <param name="AppOrganizationId"></param>
        /// <returns></returns>
        public PagedList<UserInfoDto> GetAppUsers(int Page, int Size, string Key, string SystemCode, string AppOrganizationId) {
            var type = typeof(TUser);
            var typeR = typeof(TRelationUserOrganization);
            var typeO = typeof(TOrganization);
            int Offset = (Page - 1) * Size;
            var SqlCount = $@"select count(*) from (select [UserId] from {typeR.PropName()} where [OrganizationId]=@AppOrganizationId) t1 left join {type.PropName()} t2 
                            on t1.[UserId]=t2.[Id] where [Telephone] like @Key or [Name] like @Key";

            string SqlQuery = $@"select t.*,
                                t3.[Name] as [OrganizationName],
                                t4.[Name] as [DepartmentName],
                                t5.[Name] as [PositionName]
                                from 
                                (select t2.*,t1.[OrganizationId],t1.[DepartmentId],t1.[PositionId],t1.[OrganizationCode],t1.[DepartmentCode],t1.[PositionCode],t1.[UserType] from 
                                (select [UserId],[OrganizationId],[PositionId],[DepartmentId],[OrganizationCode],[DepartmentCode],[PositionCode],[UserType]  
                                    from {typeR.PropName()} where [OrganizationId]=@AppOrganizationId) 
                                t1 left join {type.PropName()} 
                                t2 on t1.[UserId]=t2.[Id] where [Telephone] like @Key or [Name] like @Key) t left join {typeO.PropName()} t3 on t.[OrganizationId]=t3.[Id] 
                                left join {typeO.PropName()} t4 on t.[DepartmentId]=t4.[Id] 
                                left join {typeO.PropName()} t5 on t.[PositionId]=t5.[Id] 
                                order by t.[Id] 
                                limit {Size} offset {Offset}";
            int count = this.DapperRepository.QueryFirstOrDefault<int>(SqlCount, new { Key=$"%{Key}%", AppOrganizationId});
            var r = this.DapperRepository.QueryOriCommand<UserInfoView>(SqlQuery, true, new { Key = $"%{Key}%", Size, Offset, AppOrganizationId }).ToList();
            var data = StringManage.UserViewToUserDto(r);
            return new PagedList<UserInfoDto> { Index = Page, Size = Size, DataList = data, Total = count };
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <returns></returns>
        public UserInfoDto GetUserInfoOfId(string UserId) {

            var type = typeof(TUser);
            var typeR = typeof(TRelationUserOrganization);
            var typeO = typeof(TOrganization);;
            string SqlQuery = $@"select t.*,
                                t3.[Name] as [OrganizationName],
                                t4.[Name] as [DepartmentName],
                                t5.[Name] as [PositionName],
                                t3.[Code] as [OrganizationCode],
                                t4.[Code] as [DepartmentCode],
                                t5.[Code] as [PositionCode]
                                from 
                                (select t1.*,t2.[OrganizationId],t2.[DepartmentId],t2.[PositionId],t2.[UserType] from (select * from {type.PropName()} where [Id]=@UserId) t1 left join {typeR.PropName()} 
                                t2 on t1.[Id]=t2.[UserId]) t left join {typeO.PropName()} t3 on t.[OrganizationId]=t3.[Id] 
                                left join {typeO.PropName()} t4 on t.[DepartmentId]=t4.[Id] 
                                left join {typeO.PropName()} t5 on t.[PositionId]=t5.[Id]";
            var data = StringManage.UserViewToUserDto(this.DapperRepository.QueryOriCommand<UserInfoView>(SqlQuery, true, new { UserId}).ToList());
            if (data != null && data.Count > 0)
                return data[0];
            return null;
        }

        /// <summary>
        /// 批量查询用户
        /// </summary>
        /// <param name="UserIds"></param>
        /// <returns></returns>
        public IList<TUser> GetUsers(IList<string> UserIds) {
            var type = typeof(TUser);
            string sql = $"select * from {type.PropName()} where [Id] =ANY(@UserIds)";
            return this.DapperRepository.Query(sql, true, new { UserIds }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int DeleteUser(string UserId) {
            return this.DapperRepository.Delete(UserId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int UpdateUser(TUserUpdate user) {
            return this.DapperRepository.Update(user);
        }

        /// <summary>
        /// 判断用户是否存在该权限
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <param name="PrivilegeId">权限ID</param>
        /// <param name="PrivilegeCode"></param>
        /// <returns></returns>
        public int CountUserPrivilege(string UserId, string PrivilegeId, string PrivilegeCode)
        {
            var typeRp = typeof(TRelationPositionRole);
            var typeRr = typeof(TRelationRolePrivilege);
            var typeRu = typeof(TRelationUserOrganization);
            string sqlRp = $"select [PositionId],[RoleId] from {typeRp.PropName()}";
            string sqlRr = $"select [RoleId],[PrivilegeId],[PrivilegeCode] from {typeRr.PropName()}";
            string sqlRu = $"select [OrganizationIdO],[UserId] from {typeRu.PropName()} where [UserId]=@UserId";
            if (!string.IsNullOrEmpty(PrivilegeId))
            {
                string sql = $@"select count(*) from ({sqlRu}) t1 left join ({sqlRp}) t2 on t1.[OrganizationIdO]=t2.[PositionId] 
                        left join ({sqlRr}) t3 on t2.[RoleId]=t3.[RoleId] where t3.[PrivilegeId]=@PrivilegeId";
                return this.DapperRepository.QueryFirstOrDefault<int>(sql, new { UserId, PrivilegeId });
            }
            else if (!string.IsNullOrEmpty(PrivilegeCode))
            {
                string sql = $@"select count(*) from ({sqlRu}) t1 left join ({sqlRp}) t2 on t1.[OrganizationIdO]=t2.[PositionId] 
                        left join ({sqlRr}) t3 on t2.[RoleId]=t3.[RoleId] where t3.[PrivilegeCode]=@PrivilegeCode";
                return this.DapperRepository.QueryFirstOrDefault<int>(sql, new { UserId, PrivilegeCode });
            }
            else
                return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <param name="OrganizationName"></param>
        /// <returns></returns>
        public int UpdateOrganizationName(string OrganizationId, string OrganizationName) {
            var type = typeof(TUser);
            string sql = $"update {type.PropName()}, json_array_elements([ExtendAttribution]->'Organizations') r " +
                $"set r->>'OrganizationName'=@OrganizationName where r->>'OrganizationId'=@OrganizationId";
            return this.DapperRepository.ExcuteOriCommand(sql, true, new { OrganizationId, OrganizationName });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <param name="DepartmentName"></param>
        /// <returns></returns>
        public int UpdateDepartmentName(string DepartmentId, string DepartmentName)
        {
            var type = typeof(TUser);
            string sql = $"update {type.PropName()}, json_array_elements([ExtendAttribution]->'Organizations') r " +
                $"set r->>'DepartmentName'=@DepartmentName where r->>'DepartmentId'=@DepartmentId";
            return this.DapperRepository.ExcuteOriCommand(sql, true, new { DepartmentId, DepartmentName });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PositionId"></param>
        /// <param name="PositionName"></param>
        /// <returns></returns>
        public int UpdatePositionName(string PositionId, string PositionName)
        {
            var type = typeof(TUser);
            string sql = $"update {type.PropName()}, json_array_elements([ExtendAttribution]->'Organizations') r " +
                $"set r->>'PositionName'=@PositionName where r->>'PositionId'=@PositionId";
            return this.DapperRepository.ExcuteOriCommand(sql, true, new { PositionId, PositionName });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public string GetName(string UserId) {
            var type = typeof(TUser);
            string sql = $@"select [Name] from {type.PropName()} where [Id]=@UserId";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { UserId });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationCode"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public IList<AbstractDto> GetRelationOrganizationUsers(string OrganizationCode, string UserName) {
            var typeR = typeof(TRelationOrganization);
            var typeU = typeof(TUser);
            var typeRu = typeof(TRelationUserOrganization);
            string RelationAreaCode = this.DapperRepository.QueryFirstOrDefault<string>(
                $@"select [RelationAreaCode] from {typeR.PropName()} where [OrganizationCode]=@OrganizationCode", new { OrganizationCode });
            if (string.IsNullOrEmpty(RelationAreaCode))
                return new List<AbstractDto>();
            if (string.IsNullOrEmpty(UserName))
                UserName = "";
            var sql = $@"select t2.[Id],t2.[Name] from
                        (select [UserId] from {typeRu.PropName()} where [DepartmentCode] 
                        like @DepartmentCode or [PositionCode] like @PositionCode
                    ) t1 left join {typeU.PropName()} t2 on t1.[UserId]=t2.[Id] where t2.[Name] like @UserName limit 20
                        ";
            return this.DapperRepository.QueryOriCommand<AbstractDto>(sql, true,
                        new { OrganizationCode, DepartmentCode = $"{RelationAreaCode}%", PositionCode = $"{RelationAreaCode}%", UserName=$"%{UserName}%" })
                        .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public string GetUserSystemCode(string UserId) {
            var type = typeof(TUser);
            string sql = $"select [SystemCode] from {type.PropName()} where [Id]=@UserId";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { UserId });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Name"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public PagedList<OrganizationAbstractDto> QueryUserOrganizationsByName(string UserId, string Name, int Page, int Size) {
            var typeR = typeof(TRelationUserOrganization);
            var typeO = typeof(TOrganization);
            var typeRo = typeof(TRelationOrganization);
            string sqlO = $@"select [OrganizationCode] from {typeR.PropName()} where [UserId]=@UserId and [UserType]=0";
            IList<string> vs = this.DapperRepository.QueryOriCommand<string>(sqlO, true, new { UserId }).ToList();
            if (vs.Count == 0)
                return new PagedList<OrganizationAbstractDto> { DataList=new List<OrganizationAbstractDto>()};
            string sqlR = $"select [OrganizationCode] from {typeRo.PropName()} where [RelationOrganizationCode]=ANY(@vs)";
            IList<string> rs = this.DapperRepository.QueryOriCommand<string>(sqlR, true, new { vs }).ToList();
            string join;
            if (rs.Count == 0)
                join = vs.Join(",");
            else
                join = rs.Join(",");
            IList<OrganizationAbstractDto> data;
            int count = 0;
            if (Page == 0 && Size == 0) {
                if (string.IsNullOrEmpty(Name))
                {
                    string sql = $@"create temporary table c as select regexp_split_to_table(col,',') AS oriCode from (values('{join}')) t(col);
                                    create index _c on c(oriCode);
                                    select * from {typeO.PropName()} t inner join  c
                                    on t.[Code] like oriCode||'%'";
                    data = this.DapperRepository.QueryOriCommand<OrganizationAbstractDto>(sql).ToList();
                }
                else
                {
                    string sql = $@"create temporary table c as select regexp_split_to_table(col,',') AS oriCode from (values('{join}')) t(col);
                                    create index _c on c(oriCode);
                                    select * from {typeO.PropName()} t inner join  c
                                    on t.[Code] like oriCode||'%' where t.[Name] like @Name";
                    data = this.DapperRepository.QueryOriCommand<OrganizationAbstractDto>(sql, true, new { Name = $"%{Name}%" }).ToList();
                }
                return new PagedList<OrganizationAbstractDto> { DataList = data };
            }
            int offset = Size * (Page-1);
            if (string.IsNullOrEmpty(Name))
            {
                string sqlC = $@"create temporary table c as select regexp_split_to_table(col,',') AS oriCode from (values('{join}')) t(col);
                                 create index _c on c(oriCode);
                                 select count(*) from {typeO.PropName()} t inner join c
                                 on t.[Code] like oriCode||'%'";
                string sql = $@"create temporary table c as select regexp_split_to_table(col,',') AS oriCode from (values('{join}')) t(col);
                                create index _c on c(oriCode);
                                select * from {typeO.PropName()} t inner join c
                                on t.[Code] like oriCode||'%' limit {Size} offset {offset}";
                count = this.DapperRepository.QueryFirstOrDefault<int>(sqlC);
                data = this.DapperRepository.QueryOriCommand<OrganizationAbstractDto>(sql).ToList();
            }
            else {
                string sqlC = $@"create temporary table c as select regexp_split_to_table(col,',') AS oriCode from (values('{join}')) t(col);
                                 create index _c on c(oriCode);
                                 select count(*) from {typeO.PropName()} t inner join c
                                 on t.[Code] like oriCode||'%' where t.[Name] like @Name";
                string sql = $@"create temporary table c as select regexp_split_to_table(col,',') AS oriCode from (values('{join}')) t(col);
                                create index _c on c(oriCode);
                                select * from {typeO.PropName()} t inner join c
                                on t.[Code] like oriCode||'%'  where t.[Name] like @Name limit {Size} offset {offset}";
                count = this.DapperRepository.QueryFirstOrDefault<int>(sqlC, new { Name=$"%{Name}%"});
                data = this.DapperRepository.QueryOriCommand<OrganizationAbstractDto>(sql, true, new { Name = $"%{Name}%" }).ToList();
            }
            return new PagedList<OrganizationAbstractDto> { Total = count, Index = Page, Size = Size, DataList = data };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Name"></param>
        /// <param name="Role"></param>
        /// <param name="IsBeilian"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public PagedList<V2UserInfoDto> GetUsers(string UserId, string Name, IList<int> Role, bool IsBeilian, int Page, int Size)
        {
            var typeR = typeof(TRelationUserOrganization);
            var typeU = typeof(TUser);
            var typeO = typeof(TOrganization);
            List<string> Roles = new List<string>();
            Role.Foreach(item=>Roles.Add(item.ToString()));
            if (string.IsNullOrEmpty(Name))
                Name = string.Empty;
            string sql = $@"select [OrganizationCode] from {typeR.PropName()} where [UserId]=@UserId";
            IList<string> Code = this.DapperRepository.QueryOriCommand<string>(sql, true, new { UserId }).ToList();
            string join = Code.Join(",");
            string sqlLike;
            if (IsBeilian)
                sqlLike = $@"(t1.[SpareMobile] like @Name or t1.[SpareName] like @Name 
                        or t1.[Mobile] like @Name or t1.[Name] like @Name)";
            else
                sqlLike = $@"(t1.[Mobile] like @Name or t1.[Name] like @Name)";

            if (Page == 0 && Size == 0)
            {
                sql = $@"create temporary table c as select regexp_split_to_table(col,',') AS oriCode from (values('{join}')) t(col);
                         create index _c on c(oriCode);
                         select t1.*,
                                   t3.[Id] as [UnitId],
                                   t3.[ExtendAttribution]->>'unitName' as [UnitName],
                                   t3.[ExtendAttribution]->>'shorterName' as [ShorterName],
                                   t3.[ExtendAttribution]->>'unitMobile' as [UnitMobile],
                                   t3.[ExtendAttribution]->>'contactName' as [ContactName],
                                   t3.[ExtendAttribution]->>'unitCode' as [UnitCode],
                                   t3.[ExtendAttribution]->>'contactMobile' as [ContactMobile],
                                   t3.[ExtendAttribution]->>'unitState' as [UnitState],
                                   t3.[ExtendAttribution]->>'unitAddress' as [UnitAddress],
                                   t3.[ExtendAttribution]->>'unitRemartk' as [UnitRemark] ,
                                   t3.[ExtendAttribution]->>'contactName2' as [ContactName2],
                                   t3.[ExtendAttribution]->>'contactMobile2' as [ContactMobile2]
                            from 
                            (select [Id],[Name],[Telephone] as [Mobile],[Account],[Email],[Instruction],
                                    [ExtendAttribution]->>'Sex' as [Sex],
                                    [ExtendAttribution]->>'SpareName' as [SpareName],
                                    [ExtendAttribution]->>'SpareMobile' as [SpareMobile],
                                    [ExtendAttribution]->>'CreatorId' as [CreatorId],
                                    [ExtendAttribution]->>'CreateTime' as [CreateTime],
                                    [ExtendAttribution]->>'IsCompany' as [IsCompany],
                                    [ExtendAttribution]->>'NickName' as [NickName],
                                    [ExtendAttribution]->>'SourceType' as [SourceType],
                                    [ExtendAttribution]->>'Birthday' as [Birthday],
                                    [ExtendAttribution]->>'StoreCredit' as [StoreCredit],
                                    [ExtendAttribution]->>'RoleValue' as [RoleValue],
                                    [ExtendAttribution]->>'Vip' as [Vip],
                                    [ExtendAttribution]->>'HxUserId' as [HxUserId],
                                    [ExtendAttribution]->>'Location' as [Location],
                                    [ExtendAttribution]->>'Vehicle' as [Vehicle],
                                    [ExtendAttribution]->>'VehicleName' as [VehicleName],
                                    [ExtendAttribution]->>'Role' as [Role],
                                     [ExtendAttribution]->>'IdCard' as [IdCard],
                                    [ExtendAttribution]->>'CardType' as [CardType]
                               from {typeU.PropName()}) t1 left join {typeR.PropName()} t2 on t1.[Id]=t2.[UserId]
                                left join {typeO.PropName()} t3 on t2.[OrganizationId]=t3.[Id]
                         inner join  c on t2.[OrganizationCode] like oriCode||'%'
                         where t1.[RoleValue]=ANY(@Roles) and {sqlLike}";
                //var data = this.DapperRepository.QueryOriCommand<V2UserInfoDto>(sql, true, new { Name = $"%{Name}%", Role=$"{Role}" }).ToList();
                var data = this.DapperRepository.QueryOriCommand<V2UserInfoDto>(sql, true, new { Name = $"%{Name}%", Roles }).ToList();
                return new PagedList<V2UserInfoDto> { DataList = data };
            }
            else
            {
                int Total;
                string SqlCount;
                int Offset = (Page - 1) * Size;
                SqlCount = $@"create temporary table c as select regexp_split_to_table(col,',') AS oriCode from (values('{join}')) t(col);
                              create index _c on c(oriCode);
                              select count(distinct t1.[Id]) from (select [Id],[Telephone] as [Mobile],[Name],
                                    [ExtendAttribution]->>'SpareName' as [SpareName],
                                    [ExtendAttribution]->>'SpareMobile' as [SpareMobile],
                                    [ExtendAttribution]->>'RoleValue' as [RoleValue] from {typeU.PropName()}) t1 left join {typeR.PropName()} t2 
                              on t1.[Id]=t2.[UserId] 
                              inner join  c on t2.[OrganizationCode] like oriCode||'%'
                              where t1.[RoleValue]=@Role and {sqlLike}";
                Total = this.DapperRepository.QueryFirstOrDefault<int>(SqlCount, new { Name = $"%{Name}%", Code = $"{Code}%", Role = $"{Role}" });
                sql = $@"create temporary table c as select regexp_split_to_table(col,',') AS oriCode from (values('{join}')) t(col);
                         create index _c on c(oriCode);
                        select t1.*,
                                   t3.[Id] as [UnitId],
                                   t3.[ExtendAttribution]->>'unitName' as [UnitName],
                                   t3.[ExtendAttribution]->>'shorterName' as [ShorterName],
                                   t3.[ExtendAttribution]->>'unitMobile' as [UnitMobile],
                                   t3.[ExtendAttribution]->>'contactName' as [ContactName],
                                   t3.[ExtendAttribution]->>'unitCode' as [UnitCode],
                                   t3.[ExtendAttribution]->>'contactMobile' as [ContactMobile],
                                   t3.[ExtendAttribution]->>'unitState' as [UnitState],
                                   t3.[ExtendAttribution]->>'unitAddress' as [UnitAddress],
                                   t3.[ExtendAttribution]->>'unitRemartk' as [UnitRemark],
                                   t3.[ExtendAttribution]->>'contactName2' as [ContactName2],
                                   t3.[ExtendAttribution]->>'contactMobile2' as [ContactMobile2]
                            from 
                            (select [Id],[Name],[Telephone] as [Mobile],[Account],[Email],[Instruction],
                                    [ExtendAttribution]->>'Sex' as [Sex],
                                    [ExtendAttribution]->>'SpareName' as [SpareName],
                                    [ExtendAttribution]->>'SpareMobile' as [SpareMobile],
                                    [ExtendAttribution]->>'CreatorId' as [CreatorId],
                                    [ExtendAttribution]->>'CreateTime' as [CreateTime],
                                    [ExtendAttribution]->>'IsCompany' as [IsCompany],
                                    [ExtendAttribution]->>'NickName' as [NickName],
                                    [ExtendAttribution]->>'SourceType' as [SourceType],
                                    [ExtendAttribution]->>'Birthday' as [Birthday],
                                    [ExtendAttribution]->>'StoreCredit' as [StoreCredit],
                                    [ExtendAttribution]->>'RoleValue' as [RoleValue],
                                    [ExtendAttribution]->>'Vip' as [Vip],
                                    [ExtendAttribution]->>'HxUserId' as [HxUserId],
                                    [ExtendAttribution]->>'Location' as [Location],
                                    [ExtendAttribution]->>'Vehicle' as [Vehicle],
                                    [ExtendAttribution]->>'VehicleName' as [VehicleName],
                                    [ExtendAttribution]->>'Role' as [Role],
                                     [ExtendAttribution]->>'IdCard' as [IdCard],
                                    [ExtendAttribution]->>'CardType' as [CardType]
                               from {typeU.PropName()}) t1 left join {typeR.PropName()} t2 on t1.[Id]=t2.[UserId]
                                left join {typeO.PropName()} t3 on t2.[OrganizationId]=t3.[Id]
                         inner join  c on t2.[OrganizationCode] like oriCode||'%'
                         where t1.[RoleValue]=ANY(@Roles) and {sqlLike} order by t1.[CreateTime]
                         limit {Size} offset {Offset}";
                //var data = this.DapperRepository.QueryOriCommand<V2UserInfoDto>(sql, true, new { Name = $"%{Name}%", Code = $"{Code}%", Role = $"{Role}" }).ToList();
                var data = this.DapperRepository.QueryOriCommand<V2UserInfoDto>(sql, true, new { Name = $"%{Name}%", Code = $"{Code}%", Roles }).ToList();
                return new PagedList<V2UserInfoDto> { Total = Total, Index = Page, Size = Size, DataList = data };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Role"></param>
        /// <param name="IsBeilian"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public PagedList<V2UserInfoDto> GetV2UsersAll(string Name, IList<int> Role, bool IsBeilian, int Page, int Size)
        {
            var typeR = typeof(TRelationUserOrganization);
            var typeU = typeof(TUser);
            var typeO = typeof(TOrganization);
            List<string> Roles = new List<string>();
            Role.Foreach(item=>Roles.Add(item.ToString()));
            if (string.IsNullOrEmpty(Name))
                Name = string.Empty;
            string sqlLike;
            string sql;
            if (IsBeilian)
                sqlLike = $@"(t1.[SpareMobile] like @Name or t1.[SpareName] like @Name 
                        or t1.[Mobile] like @Name or t1.[Name] like @Name)";
            else
                sqlLike = $@"(t1.[Mobile] like @Name or t1.[Name] like @Name)";

            if (Page == 0 && Size == 0)
            {
                sql = $@"
                         select t1.*,
                                   t3.[Id] as [UnitId],
                                   t3.[ExtendAttribution]->>'unitName' as [UnitName],
                                   t3.[ExtendAttribution]->>'shorterName' as [ShorterName],
                                   t3.[ExtendAttribution]->>'unitMobile' as [UnitMobile],
                                   t3.[ExtendAttribution]->>'contactName' as [ContactName],
                                   t3.[ExtendAttribution]->>'unitCode' as [UnitCode],
                                   t3.[ExtendAttribution]->>'contactMobile' as [ContactMobile],
                                   t3.[ExtendAttribution]->>'unitState' as [UnitState],
                                   t3.[ExtendAttribution]->>'unitAddress' as [UnitAddress],
                                   t3.[ExtendAttribution]->>'unitRemartk' as [UnitRemark] ,
                                   t3.[ExtendAttribution]->>'contactName2' as [ContactName2],
                                   t3.[ExtendAttribution]->>'contactMobile2' as [ContactMobile2]
                            from 
                            (select [Id],[Name],[Telephone] as [Mobile],[Account],[Email],[Instruction],
                                    [ExtendAttribution]->>'Sex' as [Sex],
                                    [ExtendAttribution]->>'SpareName' as [SpareName],
                                    [ExtendAttribution]->>'SpareMobile' as [SpareMobile],
                                    [ExtendAttribution]->>'CreatorId' as [CreatorId],
                                    [ExtendAttribution]->>'CreateTime' as [CreateTime],
                                    [ExtendAttribution]->>'IsCompany' as [IsCompany],
                                    [ExtendAttribution]->>'NickName' as [NickName],
                                    [ExtendAttribution]->>'SourceType' as [SourceType],
                                    [ExtendAttribution]->>'Birthday' as [Birthday],
                                    [ExtendAttribution]->>'StoreCredit' as [StoreCredit],
                                    [ExtendAttribution]->>'RoleValue' as [RoleValue],
                                    [ExtendAttribution]->>'Vip' as [Vip],
                                    [ExtendAttribution]->>'HxUserId' as [HxUserId],
                                    [ExtendAttribution]->>'Location' as [Location],
                                    [ExtendAttribution]->>'Vehicle' as [Vehicle],
                                    [ExtendAttribution]->>'VehicleName' as [VehicleName],
                                    [ExtendAttribution]->>'Role' as [Role],
                                    [ExtendAttribution]->>'IdCard' as [IdCard],
                                    [ExtendAttribution]->>'CardType' as [CardType]
                               from {typeU.PropName()}) t1 left join {typeR.PropName()} t2 on t1.[Id]=t2.[UserId]
                                left join {typeO.PropName()} t3 on t2.[OrganizationId]=t3.[Id]
                         where t1.[RoleValue]=ANY(@Roles) and {sqlLike}";
                var data = this.DapperRepository.QueryOriCommand<V2UserInfoDto>(sql, true, new { Name = $"%{Name}%", Roles }).ToList();
                return new PagedList<V2UserInfoDto> { DataList = data };
            }
            else
            {
                int Total;
                string SqlCount;
                int Offset = (Page - 1) * Size;
                SqlCount = $@"
                              select count(distinct t1.[Id]) from (select [Id],[Name],[Telephone] as [Mobile],
                                    [ExtendAttribution]->>'SpareName' as [SpareName],
                                    [ExtendAttribution]->>'SpareMobile' as [SpareMobile],
                                    [ExtendAttribution]->>'RoleValue' as [RoleValue] from {typeU.PropName()}) t1 left join {typeR.PropName()} t2 
                              on t1.[Id]=t2.[UserId] 
                              where t1.[RoleValue]=ANY(@Roles) and {sqlLike}";
                Total = this.DapperRepository.QueryFirstOrDefault<int>(SqlCount, new { Name = $"%{Name}%", Roles });
                sql = $@"
                        select t1.*,
                                   t3.[Id] as [UnitId],
                                   t3.[ExtendAttribution]->>'unitName' as [UnitName],
                                   t3.[ExtendAttribution]->>'shorterName' as [ShorterName],
                                   t3.[ExtendAttribution]->>'unitMobile' as [UnitMobile],
                                   t3.[ExtendAttribution]->>'contactName' as [ContactName],
                                   t3.[ExtendAttribution]->>'unitCode' as [UnitCode],
                                   t3.[ExtendAttribution]->>'contactMobile' as [ContactMobile],
                                   t3.[ExtendAttribution]->>'unitState' as [UnitState],
                                   t3.[ExtendAttribution]->>'unitAddress' as [UnitAddress],
                                   t3.[ExtendAttribution]->>'unitRemartk' as [UnitRemark] ,
                                   t3.[ExtendAttribution]->>'contactName2' as [ContactName2],
                                   t3.[ExtendAttribution]->>'contactMobile2' as [ContactMobile2]
                            from 
                            (select [Id],[Name],[Telephone] as [Mobile],[Account],[Email],[Instruction],
                                    [ExtendAttribution]->>'Sex' as [Sex],
                                    [ExtendAttribution]->>'SpareName' as [SpareName],
                                    [ExtendAttribution]->>'SpareMobile' as [SpareMobile],
                                    [ExtendAttribution]->>'CreatorId' as [CreatorId],
                                    [ExtendAttribution]->>'CreateTime' as [CreateTime],
                                    [ExtendAttribution]->>'IsCompany' as [IsCompany],
                                    [ExtendAttribution]->>'NickName' as [NickName],
                                    [ExtendAttribution]->>'SourceType' as [SourceType],
                                    [ExtendAttribution]->>'Birthday' as [Birthday],
                                    [ExtendAttribution]->>'StoreCredit' as [StoreCredit],
                                    [ExtendAttribution]->>'RoleValue' as [RoleValue],
                                    [ExtendAttribution]->>'Vip' as [Vip],
                                    [ExtendAttribution]->>'HxUserId' as [HxUserId],
                                    [ExtendAttribution]->>'Location' as [Location],
                                    [ExtendAttribution]->>'Vehicle' as [Vehicle],
                                    [ExtendAttribution]->>'VehicleName' as [VehicleName],
                                    [ExtendAttribution]->>'Role' as [Role],
                                    [ExtendAttribution]->>'IdCard' as [IdCard],
                                    [ExtendAttribution]->>'CardType' as [CardType]
                               from {typeU.PropName()}) t1 left join {typeR.PropName()} t2 on t1.[Id]=t2.[UserId]
                                left join {typeO.PropName()} t3 on t2.[OrganizationId]=t3.[Id]
                         where t1.[RoleValue]=ANY(@Roles) and {sqlLike} order by t1.[CreateTime]
                         limit {Size} offset {Offset}";
                var data = this.DapperRepository.QueryOriCommand<V2UserInfoDto>(sql, true, new { Name = $"%{Name}%", Roles}).ToList();
                return new PagedList<V2UserInfoDto> { Total = Total, Index = Page, Size = Size, DataList = data };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Name"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public PagedList<UserInfoDto> QueryUserRelationUserByName(string UserId, string Name, int Page, int Size) {
            var typeR = typeof(TRelationUserOrganization);
            var typeRo = typeof(TRelationOrganization);
            var type = typeof(TUser);
            string sqlO = $@"select [OrganizationCode] from {typeR.PropName()} where [UserId]=@UserId and [UserType]=0";
            IList<string> vs = this.DapperRepository.QueryOriCommand<string>(sqlO, true, new { UserId }).ToList();
            if (vs.Count == 0)
                return new PagedList<UserInfoDto> { DataList = new List<UserInfoDto>() };
            string sqlR = $"select [OrganizationCode] from {typeRo.PropName()} where [RelationOrganizationCode]=ANY(@vs)";
            IList<string> rs = this.DapperRepository.QueryOriCommand<string>(sqlR, true, new { vs }).ToList();
            string join;
            if (rs.Count == 0)
                join = vs.Join(",");
            else
                join = rs.Join(",");
            IList<UserInfoDto> data;
            if (string.IsNullOrEmpty(Name))
                Name = string.Empty;
            int count = 0;
            if (Page == 0 && Size == 0)
            {
                string sql = $@"create temporary table c as select regexp_split_to_table(col,',') AS oriCode from (values('{join}')) t(col);
                                create index _c on c(oriCode);
                                select t2.* from {typeR.PropName()} t1 inner join c
                                on t1.[OrganizationCode] like oriCode||'%'
                                left join {type.PropName()} t2 on t1.[UserId]=t2.[Id]
                                where t2.[Name] like @Name or t2.[Telephone] like @Name group by t2.[Id]";
                data = this.DapperRepository.QueryOriCommand<UserInfoDto>(sql, true, new { Name = $"%{Name}%" }).ToList();
                return new PagedList<UserInfoDto> { DataList = data };
            }
            else
            {
                int offset = Size * (Page - 1);
                string sqlC = $@"create temporary table c as select regexp_split_to_table(col,',') AS oriCode from (values('{join}')) t(col);
                                create index _c on c(oriCode);
                                select count(distinct t2.[Id]) from {typeR.PropName()} t1 inner join c
                                on t1.[OrganizationCode] like oriCode||'%' 
                                left join {type.PropName()} t2 on t1.[UserId]=t2.[Id] 
                                where t2.[Name] like @Name or t2.[Telephone] like @Name";
                string sql = $@"create temporary table c as select regexp_split_to_table(col,',') AS oriCode from (values('{join}')) t(col);
                                create index _c on c(oriCode);
                                select t2.* from {typeR.PropName()} t1 inner join c
                                on t1.[OrganizationCode] like oriCode||'%'
                                left join {type.PropName()} t2 on t1.[UserId]=t2.[Id] 
                                where t2.[Name] like @Name or t2.[Telephone] like @Name group by t2.[Id]
                                limit {Size} offset {offset}";
                count = this.DapperRepository.QueryFirstOrDefault<int>(sqlC, new { Name = $"%{Name}%" });
                data = this.DapperRepository.QueryOriCommand<UserInfoDto>(sql, true, new { Name = $"%{Name}%" }).ToList();
                return new PagedList<UserInfoDto> { Total = count, Index = Page, Size = Size, DataList = data };
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserIds"></param>
        /// <returns></returns>
        public IList<V2UserInfoDto> GetV2UserInfos(IList<string> UserIds) {
            var type = typeof(TUser);
            var typeR = typeof(TRelationUserOrganization);
            var typeO = typeof(TOrganization);
            string sql = $@"select t1.*,
                                   t3.[Id] as [UnitId],
                                   t3.[ExtendAttribution]->>'unitName' as [UnitName],
                                   t3.[ExtendAttribution]->>'shorterName' as [ShorterName],
                                   t3.[ExtendAttribution]->>'unitMobile' as [UnitMobile],
                                   t3.[ExtendAttribution]->>'contactName' as [ContactName],
                                   t3.[ExtendAttribution]->>'unitCode' as [UnitCode],
                                   t3.[ExtendAttribution]->>'contactMobile' as [ContactMobile],
                                   t3.[ExtendAttribution]->>'unitState' as [UnitState],
                                   t3.[ExtendAttribution]->>'unitAddress' as [UnitAddress],
                                   t3.[ExtendAttribution]->>'unitRemartk' as [UnitRemark] ,
                                   t3.[ExtendAttribution]->>'contactName2' as [ContactName2],
                                   t3.[ExtendAttribution]->>'contactMobile2' as [ContactMobile2]
                            from 
                            (select [Id],[Name],[Telephone] as [Mobile],[Account],[Email],[Instruction],
                                    [ExtendAttribution]->>'Sex' as [Sex],
                                    [ExtendAttribution]->>'SpareName' as [SpareName],
                                    [ExtendAttribution]->>'SpareMobile' as [SpareMobile],
                                    [ExtendAttribution]->>'CreatorId' as [CreatorId],
                                    [ExtendAttribution]->>'CreateTime' as [CreateTime],
                                    [ExtendAttribution]->>'IsCompany' as [IsCompany],
                                    [ExtendAttribution]->>'NickName' as [NickName],
                                    [ExtendAttribution]->>'SourceType' as [SourceType],
                                    [ExtendAttribution]->>'Birthday' as [Birthday],
                                    [ExtendAttribution]->>'StoreCredit' as [StoreCredit],
                                    [ExtendAttribution]->>'RoleValue' as [RoleValue],
                                    [ExtendAttribution]->>'Vip' as [Vip],
                                    [ExtendAttribution]->>'HxUserId' as [HxUserId],
                                    [ExtendAttribution]->>'Location' as [Location],
                                    [ExtendAttribution]->>'Vehicle' as [Vehicle],
                                    [ExtendAttribution]->>'VehicleName' as [VehicleName],
                                    [ExtendAttribution]->>'Role' as [Role],
                                    [ExtendAttribution]->>'IdCard' as [IdCard],
                                    [ExtendAttribution]->>'CardType' as [CardType]
                               from {type.PropName()} where [Id]=ANY(@UserIds)
                                    ) t1 left join {typeR.PropName()} t2 on t1.[Id]=t2.[UserId]
                                left join {typeO.PropName()} t3 on t2.[OrganizationId]=t3.[Id]";
            return this.DapperRepository.QueryOriCommand<V2UserInfoDto>(sql, true, new { UserIds }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SystemCode"></param>
        /// <param name="Key"></param>
        /// <param name="RoleCode"></param>
        /// <param name="UnitCode"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public PagedList<UserInfoDto> GetRoleUsers(string SystemCode, string RoleCode, string Key, string UnitCode, int Page, int Size) {
            var type = typeof(TUser);
            var typeRr = typeof(TRelationUserRole);
            var typeRu = typeof(TRelationUserOrganization);
            var typeRp = typeof(TRelationPositionRole);
            var typeO = typeof(TOrganization);
            if (Key.IsNullOrEmpty())
                Key = "";
            string sql = $@"select tt2.* 
                            from (select distinct t1.[Id] from 
                            (select [Id] from {type.PropName()} where [SystemCode]=@SystemCode and [Name] like @Key) 
                            t1 left join {typeRr.PropName()} t2 on t1.[Id]=t2.[UserId] 
                            left join {typeRu.PropName()} t3 on t1.[Id]=t3.[UserId] 
                            left join {typeRp.PropName()} t4 on t3.[OrganizationIdO]=t4.[PositionId] where (t2.[RoleCode]=@RoleCode or t4.[RoleCode]=@RoleCode) 
                            and t3.[OrganizationCode] like @UnitCode) tt1
                            left join {type.PropName()} tt2 on tt1.[Id]=tt2.[Id] ";
            if (Page == 0 && Size == 0)
            {
                var data = this.DapperRepository.QueryOriCommand<UserInfoDto>(sql, true, new { SystemCode, RoleCode, Key=$"%{Key}%" ,
                    UnitCode=$"{UnitCode}%" }).ToList();
                return new PagedList<UserInfoDto> { DataList = data };
            }
            else {
                return this.DapperRepository.PagedList<UserInfoDto>(sql, Page, Size, new { SystemCode, RoleCode, Key = $"%{Key}%", UnitCode = $"{UnitCode}%" } ) as PagedList<UserInfoDto>;
            }
        }
    }
}
