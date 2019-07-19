using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserUpdatePasswordNewDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserId { get; set; }
    }
}
