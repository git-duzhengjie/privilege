using Acb.Plugin.PrivilegeManage.Models.Entities;
using Acb.MiddleWare.Data.DB;
using Dynamic.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamic.Core.Extensions;
using Acb.Plugin.PrivilegeManage.Models.View.Privilege;

namespace Acb.Plugin.PrivilegeManage.Models.Repository
{
    /// <summary>
    /// 权限组数据库操作
    /// </summary>
    public class RepositoryPrivilegeGroup:DBBase<TPrivilegeGroup>
    {
        ///<summary></summary>
        public RepositoryPrivilegeGroup(DBCfgViewModel dBConfig) : base(dBConfig) { }

        /// <summary> 添加权限组 </summary>
        public int Insert(TPrivilegeGroup privilegeGroup) {
            return this.DapperRepository.Insert(privilegeGroup, excepts: new[] { nameof(TPrivilegeGroup.CreateTime) });
        }

        /// <summary>
        /// 根据系统ID获取权限组
        /// </summary>
        /// <param name="SystemId"></param>
        /// <returns></returns>
        public IList<TPrivilegeGroup> GetPrivilegeGroups(string SystemId) {
            var type = typeof(TPrivilegeGroup);
            string sql = $"select * from {type.PropName()} where [SystemId]=@SystemId order by [Id]";
            return this.DapperRepository.Query(sql, true, new { SystemId}).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SystemId"></param>
        /// <returns></returns>
        public IList<PrivilegeAllView> GetAllPrivileges(string SystemId) {
            var typeG = typeof(TPrivilegeGroup);
            var typeP = typeof(TPrivilege);
            string sql = $@"select t1.[Id] as [GroupId],
                                   t1.[Name] as [GroupName],
                                   t1.[CreateTime] as [GroupCreateTime],
                                   t1.[UpdateTime] as [GroupUpdateTime],
                                   t2.[Id] as [PrivilegeId],
                                   t2.[Code] as [PrivilegeCode],
                                   t2.[OriginalCode],
                                   t2.[Name] as [PrivilegeName],
                                   t2.[Instruction] as [Instruction],
                                   t2.[CreateTime] as [PrivilegeCreateTime],
                                   t2.[UpdateTime] as [PrivilegeUpdateTime]
                            from 
                                   (select * from {typeG.PropName()} where [SystemId]=@SystemId) t1 
                            left join {typeP.PropName()} t2
                            on t1.[Id]=t2.[GroupId] 
                            order by [GroupId]";

            return this.DapperRepository.QueryOriCommand<PrivilegeAllView>(sql, true, new { SystemId }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="privilegeGroupUpdate"></param>
        /// <returns></returns>
        public int Update(TPrivilegeGroupUpdate privilegeGroupUpdate) {
            return this.DapperRepository.Update(privilegeGroupUpdate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public int Delete(string GroupId) {
            return this.DapperRepository.Delete(GroupId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public string GetSystemCode(string GroupId) {
            var type = typeof(TPrivilegeGroup);
            var typeS = typeof(TSystem);
            string sql = $@"select t2.[Code] from (select [SystemId] from {type.PropName()} where [Id]=@GroupId) 
                            t1 left join {typeS.PropName()} t2 on t1.[SystemId]=t2.[Id]";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { GroupId});
        }
    }
}
