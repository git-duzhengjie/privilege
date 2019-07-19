using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization
{
    public class DepartmentQueryDto
    {
        /// <summary>
        /// 查询名
        /// </summary>
        public string Name{ get; set; }

        /// <summary>
        /// 父级机构编码
        /// </summary>
        public string ParentOrganizationCode { get; set; }

        /// <summary>
        /// 状态码
        /// -1:表示不参与搜索；0：表示为false;1:表示为true
        /// </summary>
        public int State { get; set; }

       
    }
}
