﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User
{
    public class RelationOrganizationUserAddDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string OrganizationCode { get; set; }

        /// <summary>
        /// 1:驻店员；2：销售员
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 用户列表
        /// </summary>
        public IList<string> Users { get; set; }

    }

}
