using Acb.Plugin.PrivilegeManage.Models.Entities;
using Acb.MiddleWare.Data.DB;
using Dynamic.Core.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Dynamic.Core.Extensions;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos;

namespace Acb.Plugin.PrivilegeManage.Models.Repository
{
    /// <summary>
    /// 克隆关系操作
    /// </summary>
    public class RepositoryClone : DBBase<TClone>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dBConfig"></param>
        public RepositoryClone(DBCfgViewModel dBConfig) : base(dBConfig) { }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="clone"></param>
        /// <returns></returns>
        public int Insert(TClone clone) {
            return this.DapperRepository.Insert(clone, excepts: new[] { nameof(TRole.CreateTime) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CloneId"></param>
        /// <returns></returns>
        public int Delete(string CloneId) {
            return this.DapperRepository.Delete(CloneId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SystemId"></param>
        /// <returns></returns>
        public IList<TClone> GetClones(string SystemId) {
            var type = typeof(TClone);
            string sql = $"select * from {type.PropName()} where [SystemId]=@SystemId";
            return this.DapperRepository.Query(sql, true, new { SystemId }).ToList();
        }
    }
}
