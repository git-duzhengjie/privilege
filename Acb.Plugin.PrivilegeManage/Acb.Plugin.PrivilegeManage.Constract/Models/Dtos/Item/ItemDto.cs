using System;
using System.Collections.Generic;
using System.Text;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Item
{
    /// <summary>
    /// 
    /// </summary>
    public class ItemDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FrontSystemCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FrontSystemName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<ItemContentDto> Items { get; set; }
    }
}
