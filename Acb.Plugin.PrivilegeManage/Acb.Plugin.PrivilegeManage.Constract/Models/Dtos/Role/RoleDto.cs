using System;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleDto
    {
        /// <summary> 角色ID </summary>
        public string Id { get; set; }

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
        /// 系统ID
        /// </summary>
        public string SystemId { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string JsonItem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SystemJsonItem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ItemId { get; set; }

        /// <summary> 创建时间 </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
