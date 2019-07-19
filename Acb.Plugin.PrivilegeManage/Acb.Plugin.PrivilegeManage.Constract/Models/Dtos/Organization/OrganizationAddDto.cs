using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Attribution;
using System.Collections.Generic;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization
{
    /// <summary>
    /// 机构视图
    /// </summary>
    public class OrganizationAddDto
    {
        /// <summary>
        /// 父级机构ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 父级机构码
        /// </summary>
        public string ParentCode { get; set; }

        /// <summary>
        /// 机构名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 机构类型ID
        /// </summary>
        public string TypeId { get; set; }

        /// <summary>
        /// 层级类型
        /// </summary>
        public int HierarchyType { get; set; }

        /// <summary>
        /// 是否为区域
        /// </summary>
        public bool IsArea { get; set; }

        /// <summary>
        /// 机构对应的属性
        /// </summary>
        
        public string ExtendAttribution { get; set; }

    }
}
