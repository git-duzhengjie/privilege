using System.Collections.Generic;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemJsonItem
    {
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
        public IList<JsonItem> Items { get; set; }
    }
}
