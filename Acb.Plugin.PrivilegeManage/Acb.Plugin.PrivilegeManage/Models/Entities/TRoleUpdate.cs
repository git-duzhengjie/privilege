using System;
using CDynamic.Dapper;
using CDynamic.Dapper.Attr;
using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;

namespace Acb.Plugin.PrivilegeManage.Models.Entities
{
    /// <summary> 角色表 </summary>
    [Naming("pm_role")]
    public class TRoleUpdate:IEntity
    {
        /// <summary> 角色ID </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OriginalCode { get; set; }

        /// <summary> 角色名 </summary>
        public string Name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Json]
        public string JsonItem { get; set; }
    }
}
