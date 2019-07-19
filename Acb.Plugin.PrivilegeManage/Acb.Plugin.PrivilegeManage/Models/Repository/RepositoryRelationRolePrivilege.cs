using Acb.Plugin.PrivilegeManage.Models.Entities;
using Acb.MiddleWare.Data.DB;
using Dynamic.Core.ViewModel;
using System.Collections.Generic;
using Dynamic.Core.Extensions;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Privilege;
using System.Linq;

namespace Acb.Plugin.PrivilegeManage.Models.Repository
{
    /// <summary>
    /// 角色关联权限数据库操作
    /// </summary>
    public class RepositoryRelationRolePrivilege:DBBase<TRelationRolePrivilege>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dBConfig"></param>
        public RepositoryRelationRolePrivilege(DBCfgViewModel dBConfig) : base(dBConfig) { }

        /// <summary>
        /// 角色关联权限
        /// </summary>
        /// <param name="relationRolePrivileges"></param>
        /// <returns></returns>
        public int Insert(IList<TRelationRolePrivilege> relationRolePrivileges) {
            int count = 0;
            foreach (TRelationRolePrivilege relationRolePrivilege in relationRolePrivileges) {
                count += this.DapperRepository.Insert(relationRolePrivilege, excepts: new[] { nameof(TRelationRolePrivilege.CreateTime) });
            }
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public IList<PrivilegeDto> GetPrivilegeOfRole(string RoleId) {
            var typeR = typeof(TRelationRolePrivilege);
            var typeP = typeof(TPrivilege);
            string sql = $@"select t2.* from (select [PrivilegeId] from {typeR.PropName()} where [RoleId]=@RoleId) t1 left join {typeP.PropName()} t2 
                            on t1.[PrivilegeId]=t2.[Id]";
            return this.DapperRepository.QueryOriCommand<PrivilegeDto>(sql, true, new { RoleId }).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public int Delete(string RoleId) {
            return this.DapperRepository.Delete(RoleId, "RoleId");
        }
    }
}
