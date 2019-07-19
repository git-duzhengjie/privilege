using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Item
{
    /// <summary>
    /// 
    /// </summary>
    public class ItemAddDto
    {
        /// <summary>
        /// 菜单名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 前端系统code
        /// </summary>
        public string FrontSystemCode { get; set; }

        /// <summary>
        /// 前端系统名
        /// </summary>
        public string FrontSystemName { get; set; }

        /// <summary>
        /// 后端系统ID
        /// </summary>
        public string SystemId { get; set; }

    }
}
