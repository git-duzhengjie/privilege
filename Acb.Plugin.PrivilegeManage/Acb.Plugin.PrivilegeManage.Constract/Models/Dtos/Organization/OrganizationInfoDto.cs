using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Attribution;
using System.Collections.Generic;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization
{
    /// <summary>
    /// 机构信息
    /// </summary>
    public class OrganizationInfoDto
    {
        /// <summary>
        /// 机构基础信息
        /// </summary>
        public OrganizationDto organization { get; set; }

        /// <summary>
        /// 机构属性列表
        /// </summary>
        public IList<AttributionInfoAbstractDto> attributions { get; set; }
    }
}
