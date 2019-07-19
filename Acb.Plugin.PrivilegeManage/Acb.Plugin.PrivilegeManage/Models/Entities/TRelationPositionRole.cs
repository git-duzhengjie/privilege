using CDynamic.Dapper;
using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;
using System;

namespace Acb.Plugin.PrivilegeManage.Models.Entities
{
    /// <summary>
    /// 岗位关联角色表
    /// </summary>
    [Naming("pm_relation_position_role")]
    public class TRelationPositionRole:IEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 岗位ID
        /// </summary>
        public string PositionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PositionCode { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RoleCode { get; set; }
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
