using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Models.View.Organization
{
    public class PositionInfoView
    {
        /// <summary>
        /// 所属机构
        /// </summary>
        public string ParentOrganizationName { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public string ParentDepartmentName { get; set; }

        /// <summary>
        /// 岗位ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 岗位编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属角色
        /// </summary>
        public string Roles { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Instruction { get; set; }
    }
}
