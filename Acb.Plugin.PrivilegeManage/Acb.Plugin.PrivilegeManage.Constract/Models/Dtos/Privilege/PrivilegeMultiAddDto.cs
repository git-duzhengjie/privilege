using System.Collections.Generic;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Privilege
{
    /// <summary>
    /// 
    /// </summary>
    public class PrivilegeMultiAddDto
    {
        /// <summary>
        /// 权限名
        /// </summary>
        public IList<string> Names { get; set; }

        /// <summary>
        /// 权限组ID
        /// </summary>
        public string GroupId { get; set; }
    }
}
