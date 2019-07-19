using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Privilege;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User
{
    public class UserSelfRolePrivilege
    {
        public IList<RoleDto> Roles { get; set; }

        public IList<PrivilegeDto> Privileges { get; set; }
    }
}
