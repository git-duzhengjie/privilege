using Acb.Plugin.PrivilegeManage.Models.Entities;
using Acb.MiddleWare.Data.DB;
using Dynamic.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User;
using AutoMapper;
using Dynamic.Core.Extensions;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role;
using Acb.Plugin.PrivilegeManage.Common;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Privilege;
using Dynamic.Core;
using Acb.Plugin.PrivilegeManage.Models.View.User;

namespace Acb.Plugin.PrivilegeManage.Models.Repository
{
    /// <summary>
    /// 用户所属岗位关系数据库操作
    /// </summary>
    public class RepositoryRelationUserOrganization:DBBase<TRelationUserOrganization>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dBConfig"></param>
        /// <returns></returns>
        public RepositoryRelationUserOrganization(DBCfgViewModel dBConfig) : base(dBConfig){ }

        /// <summary>
        /// 添加用户所属机构
        /// </summary>
        /// <param name="relationUserOrganizations">用户所属机构关系对象</param>
        /// <returns></returns>
        public int Insert(IList<TRelationUserOrganization> relationUserOrganizations) {
            int count = 0;
            foreach (TRelationUserOrganization relationUserOrganization in relationUserOrganizations) {
                count += this.DapperRepository.Insert(relationUserOrganization, excepts: new[] { nameof(TRelationUserOrganization.CreateTime) });
            }
            return count;
        }

        public int Insert(TRelationUserOrganization relationUserOrganization)
        {
            return this.DapperRepository.Insert(relationUserOrganization, excepts: new[] { nameof(TRelationUserOrganization.CreateTime) });
        }

        /// <summary>
        /// 获取机构下对应的用户数
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <param name="HierarchyType"></param>
        /// <returns></returns>
        public long QueryCount(string OrganizationId, int HierarchyType) {
            string Column="";
            if (HierarchyType == 0)
                Column = "OrganizationId";
            if (HierarchyType == 1)
                Column = "DepartmentId";
            if (HierarchyType == 2)
                Column = "PositionId";
            return this.DapperRepository.Count(OrganizationId, Column);
        }

        /// <summary>
        /// 获取机构下对应的用户信息列表
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <param name="HierarchyType"></param>
        /// <returns></returns>
        public IList<UserInfoDto> GetUsersOfOrganization(string OrganizationId, int HierarchyType) {
            string Column = "";
            if (HierarchyType == 0)
                Column = "OrganizationId";
            if (HierarchyType == 1)
                Column = "DepartmentId";
            if (HierarchyType == 2)
                Column = "PositionId";
            string sql = $"select t2.Id,t2.OpenId,t2.UnitId,t2.Account,t2.Name,t2.Telephone,t2.Email,t2.State,t2.Instruction," +
                $"t3.Name as OrganizationName,t4.Name as DepartmentName,t5.Name as PositionName from (select * from [relation_user_organization] " +
                $"where {Column}=@OrganizationId) t1 left join [user] t2 on t1.[UserId]=t2.[Id] left join [Organization] t3 on t1.[OrganizationId]=t3.[Id] left " +
                $"join [Organization] t4 on t1.[DepartmentId]=t4.Id left join [Organization] t5 on t1.[PositionId]=t5.Id";
            var config = new MapperConfiguration(cfg => { });
            var mapper = config.CreateMapper();
            return mapper.Map<IList<UserInfoDto>>(this.DapperRepository.Query(sql, false, new { OrganizationId }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int DeleteUserOrganization(string UserId) {
            return this.DapperRepository.Delete(UserId, "UserId");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<RoleDto> GetUserPositionRoles(string UserId) {
            var type = typeof(TRelationUserOrganization);
            var typeRp = typeof(TRelationPositionRole);
            var typeR = typeof(TRole);
            string sql = $@"select t3.* from (select [PositionId] from {type.PropName()} where [UserId]=@UserId) t1 left join {typeRp.PropName()}
                        t2 on t1.[PositionId]=t2.[PositionId] left join {typeR.PropName()} t3 on t2.[RoleId]=t3.[Id]";
            var data= this.DapperRepository.QueryOriCommand<RoleDto>(sql, true, new { UserId }).ToList();
            return StringManage.RemoveNull(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<PrivilegeDto> GetPositionPrivileges(string UserId) {
            var type = typeof(TRelationUserOrganization);
            var typeRp = typeof(TRelationPositionRole);
            var typeRr = typeof(TRelationRolePrivilege);
            var typeP = typeof(TPrivilege);
            string sql = $@"select t4.* from (select [PositionId] from {type.PropName()} where [UserId]=@UserId) t1 left join {typeRp.PropName()}
                        t2 on t1.[PositionId]=t2.[PositionId] left join {typeRr.PropName()} t3 on t2.[RoleId]=t3.[RoleId] 
                        left join {typeP.PropName()} t4 on t3.[PrivilegeId]=t4.[Id]";
            var data = this.DapperRepository.QueryOriCommand<PrivilegeDto>(sql, true, new { UserId }).ToList();
            return StringManage.RemoveNull(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationCode"></param>
        /// <param name="UserType"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public PagedList<RelationUserInfoDto> GetRelationUsers(string OrganizationCode, int UserType, int Page, int Size) {
            var type = typeof(TRelationUserOrganization);
            var typeU = typeof(TUser);
            var typeO = typeof(TOrganization);
            var typeR = typeof(TRelationOrganization);
            string RelationOrganizationCode = this.DapperRepository.QueryFirstOrDefault<string>(
                $@"select [RelationOrganizationCode] from {typeR.PropName()} where [OrganizationCode]=@OrganizationCode", 
                new { OrganizationCode });
            string sqlCount = $@"select count(*) from {type.PropName()} where [OrganizationCode]=@OrganizationCode and [UserType]=@UserType";
            int Offset = (Page - 1) * Size;
            string sql = $@"select  t1.*,
                                    t2.*,
                                    t4.[Name] as [OrganizationName],
                                    t5.[Name] as [DepartmentName],
                                    t6.[Name] as [PositionName]
                                from 
                                (select [Id] as [RelationId],[OrganizationId],[DepartmentId],[PositionId],[OrganizationCode],[DepartmentCode],[PositionCode],[UserType],[UserId] 
                                from {type.PropName()} where [OrganizationCode]=@OrganizationCode and [UserType]=@UserType) t1
                                left join {typeU.PropName()} t2 on t1.[UserId]=t2.[Id]
                                left join (select [OrganizationId],[DepartmentId],[PositionId],[UserId] from {type.PropName()} where [UserType]=0 and [OrganizationCode]=@RelationOrganizationCode) t3 on t1.[UserId]=t3.[UserId]
                                left join {typeO.PropName()} t4 on t3.[OrganizationId]=t4.[Id] 
                                left join {typeO.PropName()} t5 on t3.[DepartmentId]=t5.[Id] 
                                left join {typeO.PropName()} t6 on t3.[PositionId]=t6.[Id] 
                                order by t1.[UserId] 
                                limit {Size} offset {Offset}";
            int count = this.DapperRepository.QueryFirstOrDefault<int>(sqlCount, new { OrganizationCode, UserType});
            var data = this.DapperRepository.QueryOriCommand<RelationUserInfoView>(sql, true, new { OrganizationCode, UserType, RelationOrganizationCode }).ToList();
            return new PagedList<RelationUserInfoDto> { Index=Page,Size=Size,DataList= StringManage.UserViewToUserDto(data),Total=count};
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RelationId"></param>
        /// <returns></returns>
        public int Delete(string RelationId) {
            return this.DapperRepository.Delete(RelationId);
        }

    }
}
