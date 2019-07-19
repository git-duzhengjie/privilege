using System.Collections.Generic;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleAddDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 角色名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色状态
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string Instruction { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string JsonItem { get; set; }

        /// <summary>
        /// 菜单id列表
        /// </summary>
        public IList<string> Items { get; set; }
    }
}
