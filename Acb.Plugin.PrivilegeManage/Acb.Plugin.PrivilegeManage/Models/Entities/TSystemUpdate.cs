using System;
using CDynamic.Dapper;
using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;

namespace Acb.Plugin.PrivilegeManage.Models.Entities
{
    /// <summary> 系统表 </summary>
    [Naming("pm_system")]
    public class TSystemUpdate:IEntity
    {
        /// <summary> 系统ID </summary>
        [Key]
        public string Id { get; set; }

        /// <summary> 系统名 </summary>
        public string Name { get; set; }

        /// <summary>
        /// 系统英文名
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// 系统LogoURL
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
