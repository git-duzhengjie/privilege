using System;
using CDynamic.Dapper;
using CDynamic.Dapper.Attr;
using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;

namespace Acb.Plugin.PrivilegeManage.Models.Entities
{
    /// <summary> 用户表 </summary>
    [Naming("pm_user")]
    public class TUserUpdatePassword:IEntity
    {
        
        /// <summary> 用户ID </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

    }
}
