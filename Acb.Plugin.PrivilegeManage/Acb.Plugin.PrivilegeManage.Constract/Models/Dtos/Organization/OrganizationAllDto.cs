using System.Collections.Generic;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization
{
    public class OrganizationAllDto
    {
        /// <summary>
        /// 父级单级机构树
        /// </summary>
        public IList<string> parentList { get; set; }

        /// <summary>
        /// 父级全域机构树
        /// </summary>
        public IList<IList<OrganizationDto>> parentAllList { get; set; }
    }
}
