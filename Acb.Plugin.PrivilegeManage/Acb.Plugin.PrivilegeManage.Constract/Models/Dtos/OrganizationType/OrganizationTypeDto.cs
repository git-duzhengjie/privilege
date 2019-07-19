using System;
using System.Collections.Generic;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Attribution;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.OrganizationType
{
    /// <summary>
    /// 机构类型
    /// </summary>
    public class OrganizationTypeDto
    {
        /// <summary>
        /// 机构类型ID
        /// </summary>
        public string Id { get; set; }



        public string Code { get; set; }

        /// <summary>
        /// 机构类型名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 机构类型说明
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        public bool IsRelevancy { get; set; }


        public bool IsHasChildren { get; set; }

        /// <summary>
        /// 属性类型列表
        /// </summary>
        public IList<AttributionTypeAddDto> attributionTypes;
    }
}
