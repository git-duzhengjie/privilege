using System.Collections.Generic;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleUpdateDto
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public string Id { get; set; }

        public string Code { get; set; }
        /// <summary>
        /// 角色名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色状态
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string JsonItem { get; set; }

    }
}
