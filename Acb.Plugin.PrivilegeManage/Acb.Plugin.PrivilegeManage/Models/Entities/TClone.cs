using System;
using CDynamic.Dapper;
using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;

namespace Acb.Plugin.PrivilegeManage.Models.Entities
{
    /// <summary> 克隆表 </summary>
    [Naming("pm_clone")]
    public class TClone:IEntity
    {
        /// <summary> 克隆ID </summary>
        [Key]
        public string Id { get; set; }

        /// <summary> 模块名 </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemId { get; set; }

        /// <summary> 创建时间 </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
