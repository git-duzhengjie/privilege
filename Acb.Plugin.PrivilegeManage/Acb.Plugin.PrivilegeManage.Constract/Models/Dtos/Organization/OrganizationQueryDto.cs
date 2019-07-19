using Dynamic.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization
{
    public class OrganizationQueryDto
    {

        /// <summary>
        /// 查询条件列表
        /// </summary>
        public IList<KeyValueItem> keyValues { get; set; }

        /// <summary>
        /// 机构类型ID
        /// </summary>
        public string TypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SystemId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SystemCode { get; set; }
    }
}
