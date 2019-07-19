using System;
using CDynamic.Dapper;
using CDynamic.Dapper.Attr;
using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;

namespace Acb.Plugin.PrivilegeManage.Models.Entities
{
    /// <summary> 用户表 </summary>
    [Naming("pm_user")]
    public class TUser:IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public TUser()
        {
            this.ExtendAttribution = "{}";
        }
        /// <summary> 用户ID </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 来源渠道
        /// </summary>
        public int Channel { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string PortraitUrl { get; set; }
        /// <summary>
        /// 第三方用户ID
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 第三方UnionID
        /// </summary>
        public string UnionId { get; set; }

        /// <summary> 账号 </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }

        /// <summary> 用户名 </summary>
        public string Name { get; set; }

        /// <summary> 电话号 </summary>
        public string Telephone { get; set; }

        /// <summary> Email </summary>
        public string Email { get; set; }

        /// <summary> 状态 </summary>
        public bool State { get; set; }

        /// <summary> 说明 </summary>
        public string Instruction { get; set; }

        /// <summary> 创建时间 </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 扩展属性
        /// </summary>
        [Json]
        public string ExtendAttribution { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SystemCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SessionKey { get; set; }
    }
}
