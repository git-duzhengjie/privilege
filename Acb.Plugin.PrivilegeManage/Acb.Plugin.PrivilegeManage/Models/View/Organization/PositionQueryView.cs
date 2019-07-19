using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Models.View.Organization
{
    /// <summary>
    /// 
    /// </summary>
    public class PositionQueryView
    {
        /// <summary>
        /// 查询名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父级机构编码
        /// </summary>
        public string ParentOrganizationCode { get; set; }

        /// <summary>
        /// 状态码
        /// 
        /// </summary>
        public bool State { get; set; }
    }
}
