using Acb.MiddleWare.Core.Config;
using Acb.Plugin.PrivilegeManage.Session;
using Dynamic.Core.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace Acb.Plugin.PrivilegeManage
{
    /// <summary>  </summary>
    public class PolicyPrivilegeManageConfig : PluginConfigBase
    {
        /// <summary>  </summary>
        public DBCfgViewModel DbConfig { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GetUserInfoUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SuperAuthorityId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SessionTimeOutMillisecond { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<SecretInfo> SecretInfoList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="secretInfo"></param>
        /// <returns></returns>
        public SecretInfo CheckSecretInfo(SecretInfo secretInfo)
        {
            if (secretInfo == null || this.SecretInfoList == null)
            {
                return null;
            }
            return this.SecretInfoList.FirstOrDefault(f => f.AppId == secretInfo.AppId && f.AppSecret == secretInfo.AppSecret);
        }
    }
}
