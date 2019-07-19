using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User
{
    public class RelationUserPrivilegeDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<string> PrivilegeIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<string> PrivilegeCodes { get; set; }
    }
}
