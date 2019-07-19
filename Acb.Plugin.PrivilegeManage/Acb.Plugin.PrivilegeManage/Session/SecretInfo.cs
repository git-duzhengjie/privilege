using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Session
{
    /// <summary>
    /// 认证信息
    /// </summary>
    public class SecretInfo
    {
        /// <summary>
        /// 应用id
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 应用密钥
        /// </summary>
        public string AppSecret { get; set; }
    }
}
