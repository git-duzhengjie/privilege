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
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization;
using Dynamic.Core;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.OrganizationType;

namespace Acb.Plugin.PrivilegeManage.Models.Repository
{
    /// <summary>
    /// 用户所属岗位关系数据库操作
    /// </summary>
    public class RepositoryRelationOrganization:DBBase<TRelationOrganization>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dBConfig"></param>
        /// <returns></returns>
        public RepositoryRelationOrganization(DBCfgViewModel dBConfig) : base(dBConfig){ }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relationOrganization"></param>
        /// <returns></returns>
        public int Insert(TRelationOrganization relationOrganization) {
            return this.DapperRepository.Insert(relationOrganization, excepts:new[] { nameof(TRelationOrganization.CreateTime) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relationOrganizations"></param>
        /// <returns></returns>
        public int Insert(IList<TRelationOrganization> relationOrganizations)
        {
            int count = 0;
            foreach (TRelationOrganization relationOrganization in relationOrganizations) {
                count += this.DapperRepository.Insert(relationOrganization, excepts: new[] { nameof(TRelationOrganization.CreateTime) });
            }
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        public int Delete(string OrganizationId) {
            return this.DapperRepository.Delete(OrganizationId, "OrganizationId");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RelationCode"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="Name"></param>
        /// <param name="TypeId"></param>
        /// <param name="where"></param>
        /// <param name="RelationTypeId"></param>
        /// <returns></returns>
        public PagedList<RelationOrganizationDetailDto> GetRelationOrganizations(string RelationCode, string Name, int Page, int Size, string TypeId, string RelationTypeId, string where) {
            var typeR = typeof(TRelationOrganization);
            var type = typeof(TOrganization);
            var typeP = typeof(TOrganizationType);
            string condition = "";
            if (Name.IsNotNullOrEmpty())
                condition += $" and t2.[Name] like '%{Name}%'";
            if (TypeId.IsNotNullOrEmpty())
                condition += $" and t2.[TypeId]='{TypeId}'";
            if (RelationTypeId.IsNotNullOrEmpty() && RelationCode.IsNullOrEmpty())
                condition += $" and t5.[TypeId]='{RelationTypeId}'";
            condition = StringManage.RemovePrefix(condition, " and");
            if (condition.IsNotNullOrEmpty())
                condition = "where " + condition;


            if (where.IsNotNullOrEmpty() && condition.IsNullOrEmpty())
            {
                where = StringManage.RemovePrefix(where, " and");
                condition = "where " + where;
            }
            else if (where.IsNotNullOrEmpty() && condition.IsNotNullOrEmpty())
                condition = condition + where;
            if (Page == 0 && Size == 0)
            {
                if (RelationCode.IsNotNullOrEmpty())
                {
                    string sql = $@"select t1.[Id] as [RelationId],t2.*,t3.[IsRelevancy],t4.[Name] as [ParentName] from (select [Id],[OrganizationId] from 
                            {typeR.PropName()} where [RelationAreaCode]=@RelationCode or ([RelationOrganizationCode]=@RelationCode and [RelationAreaCode] is null)) t1 left join
                            {type.PropName()} t2 
                            on t1.[OrganizationId]=t2.[Id] left join {typeP.PropName()} t3 on t2.[TypeId]=t3.[Id] 
                            left join {type.PropName()} t4 on t2.[ParentId]=t4.[Id] {condition}";
                    var r = this.DapperRepository.QueryOriCommand<RelationOrganizationDetailDto>(sql, true, new { RelationCode }).ToList();
                    sql = $@"select tt2.* from (select distinct t1.[RelationAreaId] from  (select [OrganizationId],[RelationAreaId] from 
                            {typeR.PropName()} where [RelationOrganizationCode]=@RelationCode and [RelationAreaCode] is not null) t1 left join {type.PropName()} t2 
                            on t1.[OrganizationId]=t2.[Id] where t2.[TypeId]=@TypeId) t left join {type.PropName()} tt2 
                            on t.[RelationAreaId]=tt2.[Id]";
                    var r2 = this.DapperRepository.QueryOriCommand<RelationOrganizationDetailDto>(sql, true, new { RelationCode, TypeId }).ToList();
                    r.AddRange(r2);
                    return new PagedList<RelationOrganizationDetailDto> { DataList = r };
                }
                else if (RelationCode.IsNullOrEmpty() && RelationTypeId.IsNotNullOrEmpty()) {
                    string sql = $@"select t1.[Id] as [RelationId],t2.*,t3.[IsRelevancy],t4.[Name] as [ParentName] from {typeR.PropName()} t1 left join
                            {type.PropName()} t2 
                            on t1.[OrganizationId]=t2.[Id] left join {typeP.PropName()} t3 on t2.[TypeId]=t3.[Id] 
                            left join {type.PropName()} t4 on t2.[ParentId]=t4.[Id] 
                            left join {type.PropName()} t5 on t1.[RelationOrganizationId]=t2.[Id] {condition}";
                    var r = this.DapperRepository.QueryOriCommand<RelationOrganizationDetailDto>(sql, true, new { RelationTypeId}).ToList();
                    return new PagedList<RelationOrganizationDetailDto> { DataList = r };
                }
            }
            else {
                if (RelationCode.IsNotNullOrEmpty())
                {
                    string sql = $@"select t1.[Id] as [RelationId],t2.*,t3.[IsRelevancy],t4.[Name] as [ParentName] from (select [Id],[OrganizationId] from 
                            {typeR.PropName()} where [RelationAreaCode]=@RelationCode or [RelationOrganizationCode]=@RelationCode) t1 left join 
                            {type.PropName()} t2 
                            on t1.[OrganizationId]=t2.[Id] 
                            left join {typeP.PropName()} t3 on t2.[TypeId]=t3.[Id] 
                            left join {type.PropName()} t4 on t2.[ParentId]=t4.[Id] 
                            {condition}";
                    var data = this.DapperRepository.PagedList<RelationOrganizationDetailDto>(sql, Page, Size, new { RelationCode }) as PagedList<RelationOrganizationDetailDto>;
                    return data;
                }
                else if (RelationCode.IsNullOrEmpty() && RelationTypeId.IsNotNullOrEmpty()) {
                    string sql = $@"select t1.[Id] as [RelationId],t2.*,t3.[IsRelevancy],t4.[Name] as [ParentName] from {typeR.PropName()} t1 left join 
                            {type.PropName()} t2 
                            on t1.[OrganizationId]=t2.[Id] 
                            left join {typeP.PropName()} t3 on t2.[TypeId]=t3.[Id] 
                            left join {type.PropName()} t4 on t2.[ParentId]=t4.[Id] 
                            left join {type.PropName()} t5 on t1.[RelationOrganizationId]=t5.[Id]
                            {condition}";
                    var data = this.DapperRepository.PagedList<RelationOrganizationDetailDto>(sql, Page, Size, new { RelationTypeId}) as PagedList<RelationOrganizationDetailDto>;
                    return data;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RelationIds"></param>
        /// <returns></returns>
        public int DeleteRelationOrganizations(IList<string> RelationIds) {
            var type = typeof(TRelationOrganization);
            string sql = $"delete from {type.PropName()} where [Id]=ANY(@RelationIds)";
            return this.DapperRepository.ExcuteOriCommand(sql, true, new { RelationIds});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        public IList<OrganizationTypeDto> GetRelationMasterOrganizationTypes(string TypeId) {
            var typeR = typeof(TRelationOrganization);
            var typeO = typeof(TOrganization);
            var typeP = typeof(TOrganizationType);
            string sql = $@"select ttt2.* from (select distinct tt2.[TypeId] from (select distinct [RelationOrganizationId] from {typeR.PropName()} t1 
                            left join {typeO.PropName()} t2 on t1.[OrganizationId]=t2.[Id] where [TypeId]=@TypeId) tt1 
                            left join {typeO.PropName()} tt2 on tt1.[RelationOrganizationId]=tt2.[Id]) ttt1 
                            left join {typeP.PropName()} ttt2 on ttt1.[TypeId]=ttt2.[Id]";
            return this.DapperRepository.QueryOriCommand<OrganizationTypeDto>(sql, true, new { TypeId }).ToList();
        }
    }
}
