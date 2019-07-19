using Acb.Plugin.PrivilegeManage.Models.Entities;
using Acb.MiddleWare.Data.DB;
using Dynamic.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Dynamic.Core.Extensions;
using CDynamic.Dapper;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization;
using Dynamic.Core;
using Acb.Plugin.PrivilegeManage.Common;
using Acb.Plugin.PrivilegeManage.Models.View.Organization;

namespace Acb.Plugin.PrivilegeManage.Models.Repository
{
    /// <summary>
    /// 机构管理数据库操作
    /// </summary>
    public class RepositoryOrganization : DBBase<TOrganization>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dBConfig"></param>
        /// <returns></returns>
        public RepositoryOrganization(DBCfgViewModel dBConfig) : base(dBConfig) { }

        /// <summary>
        /// 添加机构
        /// </summary>
        /// <param name="organization">机构对象</param>
        /// <returns></returns>
        public int Insert(TOrganization organization)
        {
            return this.DapperRepository.Insert(organization, excepts: new[] { nameof(TOrganization.CreateTime) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TOrganization GetById(string Id) {
            return this.DapperRepository.QueryById(Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParentId"></param>
        /// <param name="IsType"></param>
        /// <returns></returns>
        public bool IsHasChildren(string ParentId, bool IsType) {
            var type = typeof(TOrganization);
            string sql;
            int count;
            if (IsType)
                sql = $"select count(*) from {type.PropName()} where [TypeId]=@ParentId";
            else
                sql = $"select count(*) from {type.PropName()} where [ParentId]=@ParentId";
            count = this.DapperRepository.QueryFirstOrDefault<int>(sql, new { ParentId });
            if (count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GetExtendAttribution(string Id) {
            var type = typeof(TOrganization);
            string sql = $"select [ExtendAttribution] from {type.PropName()} where [Id]=@Id";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { Id });
        }
        /// <summary>
        /// 获取 Id
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public string GetId(string Code) {
            var type = typeof(TOrganization);
            if (string.IsNullOrEmpty(Code))
                return null;
            string sql = $"select [Id] from {type.PropName()} where [Code]=@Code";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { Code });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public string GetNameByCode(string Code) {
            var type = typeof(TOrganization);
            string sql = $"select [Name] from {type.PropName()} where [Code]=@Code";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { Code });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GetNameById(string Id)
        {
            var type = typeof(TOrganization);
            string sql = $"select [Name] from {type.PropName()} where [Id]=@Id";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { Id });
        }

        /// <summary>
        /// 获取子级机构
        /// </summary>
        /// <param name="ParentId">父级机构ID</param>
        /// <returns></returns>
        public IList<OrganizationDto> GetSubOrganizationOfId(string ParentId)
        {
            var type = typeof(TOrganization);
            string sql = $@"select t1.*,t2.[Name] as [ParentName] from (select { type.Columns() } from { type.PropName()} where [ParentId]=@ParentId and [HierarchyType]=0) t1 
                        left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id]";
            return AutoMapperExtensions.MapTo<OrganizationDto>(this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { ParentId }).ToList());
        }

        /// <summary>
        /// 获取子级
        /// </summary>
        /// <param name="ParentId">父级机构ID</param>
        /// <returns></returns>
        public IList<OrganizationDto> GetSub(string ParentId)
        {
            var type = typeof(TOrganization);
            string sql = $@"select t1.*,t2.[Name] as [ParentName] from (select { type.Columns() } from { type.PropName()} where [ParentId]=@ParentId) t1 
                        left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id]";
            return AutoMapperExtensions.MapTo<OrganizationDto>(this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { ParentId }).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public string GetTypeId(string Code) {
            var type = typeof(TOrganization);
            string sql = $"select [TypeId] from {type.PropName()} where [Code]=@Code";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { Code });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SystemCode"></param>
        /// <param name="TypeName"></param>
        /// <returns></returns>
        public string GetTypeId(string SystemCode, string TypeName) {
            var type = typeof(TOrganizationType);
            var typeS = typeof(TSystem);
            string sql = $@"select [TypeId] from {type.PropName()} t1 left join {typeS.PropName()} t2 
                            on t1.[SystemId]=t2.[Id] where t1.[Name]=@TypeName and t2.[Code]=@SystemCode";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { SystemCode, TypeName});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GetTypeIdOfId(string Id)
        {
            var type = typeof(TOrganization);
            string sql = $"select [TypeId] from {type.PropName()} where [Id]=@Id";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { Id });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        public string GetCode(string OrganizationId)
        {
            var type = typeof(TOrganization);
            string sql = $"select [Code] from {type.PropName()} where [Id]=@OrganizationId";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { OrganizationId });
        }

        /// <summary>
        /// 分页获取子级机构
        /// </summary>
        /// <param name="ParentId">父级机构ID</param>
        /// <param name="Page">页码</param>
        /// <param name="Size">行数</param>
        /// <returns></returns>
        public PagedList<OrganizationDto> GetSubOrganizationOfId(string ParentId, int Page, int Size)
        {
            var type = typeof(TOrganization);
            int Offset = (Page - 1) * Size;
            string sql1 = $@"select count(*) from {type.PropName()} where [ParentId]=@ParentId and [HierarchyType]=0";
            int count = this.DapperRepository.QueryFirstOrDefault<int>(sql1, new { ParentId });
            string sql2 = $@"select t1.*,t2.[Name] as [ParentName] from (select { type.Columns() } from { type.PropName()} where [ParentId]=@ParentId and [HierarchyType]=0) t1 
                        left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id] limit @Size offset @Offset";
            var data = AutoMapperExtensions.MapTo<OrganizationDto>(this.DapperRepository.QueryOriCommand<OrganizationDto>(sql2, true, new { ParentId, Size, Offset }).ToList());
            return new PagedList<OrganizationDto> { Index = Page, Size = Size, DataList = data, Total = count };
        }

        /// <summary>
        /// 分页获取子级机构
        /// </summary>
        /// <param name="ParentId">父级机构ID</param>
        /// <param name="Page">页码</param>
        /// <param name="Size">行数</param>
        /// <returns></returns>
        public PagedList<OrganizationDto> GetSubOrganizationOfIdAndParent(string ParentId, int Page, int Size)
        {
            var type = typeof(TOrganization);
            
            if (Page == 0 && Size == 0) {
                string sql = $@"select t1.*,t2.[Name] as [ParentName] from (select { type.Columns() } from { type.PropName()} where [ParentId]=@ParentId or 
                        [Id]=@ParentId order by [CreateTime]) t1 
                        left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id]";
                var d = AutoMapperExtensions.MapTo<OrganizationDto>(this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { ParentId}).ToList());
                return new PagedList<OrganizationDto> { Index = Page, Size = Size, DataList = d, Total = 0 };
            }
            int Offset = (Page - 1) * Size;
            string sql1 = $@"select count(*) from {type.PropName()} where [ParentId]=@ParentId or [Id]=@ParentId";
            int count = this.DapperRepository.QueryFirstOrDefault<int>(sql1, new { ParentId });
            string sql2 = $@"select t1.*,t2.[Name] as [ParentName] from (select { type.Columns() } from { type.PropName()} where [ParentId]=@ParentId or 
                        [Id]=@ParentId order by [CreateTime] limit @Size offset @Offset) t1 
                        left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id]";
            var data = AutoMapperExtensions.MapTo<OrganizationDto>(this.DapperRepository.QueryOriCommand<OrganizationDto>(sql2, true, new { ParentId, Size, Offset }).ToList());
            return new PagedList<OrganizationDto> { Index = Page, Size = Size, DataList = data, Total = count };
        }

        /// <summary>
        /// 更新机构
        /// </summary>
        /// <param name="organization">机构对象</param>
        /// <returns></returns>
        public int Update<T>(T organization)
        {
            return this.DapperRepository.Update(organization);
        }


        /// <summary>
        /// 删除机构
        /// </summary>
        /// <param name="OrganizationId">机构ID</param>
        /// <returns></returns>
        public int DeleteOrganization(string OrganizationId)
        {
            return this.DapperRepository.Delete(OrganizationId);
        }

        /// <summary>
        /// 机构类型ID
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        public IList<OrganizationDto> GetOrganizationOfType(string TypeId)
        {
            var type = typeof(TOrganization);
            string sql = $@"select t1.* ,t2.[Name] as [ParentName] from (select * from {type.PropName()} where [TypeId]=@TypeId and [ParentId] is null) t1 
                        left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id]";
            return AutoMapperExtensions.MapTo<OrganizationDto>(this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { TypeId }).ToList());
        }


        private IList<OrganizationDto> GetSubAll(string parentId) {
            var type = typeof(TOrganization);
            string sql = $@"select t1.*,t2.[Name] as [ParentName],concat(t1.[ExtendAttribution]->>'unitCode', ' ', t1.[ExtendAttribution]->>'shorterName') as [DName] from 
                                (select * from {type.PropName()} where [ParentId]=@parentId) t1 
                            left join {type.PropName()} t2 on t2.[Id]=t1.[ParentId]";
            var r = this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { parentId});
            foreach (OrganizationDto or in r) {
                if (or.IsHasChildren)
                    or.Children = GetSubAll(or.Id);
            }
            return r.ToList();
        }
        /// <summary>
        /// 机构类型ID
        /// </summary>
        /// <param name="TypeId"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="IsLoadAll"></param>
        /// <returns></returns>
        public PagedList<OrganizationDto> GetOrganizationOfType(string TypeId, int Page, int Size, bool IsLoadAll)
        {
            var type = typeof(TOrganization);
            string sql;
            if (Page == 0 && Size == 0)
            {
                sql = $@"select t1.* ,t2.[Name] as [ParentName],
                                    concat(t1.[ExtendAttribution]->>'unitCode', ' ', t1.[ExtendAttribution]->>'shorterName') as [DName]
                            from (select * from {type.PropName()} where [TypeId]=@TypeId and [ParentId] is null ) t1 
                        left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id]";
                if (!IsLoadAll)
                {
                    return new PagedList<OrganizationDto>
                    {
                        Index = Page,
                        Size = Size,
                        Total = 0,
                        DataList = this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { TypeId }).ToList()
                    };
                }
                else {
                    var r = this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { TypeId }).ToList();
                    foreach (OrganizationDto or in r) {
                        if (or.IsHasChildren)
                            or.Children = GetSubAll(or.Id);
                    }
                    return new PagedList<OrganizationDto> { DataList = r };
                }

            }
            
            string sql1 = $@"select count(*) from {type.PropName()} where [TypeId]=@TypeId and [ParentId] is null";
            int count = this.DapperRepository.QueryFirstOrDefault<int>(sql1, new { TypeId });
            int Offset = (Page - 1) * Size;
            sql = $@"select t1.* ,t2.[Name] as [ParentName] ,
                        concat(t1.[ExtendAttribution]->>'unitCode', ' ', t1.[ExtendAttribution]->>'shorterName') as [DName]
                        from (select * from {type.PropName()} where [TypeId]=@TypeId and [ParentId] is null 
                        limit @Size offset @Offset) t1 
                        left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id]";
            var data = this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { TypeId, Offset, Size }).ToList();
            if (IsLoadAll)
            {
                foreach (OrganizationDto or in data)
                {
                    if (or.IsHasChildren)
                        or.Children = GetSubAll(or.Id);
                }
            }
            return new PagedList<OrganizationDto> { Index = Page, Size = Size, Total = count, DataList = data };
        }

        /// <summary>
        /// 机构类型ID
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        public IList<TOrganization> GetOrganizationAllOfType(string TypeId)
        {
            var type = typeof(TOrganization);
            string sql = $"select * from {type.PropName()} where [TypeId]=@TypeId and [HierarchyType] = 0";
            return this.DapperRepository.Query(sql, true, new { TypeId }).ToList();
        }

        /// <summary>
        /// 更新字段信息
        /// </summary>
        /// <param name="column">更新字段名</param>
        /// <param name="value">更新字段值</param>
        /// <param name="ConditionColumn">判断字段名</param>
        /// <param name="ConditionValue">判断字段值</param>
        /// <returns></returns>
        public int Update(string column, object value, string ConditionColumn, object ConditionValue)
        {
            var type = typeof(TOrganization);
            string sql = $"update [{type.PropName()}] set [{column}]=@value where [{ConditionColumn}]=@ConditionValue";
            return this.DapperRepository.ExcuteOriCommand(sql, true, new { value, ConditionValue });
        }

        /// <summary>
        /// 查询父级机构列表
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        public IList<OrganizationDto> GetParentList(string OrganizationId)
        {
            var type = typeof(TOrganization);
            string sql = $@"WITH RECURSIVE r AS ( 
                            SELECT {type.Columns()},concat([ExtendAttribution]->>'unitCode', ' ', [ExtendAttribution]->>'shorterName') as [DName] FROM {type.PropName()} WHERE [Id] = @OrganizationId
                            union   ALL 
                            SELECT {type.Columns().Replace("pm_organization", "t")},concat(t.[ExtendAttribution]->>'unitCode', ' ', t.[ExtendAttribution]->>'shorterName') as [DName] FROM {type.PropName()} t, r WHERE t.[Id] = r.[ParentId]
                           ) 
                            SELECT * FROM r ORDER BY [CreateTime]";
            return this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { OrganizationId }).ToList();
        }

        /// <summary>
        /// 获取机构编码
        /// </summary>
        /// <param name="ParentCode"></param>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        public string GetNextOrganizationCode(string ParentCode, string TypeId)
        {
            var typeO = typeof(TOrganization);
            var typeT = typeof(TOrganizationType);
            string sql;
            string Code;
            if (string.IsNullOrEmpty(ParentCode))
            {
                sql = $"select COALESCE(max([Code]), '0') from {typeO.PropName()} where [TypeId]=@TypeId and [ParentId] is null";
                Code = this.DapperRepository.QueryFirstOrDefault<string>(sql, new { TypeId });
                sql = $"select [Code] from {typeT.PropName()} where [Id]=@TypeId";
                string TypeCode = this.DapperRepository.QueryFirstOrDefault<string>(sql, new { TypeId });
                if (Code == "0")
                    return TypeCode + "0000";
                else
                {
                    int code = int.Parse(Code.Substring(6, 3));
                    char h = Code.Substring(5, 1).ToCharArray()[0];
                    if (code == 999)
                    {
                        if (h == '0')
                            h = 'A';
                        else if (h == 'Z')
                            h = 'a';
                        else
                            h = (char)(h + 1);
                        return TypeCode + h.ToString() + "000";
                    }
                    else {
                        return TypeCode + h.ToString() + string.Format("{0:D3}", code + 1);
                    }
                }
            }
            int pLen = ParentCode.Length;
            sql = $"select COALESCE(max([Code]), '0') from {typeO.PropName()} where [ParentCode]=@ParentCode";
            Code = this.DapperRepository.QueryFirstOrDefault<string>(sql, new { ParentCode });
            if (Code == "0")
                return ParentCode + "0000";
            else
            {
                int code = int.Parse(Code.Substring(pLen+1, 3));
                char h = Code.Substring(pLen, 1).ToCharArray()[0];
                if (code == 999)
                {
                    if (h == '0')
                        h = 'A';
                    else if (h == 'Z')
                        h = 'a';
                    else
                        h = (char)(h + 1);
                    return ParentCode + h.ToString() + "000";
                }
                else
                {
                    return ParentCode + h.ToString() + string.Format("{0:D3}", code + 1);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        public OrganizationInfoMoreDto QueryById(string OrganizationId)
        {
            var type = typeof(TOrganization);
            var typeP = typeof(TOrganizationType);
            string sql = $@"select t1.*,t2.[Name] as [ParentName],t3.[IsRelevancy] from (select * from {type.PropName()} where [Id]=@OrganizationId)
                            t1 left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id] left join {typeP.PropName()} t3 on t1.[TypeId]=t3.[Id]";
            return this.DapperRepository.QueryFirstOrDefault<OrganizationInfoMoreDto>(sql, new { OrganizationId});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeId"></param>
        /// <param name="Name"></param>
        /// <param name="SystemId"></param>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        public IList<OrganizationDto> QueryOrganizationsByName(string TypeId, string Name, string SystemId, string OrganizationId)
        {
            var type = typeof(TOrganization);
            var typeP = typeof(TOrganizationType);
            if (string.IsNullOrEmpty(Name))
                Name = string.Empty;
            string sqlOP = $"select * from {type.PropName()} where [TypeId]=@TypeId and [HierarchyType]=0";
            string sqlOS = $"select * from {type.PropName()} where [HierarchyType]=0";
            if (!string.IsNullOrEmpty(OrganizationId))
            {
                sqlOP += $" and [Id] != '{OrganizationId}'";
                sqlOS += $" and [Id] != '{OrganizationId}'";
            }
            if (!string.IsNullOrEmpty(Name))
            {
                sqlOP += $" and [Name] like '%{Name}%'";
                sqlOS += $" and [Name] like '%{Name}%'";
            }
            string sql;
            if (!string.IsNullOrEmpty(TypeId))
            {
                sql = $@"select t1.*,t2.[Name] as ParentName from ( {sqlOP}) t1 left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id] limit 20";
                return this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { TypeId }).ToList();
            }
            else
            {
                sql = $@"select t.*,t3.[Name] as [ParentName] from (select t1.* from ({sqlOS}) t1 
                    left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] where [SystemId]=@SystemId) t left join {type.PropName()} t3 
                    on t.[ParentId]=t3.[Id] limit 20";
                return this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { SystemId }).ToList();
            }
        }

        public IList<OrganizationDto> QueryOrganizationsByDName(string TypeId, string Name, string SystemId, string OrganizationId)
        {
            var type = typeof(TOrganization);
            var typeP = typeof(TOrganizationType);
            if (string.IsNullOrEmpty(Name))
                Name = string.Empty;
            string sqlTmp = $@"create temporary table tmp as select *,concat([ExtendAttribution]->>'unitCode', ' ', [ExtendAttribution]->>'shorterName') as [DName] from {type.PropName()};
                                create index _t on tmp([DName]);";
            string sqlOP = $"select * from tmp where [TypeId]=@TypeId and [HierarchyType]=0";
            string sqlOS = $"select * from tmp where [HierarchyType]=0";
            if (!string.IsNullOrEmpty(OrganizationId))
            {
                sqlOP += $" and [Id] != '{OrganizationId}'";
                sqlOS += $" and [Id] != '{OrganizationId}'";
            }
            if (!string.IsNullOrEmpty(Name))
            {
                sqlOP += $" and [DName] like '%{Name}%'";
                sqlOS += $" and [DName] like '%{Name}%'";
            }
            string sql;
            if (!string.IsNullOrEmpty(TypeId))
            {
                sql = $@"select t1.*,t2.[Name] as ParentName from ( {sqlOP}) t1 left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id] limit 20";
                return this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { TypeId }).ToList();
            }
            else
            {
                sql = $@"select t.*,t3.[Name] as [ParentName] from (select t1.* from ({sqlOS}) t1 
                    left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] where [SystemId]=@SystemId) t left join {type.PropName()} t3 
                    on t.[ParentId]=t3.[Id] limit 20";
                return this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { SystemId }).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeId"></param>
        /// <param name="Name"></param>
        /// <param name="SystemId"></param>
        /// <param name="OrganizationId"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public PagedList<OrganizationDto> QueryOrganizationsByNameAsPage(string TypeId, string Name, string SystemId, string OrganizationId, int Page=0, int Size=0)
        {
            var type = typeof(TOrganization);
            var typeP = typeof(TOrganizationType);
            string sqlOP = $"select * from {type.PropName()} where [TypeId]=@TypeId and [HierarchyType]=0";
            string sqlOS = $"select * from {type.PropName()} where [HierarchyType]=0";
            if (!string.IsNullOrEmpty(OrganizationId))
            {
                sqlOP += $" and [Id] != '{OrganizationId}'";
                sqlOS += $" and [Id] != '{OrganizationId}'";
            }
            if (!string.IsNullOrEmpty(Name))
            {
                sqlOP += $" and [Name] like '%{Name}%'";
                sqlOS += $" and [Name] like '%{Name}%'";
            }
            string sql;
            if (Page == 0 && Size == 0)
            {
                
                if (!string.IsNullOrEmpty(TypeId))
                {
                    sql = $@"select t1.*,t2.[Name] as ParentName from ( {sqlOP}) t1 left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id] limit 20";
                    var data = this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { TypeId }).ToList();
                    return new PagedList<OrganizationDto> { DataList = data };
                }
                else
                {
                    sql = $@"select t.*,t3.[Name] as [ParentName] from (select t1.* from ({sqlOS}) t1 
                    left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] where [SystemId]=@SystemId) t left join {type.PropName()} t3 
                    on t.[ParentId]=t3.[Id] limit 20";
                    var data = this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { SystemId }).ToList();
                    return new PagedList<OrganizationDto> { DataList = data };
                }
            }
            else {
                int offset = (Page - 1) * Size;
                string sqlCount;
                int total;
                if (!string.IsNullOrEmpty(TypeId))
                {
                    sqlCount = $@"select count(*) from ( {sqlOP}) t1 left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id]";
                    total = this.DapperRepository.QueryFirstOrDefault<int>(sqlCount, new { TypeId});
                    sql = $@"select t1.*,t2.[Name] as ParentName from ( {sqlOP}) t1 left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id] limit {Size} offset {offset}";
                    var data = this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { TypeId }).ToList();
                    return new PagedList<OrganizationDto> { DataList = data , Index=Page,Size=Size,Total=total};
                }
                else
                {
                    sqlCount = $@"select count(*) from (select t1.* from ({sqlOS}) t1 
                    left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] where [SystemId]=@SystemId) t left join {type.PropName()} t3 
                    on t.[ParentId]=t3.[Id]";
                    total = this.DapperRepository.QueryFirstOrDefault<int>(sqlCount, new { SystemId });
                    sql = $@"select t.*,t3.[Name] as [ParentName] from (select t1.* from ({sqlOS}) t1 
                    left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] where [SystemId]=@SystemId) t left join {type.PropName()} t3 
                    on t.[ParentId]=t3.[Id] limit {Size} offset {offset}";
                    var data = this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { SystemId }).ToList();
                    return new PagedList<OrganizationDto> { DataList = data, Index=Page,Size=Size,Total=total };
                }
            }
        }

        public PagedList<OrganizationDto> QueryOrganizationsByDNameAsPage(string TypeId, string Name, string SystemId, string OrganizationId, int Page = 0, int Size = 0)
        {
            var type = typeof(TOrganization);
            var typeP = typeof(TOrganizationType);
            string sqlTmp = $@"create temporary table tmp as select *,concat([ExtendAttribution]->>'unitCode', ' ', [ExtendAttribution]->>'shorterName') as [DName] from {type.PropName()};
                                create index _t on tmp([DName]);";
            string sqlOP = $"select * from tmp where [TypeId]=@TypeId and [HierarchyType]=0";
            string sqlOS = $"select * from tmp where [HierarchyType]=0";
            if (!string.IsNullOrEmpty(OrganizationId))
            {
                sqlOP += $" and [Id] != '{OrganizationId}'";
                sqlOS += $" and [Id] != '{OrganizationId}'";
            }
            if (!string.IsNullOrEmpty(Name))
            {
                sqlOP += $" and [DName] like '%{Name}%'";
                sqlOS += $" and [DName] like '%{Name}%'";
            }
            string sql;
            if (Page == 0 && Size == 0)
            {

                if (!string.IsNullOrEmpty(TypeId))
                {
                    sql = $@"{sqlTmp}select t1.*,t2.[Name] as ParentName from ( {sqlOP}) t1 left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id] limit 20";
                    var data = this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { TypeId }).ToList();
                    return new PagedList<OrganizationDto> { DataList = data };
                }
                else
                {
                    sql = $@"{sqlTmp}select t.*,t3.[Name] as [ParentName] from (select t1.* from ({sqlOS}) t1 
                    left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] where [SystemId]=@SystemId) t left join {type.PropName()} t3 
                    on t.[ParentId]=t3.[Id] limit 20";
                    var data = this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { SystemId }).ToList();
                    return new PagedList<OrganizationDto> { DataList = data };
                }
            }
            else
            {
                int offset = (Page - 1) * Size;
                string sqlCount;
                int total;
                if (!string.IsNullOrEmpty(TypeId))
                {
                    sqlCount = $@"{sqlTmp}select count(*) from ( {sqlOP}) t1 left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id]";
                    total = this.DapperRepository.QueryFirstOrDefault<int>(sqlCount, new { TypeId });
                    sql = $@"{sqlTmp}select t1.*,t2.[Name] as ParentName from ( {sqlOP}) t1 left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id] limit {Size} offset {offset}";
                    var data = this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { TypeId }).ToList();
                    return new PagedList<OrganizationDto> { DataList = data, Index = Page, Size = Size, Total = total };
                }
                else
                {
                    sqlCount = $@"{sqlTmp}select count(*) from (select t1.* from ({sqlOS}) t1 
                    left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] where [SystemId]=@SystemId) t left join {type.PropName()} t3 
                    on t.[ParentId]=t3.[Id]";
                    total = this.DapperRepository.QueryFirstOrDefault<int>(sqlCount, new { SystemId });
                    sql = $@"{sqlTmp}select t.*,t3.[Name] as [ParentName] from (select t1.* from ({sqlOS}) t1 
                    left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] where [SystemId]=@SystemId) t left join {type.PropName()} t3 
                    on t.[ParentId]=t3.[Id] limit {Size} offset {offset}";
                    var data = this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { SystemId }).ToList();
                    return new PagedList<OrganizationDto> { DataList = data, Index = Page, Size = Size, Total = total };
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        public int CountName(string Name, string TypeId) {
            var type = typeof(TOrganization);
            string sql = $"select count(*) from {type.PropName()} where [Name]=@Name and [TypeId]=@TypeId and [ParentId] is null";
            return this.DapperRepository.QueryFirstOrDefault<int>(sql, new { Name, TypeId });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GetParentId (string Id){
            var type = typeof(TOrganization);
            string sql = $"select [ParentId] from {type.PropName()} where [Id]=@Id";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { Id });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="TypeId"></param>
        /// <param name="SystemId"></param>
        /// <returns></returns>
        public IList<TOrganization> QueryByWhere(string where, string TypeId, string SystemId)
        {
            var type = typeof(TOrganization);
            var typeP = typeof(TOrganizationType);
            string condition = "";
            if (!TypeId.IsNullOrEmpty())
                condition = "and [TypeId]=@TypeId";
            string condition2 = "";
            if (!SystemId.IsNullOrEmpty())
                condition2 = " where [SystemId]=@SystemId";
            string sql = $@"select t1.*,t2.[Name] as [ParentName],t3.[IsRelevancy] from (select * from {type.PropName()} where [HierarchyType]=0 {condition}
                             { where}) t1 left join { type.PropName()} t2 on t1.[ParentId] = t2.[Id]
                             left join { typeP.PropName()} t3 on t1.[TypeId] = t3.[Id] {condition2}";
            return this.DapperRepository.Query(sql, true, new { TypeId }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="TypeId"></param>
        /// <param name="SystemId"></param>
        /// <returns></returns>
        public PagedList<OrganizationDto> QueryByWhere(string where, int Page, int Size, string TypeId, string SystemId)
        {
            var type = typeof(TOrganization);
            var typeP = typeof(TOrganizationType);
            string condition = "";
            if (!TypeId.IsNullOrEmpty())
                condition += "and [TypeId]=@TypeId";
            string condition2 = "";
            if (!SystemId.IsNullOrEmpty())
                condition2 = " where [SystemId]=@SystemId";
            int Offset = (Page - 1) * Size;
            string sql1 = $@"select count(*) from (select * from {type.PropName()} where [HierarchyType]=0 {condition}
                             {where}) t1 left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id] 
                             left join {typeP.PropName()} t3 on t1.[TypeId]=t3.[Id]  {condition2}";
            string sql2 = $@"select t1.*,t2.[Name] as [ParentName],t3.[IsRelevancy] from (select * from {type.PropName()} where [HierarchyType]=0 {condition}
                             {where}) t1 left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id] 
                             left join {typeP.PropName()} t3 on t1.[TypeId]=t3.[Id]  {condition2} limit @Size offset @Offset";

            var count = this.DapperRepository.QueryFirstOrDefault<int>(sql1, new { TypeId});
            var data = this.DapperRepository.QueryOriCommand<OrganizationDto>(sql2, true, new { TypeId, Size, Offset });
            var list = AutoMapperExtensions.MapTo<OrganizationDto>(data);
            return new PagedList<OrganizationDto> { Index = Page, Size = Size, DataList = list, Total = count };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        public PagedList<OrganizationAbstractDto> GetOrganizationAll(string Code, int Page=0, int Size=0, string OrganizationId=null)
        {
            var type = typeof(TOrganization);
            if (string.IsNullOrEmpty(Code))
            {
                Code = GetCode(OrganizationId);
            }
            string sql2;
            if (Page != 0 && Size != 0)
            {
                string sql1 = $@"select count(*) from {type.PropName()} where [Code] like @Code and [HierarchyType]=0";
                int count = this.DapperRepository.QueryFirstOrDefault<int>(sql1, new { Code = $"{Code}%" });
                int Offset = ((int)Page - 1) * (int)Size;
                sql2 = $@"select [Id],[Code],[Name] from {type.PropName()} where [Code] like @Code and [HierarchyType]=0 order by [CreateTime] limit @Size offset @Offset";
                var data = this.DapperRepository.QueryOriCommand<OrganizationAbstractDto>(sql2, true, new { Code = $"{Code}%", Size, Offset }).ToList();
                return new PagedList<OrganizationAbstractDto> { Index = (int)Page, Size = (int)Size, DataList = data, Total = count };
            }
            else
            {
                sql2 = $@"select [Id],[Code],[Name] from {type.PropName()} where [Code] like @Code and [HierarchyType]=0 order by [CreateTime]";
                var data = this.DapperRepository.QueryOriCommand<OrganizationAbstractDto>(sql2, true, new { Code = $"{Code}%"}).ToList();
                return new PagedList<OrganizationAbstractDto> { Index = 0, Size = 0, DataList = data, Total = 0 };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public PagedList<OrganizationDto> GetOrganizationAllInfo(string Code, int Page, int Size)
        {
            var type = typeof(TOrganization);
            var typeP = typeof(TOrganizationType);
            string sql1 = $@"select count(*) from {type.PropName()} where [Code] like @Code and [HierarchyType]=0";
            int count = this.DapperRepository.QueryFirstOrDefault<int>(sql1, new { Code = $"{Code}%" });
            int Offset = (Page - 1) * Size;
            string sql2 = $@"select t1.*,t2.[Name] as [ParentName],t3.[IsRelevancy] from (select * from {type.PropName()} where [Code] like @Code and [HierarchyType]=0) t1
                            left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id] 
                            left join {typeP.PropName()} t3 on t1.[TypeId]=t3.[Id]
                            order by t1.[CreateTime] limit @Size offset @Offset";
            var data = this.DapperRepository.QueryOriCommand<OrganizationDto>(sql2, true, new { Code = $"{Code}%", Size, Offset }).ToList();
            return new PagedList<OrganizationDto> { Index = Page, Size = Size, DataList = data, Total = count };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParentCode"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IList<OrganizationAbstractDto> GetDepartments(string ParentCode, string Name) {
            var type = typeof(TOrganization);
            string sql;
            if (string.IsNullOrEmpty(ParentCode))
                return new List<OrganizationAbstractDto>();
            if (string.IsNullOrEmpty(Name))
            {
                sql = $"select [Id],[Code],[Name] from {type.PropName()} where [Code] like @ParentCode and [Code] != @ParentCode and [HierarchyType]=1";
                return this.DapperRepository.QueryOriCommand<OrganizationAbstractDto>(sql, true, new { ParentCode = $"{ParentCode}%" }).ToList();
            }
            else
            {
                sql = $"select [Id],[Code],[Name] from {type.PropName()} where [Code] like @ParentCode and [Code] != @ParentCode and [HierarchyType]=1 and " +
                     $"[Name] like @Name";
                return this.DapperRepository.QueryOriCommand<OrganizationAbstractDto>(sql, true, new { ParentCode = $"{ParentCode}%" , Name=$"%{Name}%"}).ToList();
            }
           
        }

        /// <summary>
        /// 查询部门
        /// </summary>
        /// <param name="pageQueryDto"></param>
        /// <returns></returns>
        public PagedList<DepartmentInfoDto> QueryDepartments(DepartmentPageQueryDto pageQueryDto) {
            var type = typeof(TOrganization);
            var typeP = typeof(TOrganizationType);
            int Offset = (pageQueryDto.Page - 1) * pageQueryDto.Size;
            string sql1;
            string sql2;
            if (!pageQueryDto.IsHasQueryConditions) {
                sql1 = $@"select count(*) from (select [TypeId] from {type.PropName()} where [HierarchyType]=1) t1 left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] where [SystemId]=@SystemId";
                sql2 = $@"select    t1.[ExtendAttribution]->>'ParentOrganizationName' as [ParentOrganizationName],
                                    t1.[ExtendAttribution]->>'ParentDepartmentName' as [ParentDepartmentName],
                                    t1.[Id],
                                    t1.[Code],
                                    t1.[Name],
                                    t1.[State],
                                    t1.[Instruction]
                          from (select * from {type.PropName()} where [HierarchyType]=1) t1 left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] where [SystemId]=@SystemId limit @Size offset @Offset";
                int count = this.DapperRepository.QueryFirstOrDefault<int>(sql1, new { pageQueryDto.SystemId});
                var data = this.DapperRepository.QueryOriCommand<DepartmentInfoDto>(sql2, true, new { pageQueryDto.SystemId, pageQueryDto.Size, Offset }).ToList();
                return new PagedList<DepartmentInfoDto> { Index = pageQueryDto.Page, Size = pageQueryDto.Size, Total = count, DataList = data };
            }
            else {
                DepartmentQueryView departmentQueryView = AutoMapperExtensions.MapTo<DepartmentQueryView>(pageQueryDto.QueryDto);
                string w = StringManage.GenerateSqlFromEntity(pageQueryDto.QueryDto, "Name", "ExtendAttribution", "ParentOrganizationCode");
                if (string.IsNullOrEmpty(w))
                    w += " where [HierarchyType]=1";
                else
                    w += " and [HierarchyType]=1";
                sql1 = $@"select count(*) from (select [TypeId] from {type.PropName()} {w}) t1 
                          left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] where [SystemId]='{pageQueryDto.SystemId}'";
                int count = this.DapperRepository.QueryFirstOrDefault<int>(sql1, departmentQueryView);
                sql2 = $@"select    t1.[ExtendAttribution]->>'ParentOrganizationName' as [ParentOrganizationName],
                                    t1.[ExtendAttribution]->>'ParentDepartmentName' as [ParentDepartmentName],
                                    t1.[Id],
                                    t1.[Code],
                                    t1.[Name],
                                    t1.[State],
                                    t1.[Instruction]
                     from (select * from {type.PropName()} {w}) t1 
                     left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] where [SystemId]='{pageQueryDto.SystemId}' limit {pageQueryDto.Size} offset {Offset}";
                var data = this.DapperRepository.QueryOriCommand<DepartmentInfoDto>(sql2, true, departmentQueryView).ToList();
                return new PagedList<DepartmentInfoDto> { Index=pageQueryDto.Page, Size=pageQueryDto.Size,DataList=data, Total=count};

            }
        }

        /// <summary>
        /// 查询岗位
        /// </summary>
        /// <param name="pageQueryDto"></param>
        /// <returns></returns>
        public PagedList<PositionInfoDto> QueryPositions(PositionPageQueryDto pageQueryDto)
        {
            var type = typeof(TOrganization);
            var typeP = typeof(TOrganizationType);
            int Offset = (pageQueryDto.Page - 1) * pageQueryDto.Size;
            string sql1;
            string sql2;
            if (!pageQueryDto.IsHasQueryConditions)
            {
                sql1 = $@"select count(*) from (select [TypeId] from {type.PropName()} where [HierarchyType]=2) t1 left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] where [SystemId]=@SystemId";
                sql2 = $@"select    t1.[ExtendAttribution]->>'ParentOrganizationName' as [ParentOrganizationName],
                                    t1.[ExtendAttribution]->>'ParentDepartmentName' as [ParentDepartmentName],
                                    t1.[ExtendAttribution]->>'Roles' as [Roles],
                                    t1.[Id],
                                    t1.[Code],
                                    t1.[Name],
                                    t1.[State],
                                    t1.[Instruction]
                          from (select * from {type.PropName()} where [HierarchyType]=2) t1 left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] where [SystemId]=@SystemId limit @Size offset @Offset";
                int count = this.DapperRepository.QueryFirstOrDefault<int>(sql1, new { pageQueryDto.SystemId });
                var data = StringManage.PositionViewToPositionInfo(this.DapperRepository.QueryOriCommand<PositionInfoView>(sql2, true, new { pageQueryDto.SystemId, pageQueryDto.Size, Offset }).ToList());
                return new PagedList<PositionInfoDto> { Index = pageQueryDto.Page, Size = pageQueryDto.Size, Total = count, DataList = data };
            }
            else
            {
                PositionQueryView positionQueryView = AutoMapperExtensions.MapTo<PositionQueryView>(pageQueryDto.QueryDto);
                string w = StringManage.GenerateSqlFromEntity(pageQueryDto.QueryDto, "Name", "ExtendAttribution", "ParentOrganizationCode");
                if (string.IsNullOrEmpty(w))
                    w += " where [HierarchyType]=2";
                else
                    w += " and [HierarchyType] = 2";
                sql1 = $@"select count(*) from (select [TypeId] from {type.PropName()} {w}) t1 
                          left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] where [SystemId]='{pageQueryDto.SystemId}'";
                int count = this.DapperRepository.QueryFirstOrDefault<int>(sql1, positionQueryView);
                sql2 = $@"select    t1.[ExtendAttribution]->>'ParentOrganizationName' as [ParentOrganizationName],
                                    t1.[ExtendAttribution]->>'ParentDepartmentName' as [ParentDepartmentName],
                                    t1.[ExtendAttribution]->>'Roles' as [Roles],
                                    t1.[Id],
                                    t1.[Code],
                                    t1.[Name],
                                    t1.[State],
                                    t1.[Instruction]
                     from (select * from {type.PropName()} {w}) t1 
                     left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] where [SystemId]='{pageQueryDto.SystemId}' limit {pageQueryDto.Size} offset {Offset}";
                var data = StringManage.PositionViewToPositionInfo(this.DapperRepository.QueryOriCommand<PositionInfoView>(sql2, true, positionQueryView).ToList());
                return new PagedList<PositionInfoDto> { Index = pageQueryDto.Page, Size = pageQueryDto.Size, DataList = data , Total=count};

            }
        }

        /// <summary>
        /// 通过父级机构和父级部门查询岗位
        /// </summary>
        /// <param name="ParentOrganizationCode"></param>
        /// <param name="ParentDepartmentCode"></param>
        /// <returns></returns>
        public IList<OrganizationAbstractDto> GetPositions(string ParentOrganizationCode, string ParentDepartmentCode) {
            var type = typeof(TOrganization);
            var typeP = typeof(TOrganizationType);
            string Code = string.IsNullOrEmpty(ParentDepartmentCode) ? ParentOrganizationCode : ParentDepartmentCode;
            string sql = $@"select  [Id],
                                    [Code],
                                    [Name]
                     from {type.PropName()} where [ParentCode] = @Code and [HierarchyType]=2";
            return this.DapperRepository.QueryOriCommand<OrganizationAbstractDto>(sql, true, new { Code}).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationCode"></param>
        /// <returns></returns>
        public IList<OrganizationAbstractDto> GetAreas(string OrganizationCode) {
            var type = typeof(TOrganization);
            string sql = $@"select [Id],[Code],[Name],'true' as [IsHasChildren],'true' as [IsHasChildOrganization],[IsArea] from {type.PropName()} where [Code] like @Code and [IsArea]=true and [HierarchyType]=1";
            return this.DapperRepository.QueryOriCommand<OrganizationAbstractDto>(sql, true, new { Code = $"{OrganizationCode}%" }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public PagedList<OrganizationAbstractDto> GetAreas(string OrganizationId, int Page, int Size, string name=null)
        {
            var type = typeof(TOrganization);
            if (name.IsNullOrEmpty())
                name = String.Empty;
            string sql = $@"select [Id],[Code],[Name],'true' as [IsHasChildren],'true' as [IsHasChildOrganization],[IsArea] 
                            from {type.PropName()} where [ParentId] = @OrganizationId and [IsArea]=true and [HierarchyType]=1 and [Name] like @name"; ;
            if (Page == 0 && Size == 0)
            {
                var data = this.DapperRepository.QueryOriCommand<OrganizationAbstractDto>(sql, true, new { OrganizationId, name=$"%{name}%" }).ToList();
                return new PagedList<OrganizationAbstractDto> { DataList = data };
            }
            else
                return this.DapperRepository.PagedList<OrganizationAbstractDto>(sql, Page, Size, new { OrganizationId, name = $"%{name}%" }) as PagedList<OrganizationAbstractDto>;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeId"></param>
        /// <param name="RelationTypeId"></param>
        /// <returns></returns>
        public IList<OrganizationAbstractDto> GetRelationMasterOrganizations(string TypeId, string RelationTypeId) {
            var type = typeof(TOrganization);
            var typeR = typeof(TRelationOrganization);
            string sql = $@"select t4.[Id],t4.[Code],t4.[Name],t4.[IsHasChildren],t4.[IsHasChildOrganization] from 
                        (select distinct [RelationOrganizationId] from 
                        (
                        select distinct [RelationOrganizationId],[OrganizationId] from {typeR.PropName()} t1 left join {type.PropName()} t2 on t1.[RelationOrganizationId]=t2.[Id] 
                        left join {type.PropName()} t3 on t1.[OrganizationId]=t3.[Id]
                        where t2.[TypeId]=@TypeId  and t3.[TypeId]=@RelationTypeId
                        ) tt )t left join {type.PropName()} 
                        t4 on t.[RelationOrganizationId]=t4.[Id] 
                        ";
            var r = this.DapperRepository.QueryOriCommand<OrganizationAbstractDto>(sql, true, new { TypeId, RelationTypeId }).ToList();
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeName"></param>
        /// <param name="SystemCode"></param>
        /// <param name="OrganizationName"></param>
        /// <returns></returns>
        public string GetOrganizationId(string TypeName, string SystemCode, string OrganizationName) {
            var typeS = typeof(TSystem);
            var typeP = typeof(TOrganizationType);
            var typeO = typeof(TOrganization);
            string sql = $@"select t1.[Id] from (select [Id],[TypeId] from {typeO.PropName()} where [Name]=@OrganizationName) t1 left join {typeP.PropName()} t2 on 
                            t1.[TypeId]=t2.[Id] left join {typeS.PropName()} t3 on t2.[SystemId]=t3.[Id] where t2.[Name]=@TypeName and t3.[Code]=@SystemCode";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { TypeName, SystemCode, OrganizationName });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UnitCode"></param>
        /// <returns></returns>
        public OrganizationDto GetOrganizationByUnitCode(string UnitCode) {
            var type = typeof(TOrganization);
            string sql = $@"select * from {type.PropName()} where [ExtendAttribution]->>'unitCode'=@UnitCode";
            return this.DapperRepository.QueryFirstOrDefault<OrganizationDto>(sql, new { UnitCode });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationIds"></param>
        /// <returns></returns>
        public IList<OrganizationDto> GetOrganizations(IList<string> OrganizationIds) {
            var type = typeof(TOrganization);
            var typeP = typeof(TOrganizationType);
            string sql = $@"select t1.*,t2.[IsRelevancy] from {type.PropName()} t1 left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] where t1.[Id] =ANY(@OrganizationIds)";
            return this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { OrganizationIds }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationCode"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        public PagedList<OrganizationAbstractDto> GetAll4sOrganizationsOfCode(string OrganizationCode, int Page, int Size, string OrganizationId) {
            if (string.IsNullOrEmpty(OrganizationCode))
                OrganizationCode = GetCode(OrganizationId);
            var type = typeof(TOrganization);
            var typeR = typeof(TRelationOrganization);
            string sql = $@"select [OrganizationCode] from {typeR.PropName()} where [RelationOrganizationCode]=@OrganizationCode";
            IList<string> rs = this.DapperRepository.QueryOriCommand<string>(sql, true, new { OrganizationCode }).ToList();
            if (Page == 0 && Size == 0)
            {
                if (rs.Count == 0)
                {
                    sql = $@"select * from {type.PropName()} where [Code] like @OrganizationCode";
                    var data = this.DapperRepository.QueryOriCommand<OrganizationAbstractDto>(sql, true, new { OrganizationCode = $"{OrganizationCode}%" }).ToList();
                    return new PagedList<OrganizationAbstractDto> { DataList = data };
                }
                else
                {
                    string join = rs.Join(",");
                    sql = $@"select * from {type.PropName()} t1 inner join 
                    (select regexp_split_to_table(col,',') AS oriCode from (values('{join}')) t(col)) t2
                    on t1.[Code] like oriCode||'%'";
                    var data = this.DapperRepository.QueryOriCommand<OrganizationAbstractDto>(sql).ToList();
                    return new PagedList<OrganizationAbstractDto> { DataList = data };
                }
            }
            else {
                string sqlCount;
                int offset = (Page - 1) * Size;
                int count;
                if (rs.Count == 0)
                {
                    sqlCount = $"select count(*) from {type.PropName()} where [Code] like @OrganizationCode";
                    count = this.DapperRepository.QueryFirstOrDefault<int>(sqlCount, new { OrganizationCode });
                    sql = $@"select * from {type.PropName()} where [Code] like @OrganizationCode order by [CreateTime] limit {Size} offset {offset}";
                    var data = this.DapperRepository.QueryOriCommand<OrganizationAbstractDto>(sql, true, new { OrganizationCode = $"{OrganizationCode}%" }).ToList();
                    return new PagedList<OrganizationAbstractDto> { DataList = data , Total=count, Index=Page, Size=Size};
                }
                else
                {
                    string join = rs.Join(",");
                    sqlCount = $@"select count(*) from {type.PropName()} t1 inner join 
                    (select regexp_split_to_table(col,',') AS oriCode from (values('{join}')) t(col)) t2
                    on t1.[Code] like oriCode||'%'";
                    sql = $@"select * from {type.PropName()} t1 inner join 
                    (select regexp_split_to_table(col,',') AS oriCode from (values('{join}')) t(col)) t2
                    on t1.[Code] like oriCode||'%' limit { Size} offset {offset}" ;
                    count = this.DapperRepository.QueryFirstOrDefault<int>(sqlCount);
                    var data = this.DapperRepository.QueryOriCommand<OrganizationAbstractDto>(sql).ToList();
                    return new PagedList<OrganizationAbstractDto> { DataList = data, Index=Page,Size=Size };
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Name"></param>
        /// <param name="IsSub"></param>
        /// <param name="TypeName"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public PagedList<OrganizationDto> GetUnits(string UserId, string Name, bool IsSub, string TypeName, int Page, int Size)
        {
            var type = typeof(TOrganization);
            var typeU = typeof(TRelationUserOrganization);
            var typeP = typeof(TOrganizationType);
            if (string.IsNullOrEmpty(Name))
                Name = string.Empty;
            string sql = $@"select t2.[Code] from (select [OrganizationId] from {typeU.PropName()} where [UserId]=@UserId) t1
                                left join {type.PropName()} t2 on t1.[OrganizationId]=t2.[Id] 
                                ";
            IList<string> Code = this.DapperRepository.QueryOriCommand<string>(sql, true, new { UserId }).ToList();
            string join = Code.Join(",");
            if (Page == 0 && Size == 0)
            {
                if (IsSub)
                    sql = $@"select t1.* from {type.PropName()} t1 left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] 
                            inner join 
                                (select regexp_split_to_table(col,',') AS oriCode from (values('{join}')) t(col)) c 
                            on t1.[Code] like oriCode||'%'
                            where t1.[Name] like @Name and t2.[Name]=@TypeName";
                else
                    sql = $@"select t1.* from {type.PropName()} t1 left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] 
                            where t1.[Name] like @Name and t1.[Code] =ANY(@Code) and t2.[Name]=@TypeName";
                var data = this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { Name = $"%{Name}%", TypeName, Code }).ToList();
                return new PagedList<OrganizationDto> { DataList = data };
            }
            else
            {
                int Offset = (Page - 1) * Size;
                string sqlCount;
                int Total;
                IList<OrganizationDto> data;
                if (IsSub)
                {
                    sqlCount = $@"select count(*) from {type.PropName()} t1 left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] 
                                inner join 
                                (select regexp_split_to_table(col,',') AS oriCode from (values('{join}')) t(col)) c 
                                on t1.[Code] like oriCode||'%'
                                where t1.[Name] like @Name and t2.[Name]=@TypeName";
                    sql = $@"select t1.* from {type.PropName()} t1 left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] 
                              inner join 
                                (select regexp_split_to_table(col,',') AS oriCode from (values('{join}')) t(col)) c 
                            on t1.[Code] like oriCode||'%'
                            where t1.[Name] like @Name and t2.[Name]=@TypeName order by t1.[CreateTime] limit @Size offset @Offset";
                    Total = this.DapperRepository.QueryFirstOrDefault<int>(sqlCount, new { Name = $"%{Name}%", TypeName });
                    data = this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { Name = $"%{Name}%", TypeName, Size, Offset }).ToList();
                }
                else
                {
                    sqlCount = $@"select count(*) from {type.PropName()} t1 left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] 
                        where t1.[Name] like @Name and t1.[Code] = ANY(@Code) and t2.[Name]=@TypeName";
                    sql = $@"select t1.* from {type.PropName()} t1 left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id]
                        where t1.[Name] like @Name and t1.[Code] = ANY(@Code) and t2.[Name]=@TypeName order by t1.[CreateTime] limit @Size offset @Offset";
                    Total = this.DapperRepository.QueryFirstOrDefault<int>(sqlCount, new { Name = $"%{Name}%", TypeName, Code });
                    data = this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { Name = $"%{Name}%", TypeName, Code, Size, Offset }).ToList();
                }
                return new PagedList<OrganizationDto> { Total = Total, DataList = data, Index = Page, Size = Size };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="SystemCode"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public PagedList<OrganizationDto> GetRelevancyOrganizations(string Name, string SystemCode, int Page, int Size) {
            var type = typeof(TOrganization);
            var typeP = typeof(TOrganizationType);
            var typeS = typeof(TSystem);
            if (Name.IsNullOrEmpty())
                Name = string.Empty;
            string sql = $@"select t1.*,t4.[Name] as [ParentName] from {type.PropName()} t1 left join {typeP.PropName()} t2 on t1.[TypeId]=t2.[Id] 
                            left join {typeS.PropName()} t3 on t2.[SystemId]=t3.[Id] left join {type.PropName()} t4 on t1.[ParentId]=t4.[Id]
                            where t1.[Name] like @Name and t2.[IsRelevancy]=true and t3.[Code]=@SystemCode and t1.[HierarchyType]=0";
            if (Page == 0 && Size == 0)
            {
                return new PagedList<OrganizationDto>
                {
                    DataList = this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new { Name = $"%{Name}%", SystemCode }).ToList()
                };
            }
            else {
                return this.DapperRepository.PagedList<OrganizationDto>(sql, Page, Size, new { Name = $"%{Name}%", SystemCode }) as PagedList<OrganizationDto>;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentCode"></param>
        /// <param name="newCode"></param>
        /// <returns></returns>
        public int UpdateCode(string currentCode, string newCode) {
            var type = typeof(TOrganization);
            var typeR = typeof(TRelationOrganization);
            var typeRp = typeof(TRelationPositionRole);
            var typeRu = typeof(TRelationUserOrganization);
            string sqlUpdateAll = $@"update {type.PropName()} set [Code]=concat('{newCode}', substr([Code], {currentCode.Length + 1})) where [Code] like '{currentCode}%';
                                     update {type.PropName()} set [ParentCode]=concat('{newCode}', substr([ParentCode], {currentCode.Length + 1})) where [ParentCode] like '{currentCode}%';
                                     update {typeRu.PropName()} set [OrganizationCode]=t.[Code] from {type.PropName()} t where [OrganizationId]=t.[Id] and t.[Code] like '{newCode}%';
                                     update {typeRu.PropName()} set [DepartmentCode]=t.[Code] from {type.PropName()} t where [DepartmentId]=t.[Id] and t.[Code] like '{newCode}%';
                                     update {typeRu.PropName()} set [PositionCode]=t.[Code] from {type.PropName()} t where [PositionId]=t.[Id] and t.[Code] like '{newCode}%';
                                     update {typeRp.PropName()} set [PositionCode]=t.[Code] from {type.PropName()} t where [PositionId]=t.[Id] and t.[Code] like '{newCode}%';
                                     update {typeR.PropName()} set [OrganizationCode]=t.[Code] from {type.PropName()} t where [OrganizationId]=t.[Id] and t.[Code] like '{newCode}%';
                                     update {typeR.PropName()} set [RelationOrganizationCode]=t.[Code] from {type.PropName()} t where [RelationOrganizationId]=t.[Id] and t.[Code] like '{newCode}%';
                                     update {typeR.PropName()} set [RelationAreaCode]=t.[Code] from {type.PropName()} t where [RelationAreaId]=t.[Id] and t.[Code] like '{newCode}%';
                                     ";
            return this.DapperRepository.ExcuteOriCommand(sqlUpdateAll);

        }

        public PagedList<OrganizationDto> GetSubOrganizationsNoChildren(string organizationId, int page, int size) {
            var type = typeof(TOrganization);
            string organizationCode = GetCode(organizationId);
            if (organizationCode.IsNullOrEmpty())
                organizationCode = string.Empty;
            string sql = $@"select t1.* ,t2.[Name] as [ParentName],
                                    concat(t1.[ExtendAttribution]->>'unitCode', ' ', t1.[ExtendAttribution]->>'shorterName') as [DName]
                            from (select * from {type.PropName()} where [Code] like @organizationCode and [IsHasChildOrganization]=false ) t1 
                        left join {type.PropName()} t2 on t1.[ParentId]=t2.[Id]";
            if (page == 0 && size == 0)
            {
                return new PagedList<OrganizationDto>
                {
                    DataList = this.DapperRepository.QueryOriCommand<OrganizationDto>(sql, true, new
                    {
                        organizationCode = $"{organizationCode}%"
                    }).ToList()
                };
            }
            else {
                return this.DapperRepository.PagedList<OrganizationDto>(sql, page, size, new
                {
                    organizationCode = $"{organizationCode}%"
                }) as PagedList<OrganizationDto>;
            }



        }

        public IList<ScsjOrganizationDto> GetScsjOrganization(string key) {
            var type = typeof(TOrganization);
            var typeT = typeof(TOrganizationType);
            var typeS = typeof(TSystem);
            string where= "where t3.[Code]='05' and length(t1.[Code]) = ";
            if (key == "area")
                where += "13 and t1.[Name] like '%事业部'";
            if (key == "city")
                where += "17 and t1.[Name] like '%商代处'";
            string sql = $@"select t1.[Id],t1.[Code],t1.[Name],t1.[ExtendAttribution]->>'shorterName' as [ShorterName],
                                   t1.[ExtendAttribution]->>'unitCode' as [UnitCode] from {type.PropName()} t1 
                            left join {typeT.PropName()} t2 on t1.[TypeId]=t2.[Id] 
                            left join {typeS.PropName()} t3 on t3.[Id]=t2.[SystemId] {where} order by [ShorterName]";
            return this.DapperRepository.QueryOriCommand<ScsjOrganizationDto>(sql).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public OrganizationDto GetByDName(string name) {
            var type = typeof(TOrganization);
            string sql = $@"create temporary table tmp as select [Id],[Code],
                            concat([ExtendAttribution]->>'unitCode', ' ', [ExtendAttribution]->>'shorterName') as [DName]
                            from {type.PropName()} where [Code] like '05%';
                            create index c on tmp([DName]);
                            select * from tmp where [DName]=@name";
            return this.DapperRepository.QueryFirstOrDefault<OrganizationDto>(sql, new { name });
        }
    }

}
