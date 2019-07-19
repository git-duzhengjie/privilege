using System;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Privilege
{
    /// <summary>
    /// 
    /// </summary>
    public class PrivilegeUpdateDto
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        public string Id { get; set; }

        public string Code { get; set; }

        /// <summary>
        /// 权限名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限描述
        /// </summary>
        public string Instruction { get; set; }

    }
}
