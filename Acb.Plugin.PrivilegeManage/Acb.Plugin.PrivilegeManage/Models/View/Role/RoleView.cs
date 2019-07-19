using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Models.View.Role
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleView
    {
        /// <summary> 角色ID </summary>
        public string Id { get; set; }

        /// <summary> 角色名 </summary>
        public string Name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemId { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Instruction { get; set; }


        public string JsonItem { get; set; }

        /// <summary> 创建时间 </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
