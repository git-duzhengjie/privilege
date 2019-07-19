using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User
{
    public class RelationOrganizationUserDto
    {

        public string UserId { get; set; }

        /// <summary>
        /// 1:驻店员；2：销售员
        /// </summary>
        public int UserType { get; set; }
    }
}
