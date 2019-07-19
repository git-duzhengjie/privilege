using System.Collections.Generic;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Privilege
{
    /// <summary>
    /// 
    /// </summary>
    public class PrivilegeAddForNewGroupDto
    {
        /// <summary>
        /// 权限名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限描述
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// 权限组名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
    }
}
