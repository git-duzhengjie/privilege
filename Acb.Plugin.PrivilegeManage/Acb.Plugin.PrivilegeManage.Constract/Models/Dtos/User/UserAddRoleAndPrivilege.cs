using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User
{
    public class UserAddRoleAndPrivilege
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 角色列表
        /// </summary>
        public IList<string> Roles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<string> RoleCodes { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public IList<string> Privileges { get; set; }

        /// <summary>
        /// 权限Code列表
        /// </summary>
        public IList<string> PrivilegeCodes { get; set; }
    }
}
