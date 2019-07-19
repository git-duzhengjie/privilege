using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Enum
{
    /// <summary>
    /// 
    /// </summary>
    public enum UserTypeEnum
    {
        /// <summary>
        /// 普通登录
        /// </summary>
        Normal=0x0,

        /// <summary>
        /// APP登录
        /// </summary>
        App=0x1,

        /// <summary>
        /// 微信登录
        /// </summary>
        WeChat=0x2,

        /// <summary>
        /// QQ登录
        /// </summary>
        QQ = 0x3,

        /// <summary>
        /// 小程序登录
        /// </summary>
        LittleProgram=0x4,

        /// <summary>
        /// 厂商
        /// </summary>
        Factory=0x5,

        /// <summary>
        /// 经销商
        /// </summary>
        Dealer=0x6
    }
}
