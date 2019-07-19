using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Models.View.User
{
    public class UserInfoView
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 第三方用户ID
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string PortraitUrl { get; set; }

        /// <summary>
        /// 第三方团体ID
        /// </summary>
        public string UnionId { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 来源渠道
        /// </summary>
        public int Channel { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户电话号
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 用户电子邮箱账号
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// 用户状态
        /// true：启用；false：禁用
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 用户说明
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemId { get; set; }

        /// <summary>
        /// 用户所属机构列表
        /// </summary>
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
        /// 
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExtendAttribution { get; set; }
    }
}
