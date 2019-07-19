using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization
{
    public class PositionPageQueryDto
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
        /// 查询条件
        /// </summary>
        public PositionQueryDto QueryDto { get; set; }

        /// <summary>
        /// 系统Id
        /// </summary>
        public string SystemId { get; set; }

        /// <summary>
        /// 是否包含查询条件
        /// </summary>
        public bool IsHasQueryConditions { get; set; }
    }
}
