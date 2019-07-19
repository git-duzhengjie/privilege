using Dynamic.Core.Models;
using System.Collections.Generic;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization
{
    public class OrganizationAddAbstractDto
    {
        /// <summary>
        /// 机构ID，用于导数据使用
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 机构类型ID
        /// </summary>
        public string TypeId { get; set; }

        /// <summary>
        /// 机构类型名
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SystemCode { get; set; }

        /// <summary>
        /// 属性json字符串
        /// </summary>
        public IList<KeyValueItem> Attributions { get; set; }
    }
}
