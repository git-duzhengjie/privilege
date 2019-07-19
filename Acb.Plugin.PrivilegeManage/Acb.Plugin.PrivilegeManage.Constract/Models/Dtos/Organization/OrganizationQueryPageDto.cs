using Dynamic.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization
{
    public class OrganizationQueryPageDto
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页行数
        /// </summary>
        public int Size { get; set; }

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
