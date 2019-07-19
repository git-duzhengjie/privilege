using Acb.Plugin.PrivilegeManage.Models.Entities;
using Acb.MiddleWare.Data.DB;
using Dynamic.Core.ViewModel;
using Dynamic.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role;

namespace Acb.Plugin.PrivilegeManage.Models.Repository
{
    /// <summary>
    /// 岗位角色关系
    /// </summary>
    public class RepositoryRelationPositionRole:DBBase<TRelationPositionRole>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dBConfig"></param>
        public RepositoryRelationPositionRole(DBCfgViewModel dBConfig) : base(dBConfig) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relationPositionRoles"></param>
        /// <returns></returns>
        public int Insert(IList<TRelationPositionRole> relationPositionRoles) {
            int count = 0;
            foreach (TRelationPositionRole relationPositionRole in relationPositionRoles) {
                count += this.DapperRepository.Insert(relationPositionRole);
            }
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int Delete(object value, string column) {
            return this.DapperRepository.Delete(value, column);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PositionId"></param>
        /// <returns></returns>
        public IList<RoleDto> GetRoles(string PositionId) {
            var type = typeof(TRelationPositionRole);
            var typeR = typeof(TRole);
            string sql = $@"select t2.* from (select [RoleId] from {type.PropName()} where [PostionId]=@PositionId) t1 left join {type.PropName()} t2
                            on t1.[RoleId]=t2.[Id]";
            return this.DapperRepository.QueryOriCommand<RoleDto>(sql, true, new { PositionId }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<RoleDto> GetRolesByUserId(string UserId)
        {
            var typeU = typeof(TRelationUserOrganization);
            var type = typeof(TRelationPositionRole);
            var typeR = typeof(TRole);
            var typeI = typeof(TItem);
            string sql = $@"select t3.*,t4.[SystemJsonItem] from (select [PositionId] from {typeU.PropName()} where [UserId]=@UserId) t1 inner join {type.PropName()}
                            t2 on t1.[PositionId]=t2.[PositionId] inner join {typeR.PropName()} t3 on t2.[RoleId]=t3.[Id] 
                            left join {typeI.PropName()} t4 on t3.[ItemId]=t4.[Id]";
            return this.DapperRepository.QueryOriCommand<RoleDto>(sql, true, new { UserId }).ToList();
        }

    }
}
