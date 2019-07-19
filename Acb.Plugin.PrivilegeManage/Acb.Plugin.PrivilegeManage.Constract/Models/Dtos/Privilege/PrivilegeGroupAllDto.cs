using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Privilege
{
    public class PrivilegeGroupAllDto
    {
        /// <summary>
        /// 权限组ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 权限组名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 对应的权限列表
        /// </summary>
        public IList<PrivilegeDto> Privileges;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
