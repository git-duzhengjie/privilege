using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Privilege;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User
{
    public class UserRolePrivilege
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 岗位角色、权限列表
        /// </summary>
        public PositionRolePrivilege RolePrivilegePosition { get; set; }

        /// <summary>
        /// 自身的角色列表
        /// </summary>
        public UserSelfRolePrivilege RolePrivilegeSelf { get; set; }


        /// <summary>
        /// 自身的权限列表
        /// </summary>
        public IList<PrivilegeDto> Privileges{get;set;}
    }
}
