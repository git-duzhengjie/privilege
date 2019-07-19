using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Item
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonItemEditDto
    {
        /// <summary>
        /// 父级菜单id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }
    }
}
