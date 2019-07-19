using Acb.Plugin.PrivilegeManage.Models.Entities;
using Acb.MiddleWare.Data.DB;
using Dynamic.Core.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Dynamic.Core.Extensions;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Privilege;

namespace Acb.Plugin.PrivilegeManage.Models.Repository
{
    /// <summary>
    /// 用户权限关联表操作
    /// </summary>
    public class RepositoryRelationUserPrivilege:DBBase<TRelationUserPrivilege>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dBCfgViewModel"></param>
        public RepositoryRelationUserPrivilege(DBCfgViewModel dBCfgViewModel) : base(dBCfgViewModel) { }

        /// <summary>
        /// 判断用户是否存在该权限
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="PrivilegeId"></param>
        /// <param name="PrivilegeCode"></param>
        /// <returns></returns>
        public long Count(string UserId, string PrivilegeId, string PrivilegeCode) {
            if (!string.IsNullOrEmpty(PrivilegeId))
                return this.DapperRepository.CountWhere("[UserId]=@UserId and [PrivilegeId]=@PrivilegeId", new { UserId, PrivilegeId });
            else if (!string.IsNullOrEmpty(PrivilegeCode))
                return this.DapperRepository.CountWhere("[UserId]=@UserId and [PrivilegeCode]=@PrivilegeCode", new { UserId, PrivilegeCode });
            else
                return 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="relationUserPrivileges"></param>
        /// <returns></returns>
        public int Insert(IList<TRelationUserPrivilege> relationUserPrivileges) {
            int count = 0;
            foreach (TRelationUserPrivilege relationUserPrivilege in relationUserPrivileges) {
                count += this.DapperRepository.Insert(relationUserPrivilege, excepts: new[] { nameof(TRelationUserPrivilege.CreateTime) });
            }
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int Delete(string UserId) {
            return this.DapperRepository.Delete(UserId, "UserId");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<PrivilegeDto> GetPrivileges(string UserId) {
            var type = typeof(TRelationUserPrivilege);
            var typeP = typeof(TPrivilege);
            string sql = $@"select t2.* from (select [PrivilegeId] from {type.PropName()} where [UserId]=@UserId) 
                        t1 left join {typeP.PropName()} t2 on t1.[PrivilegeId]=t2.[Id]";
            return this.DapperRepository.QueryOriCommand<PrivilegeDto>(sql, true, new { UserId }).ToList();
        }
    }
}
