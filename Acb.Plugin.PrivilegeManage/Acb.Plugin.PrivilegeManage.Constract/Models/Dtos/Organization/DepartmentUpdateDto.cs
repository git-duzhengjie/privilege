using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Attribution;
using Dynamic.Core.Entities;
using System.Collections.Generic;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization
{
    /// <summary>
    /// 
    /// </summary>
    public class DepartmentUpdateDto
    {
        /// <summary>
        /// 部门ID
        /// </summary>
       public string Id { get; set; }

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
