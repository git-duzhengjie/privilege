using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization
{
    public class DepartmentInfoDto
    {
        /// <summary>
        /// 所属机构
        /// </summary>
        public string ParentOrganizationName { get; set; }

        /// <summary>
        /// 上级部门
        /// </summary>
        public string ParentDepartmentName { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 部门名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Instruction { get; set; }
    }
}
