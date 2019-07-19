using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Models.View.Privilege
{
    /// <summary>
    /// 
    /// </summary>
    public class PrivilegeAllView
    {
        /// <summary>
        /// 权限组ID
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// 权限组名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 权限组创建时间
        /// </summary>
        public DateTime GroupCreateTime { get; set; }

        /// <summary>
        /// 权限组更新时间
        /// </summary>
        public DateTime GroupUpdateTime { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public string PrivilegeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PrivilegeCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OriginalCode { get; set;}

        /// <summary>
        /// 权限名
        /// </summary>
        public string PrivilegeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// 权限创建时间
        /// </summary>
        public DateTime PrivilegeCreateTime { get; set; }

        /// <summary>
        /// 权限更新时间
        /// </summary>
        public DateTime PrivilegeUpdateTime { get; set; }

    }
}
