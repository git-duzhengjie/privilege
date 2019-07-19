using System.Collections.Generic;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization
{
    /// <summary>
    /// 添加岗位
    /// </summary>
    public class PositionUpdateDto
    {
        //
        public string Id { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 关联角色列表
        /// </summary>
        public IList<string> Roles { get; set; }

        /// <summary>
        /// 岗位状态
        /// true：启用;false：禁用
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Instruction { get; set; }
    }
}
