using System;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Privilege
{
    /// <summary>
    /// 
    /// </summary>
    public class PrivilegeDto
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        public string Id { get; set; }

        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OriginalCode { get; set; }

        /// <summary>
        /// 权限名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限组ID
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// 权限描述
        /// </summary>
        public string Instruction { get; set; }

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
