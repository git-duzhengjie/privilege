using CDynamic.Dapper;
using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;
using System;

namespace Acb.Plugin.PrivilegeManage.Models.Entities
{
    /// <summary>
    /// 
    /// </summary>
    [Naming("pm_relation_organization")]
    public class TRelationOrganization:IEntity
    {
        /// <summary>
        /// 用户机构关系ID
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public string OrganizationId { get; set; }

        /// <summary>
        /// 关联机构ID
        /// </summary>
        public string RelationOrganizationId { get; set; }


        /// <summary>
        /// 机构ID
        /// </summary>
        public string OrganizationCode { get; set; }

        /// <summary>
        /// 关联机构ID
        /// </summary>
        public string RelationOrganizationCode { get; set; }

        /// <summary>
        /// 关联区域ID
        /// </summary>
        public string RelationAreaId { get; set; }


        /// <summary>
        /// 关联区域CODE
        /// </summary>
        public string RelationAreaCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

    }
}
