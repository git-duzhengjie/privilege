using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization
{
    public class OrganizationAbstractDto
    {
        public string Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public bool IsHasChildren { get; set; }

        public bool IsHasChildOrganization { get; set; }

        /// <summary>
        /// 层级类型
        /// </summary>
        public int HierarchyType { get; set; }

        public bool IsArea { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParentCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<OrganizationAbstractDto> Children { get; set; }
    }
}
