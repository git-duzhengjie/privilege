using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User
{
    public class UserPositionDto
    {
        /// <summary>
        /// 岗位ID
        /// </summary>
        public string PositionId { get; set; }

        /// <summary>
        /// 岗位Code
        /// </summary>
        public string PositionCode { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 部门Code
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public string OrganizationId { get; set; }

        /// <summary>
        /// 机构Code
        /// </summary>
        public string OrganizationCode { get; set; }


        /// <summary>
        /// 用户所属部门名
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 用户所属岗位名
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// 用户机构名
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// 0:普通用户；1：驻店员；2：销售员
        /// </summary>
        public int UserType { get; set; }
    }
}
