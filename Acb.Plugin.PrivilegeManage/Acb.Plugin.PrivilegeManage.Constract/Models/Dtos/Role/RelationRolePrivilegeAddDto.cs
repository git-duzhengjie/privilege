using System.Collections.Generic;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role
{
    /// <summary>
    /// 
    /// </summary>
    public class RelationRolePrivilegeAddDto
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 权限ID列表
        /// </summary>
        public IList<string> PrivilegeIds { get; set; }
    }
}
