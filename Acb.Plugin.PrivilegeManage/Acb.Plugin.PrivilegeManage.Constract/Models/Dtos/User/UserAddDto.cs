using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Attribution;
using Dynamic.Core.Models;
using System;
using System.Collections.Generic;


namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User
{
    /// <summary>
    /// 用户视图
    /// </summary>
    public class UserAddDto
    {
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
        /// 第三方UnionID
        /// </summary>
        public string UnionId { get; set; }

        /// <summary>
        /// 来源渠道
        /// </summary>
        public int Channel { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 是否启用
        /// true：启用；false：禁用
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 用户说明
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// 用户所属机构列表
        /// </summary>
        public IList<UserPositionDto> OrganizationIds { get; set; }
        
        /// <summary>
        /// 用户扩展属性
        /// </summary>
        public IList<KeyValueItem> ExtendAttribution { get; set; }

        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SystemCode { get; set; }

        /// <summary>
        /// 用户类型
        /// 0：APP用户；1：微信用户；2：QQ用户
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SessionKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsNoEncryptPassword { get; set; }

    }
}
