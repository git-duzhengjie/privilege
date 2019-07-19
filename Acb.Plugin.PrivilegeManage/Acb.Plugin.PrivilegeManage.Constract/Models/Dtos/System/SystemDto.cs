using System;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.System
{
    public class SystemDto
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 系统Code
        /// </summary>
        public string Code { get; set; }

        /// <summary> 系统名 </summary>
        public string Name { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// LogoUrl
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
