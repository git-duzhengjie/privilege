using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Attribution;
using System.Collections.Generic;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.OrganizationType
{
    /// <summary>
    /// 
    /// </summary>
    public class OrganizationTypeAddDto
    {
        /// <summary>
        /// 机构类型名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 机构类型ID
        /// </summary>
        public string SystemId { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// 是否关联区域
        /// </summary>
        public bool IsRelevancy { get; set; }

        /// <summary>
        /// 属性类型
        /// </summary>
        public IList<AttributionTypeAddDto> AttributionTypes { get; set; }
    }
}
