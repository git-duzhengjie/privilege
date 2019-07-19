using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Attribution;
using Dynamic.Core.Entities;
using System.Collections.Generic;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization
{
    /// <summary>
    /// 
    /// </summary>
    public class DepartmentAddDto
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 父级机构码
        /// </summary>
        public string ParentOrganizationCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParentOrganizationId { get; set; }

        /// <summary>
        /// 父级部门码
        /// </summary>
        public string ParentDepartmentCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParentDepartmentId { get; set; }

        /// <summary>
        /// 部门名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 是否为区域
        /// </summary>
        public bool IsArea { get; set; }

       
    }
}
