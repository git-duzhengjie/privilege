
using Dynamic.Core.Models;
using System.Collections.Generic;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization
{
    /// <summary>
    /// 机构视图
    /// </summary>
    public class OrganizationUpdateDto
    {
        /// <summary>
        /// 机构ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 属性json字符串
        /// </summary>
        public IList<KeyValueItem> Attributions { get; set; }

    }
}
