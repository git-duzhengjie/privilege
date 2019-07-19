using System;
using CDynamic.Dapper;
using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;

namespace Acb.Plugin.PrivilegeManage.Models.Entities
{
    /// <summary> 克隆表 </summary>
    [Naming("pm_relation_organization_foreign")]
    public class TRelationOrganizationForeign:IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>  </summary>
        public string OrganizationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UnionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UnionName { get; set; }

        /// <summary> 创建时间 </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
