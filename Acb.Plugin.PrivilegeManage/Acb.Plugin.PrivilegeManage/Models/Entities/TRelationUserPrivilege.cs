using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;
using System;

namespace Acb.Plugin.PrivilegeManage.Models.Entities
{
    /// <summary>
    /// 
    /// </summary>
    [Naming("pm_relation_user_privilege")]
    public class TRelationUserPrivilege:IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public string PrivilegeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PrivilegeCode { get; set; }

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
