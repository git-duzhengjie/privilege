using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Privilege;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User
{
    public class PositionRolePrivilege
    {
        /// <summary>
        /// 对应的岗位角色列表
        /// </summary>
        public IList<RoleDto> PositionRoles { get; set; }


        public IList<PrivilegeDto> PositionPrivileges { get; set; }
    }
}
