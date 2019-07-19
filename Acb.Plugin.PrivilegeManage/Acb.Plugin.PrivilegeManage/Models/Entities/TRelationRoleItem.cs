using CDynamic.Dapper;
using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;
using System;

namespace Acb.Plugin.PrivilegeManage.Models.Entities
{
    /// <summary>
    /// 角色关联权限表
    /// </summary>
    [Naming("pm_relation_role_item")]
    public class TRelationRoleItem:IEntity
    {
        /// <summary>
        /// 关联ID
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
}
