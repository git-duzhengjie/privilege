using System;
using CDynamic.Dapper;
using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;

namespace Acb.Plugin.PrivilegeManage.Models.Entities
{
    /// <summary> 权限组表 </summary>
    [Naming("pm_privilege_group")]
    public class TPrivilegeGroup : IEntity
    {
        /// <summary> 权限组ID </summary>
        [Key]
        public string Id { get; set; }

        /// <summary> 权限组名 </summary>
        public string Name { get; set; }

        /// <summary> 系统ID </summary>
        public string SystemId { get; set; }

        /// <summary> 创建时间 </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
