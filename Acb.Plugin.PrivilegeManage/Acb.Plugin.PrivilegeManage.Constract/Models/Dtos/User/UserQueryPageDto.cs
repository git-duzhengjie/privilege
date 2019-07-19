using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserQueryPageDto
    {
        /// <summary>
        /// 用户查询信息类
        /// </summary>
        public UserQueryDto UserQuery { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页个数
        /// </summary>
        public int Size { get; set; }

    }


}
