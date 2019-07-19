using Acb.Plugin.PrivilegeManage.Models.Entities;
using Acb.MiddleWare.Data.DB;
using Dynamic.Core.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Dynamic.Core.Extensions;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization;
using Acb.Plugin.PrivilegeManage.Common;

namespace Acb.Plugin.PrivilegeManage.Models.Repository
{
    /// <summary>
    /// 克隆关系操作
    /// </summary>
    public class RepositioryRelationOrganizationForeign : DBBase<TRelationOrganizationForeign>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dBConfig"></param>
        public RepositioryRelationOrganizationForeign(DBCfgViewModel dBConfig) : base(dBConfig) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clone"></param>
        /// <returns></returns>
        public int Insert(TRelationOrganizationForeign clone) {
            return this.DapperRepository.Insert(clone, excepts: new[] { nameof(TRelationOrganizationForeign.CreateTime) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<OrganizationForeignDto> GetForeignDtos(IList<string> OrganizationIds) {
            var type = typeof(TRelationOrganizationForeign);
            if (OrganizationIds == null)
            {
                string sql = $"select * from {type.PropName()}";
                return this.DapperRepository.QueryOriCommand<OrganizationForeignDto>(sql).ToList();
            }
            else {
                string sql = $"select * from {type.PropName()} where [OrganizationId]=ANY(@OrganizationIds)";
                return this.DapperRepository.QueryOriCommand<OrganizationForeignDto>(sql, true, new { OrganizationIds }).ToList();
            }
        }

       
    }
}
