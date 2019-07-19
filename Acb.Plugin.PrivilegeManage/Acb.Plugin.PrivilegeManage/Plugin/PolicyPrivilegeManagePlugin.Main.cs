using Acb.MiddleWare.Core;
using Acb.MiddleWare.Core.Config;
using Acb.MiddleWare.Core.Plugin;
using Microsoft.AspNetCore.Mvc;
using Acb.Plugin.PrivilegeManage.Models.Business;
using Acb.Plugin.PrivilegeManage.Common;
using Acb.Plugin.PrivilegeManage.Constract;
using Dynamic.Core.Service;
using Acb.MiddleWare.Data.Cache;

namespace Acb.Plugin.PrivilegeManage.Plugin
{
    /// <summary>权限管理</summary>
    [Route("api/prvilege")]
    [DynamicWebApi("权限网关", Author = "duzhengjie")]
    public partial class PolicyPrivilegeManagePlugin : WebApiPluginBase<PolicyPrivilegeManageConfig>, IPrivilegeManageConstract
    {
        /// <summary> 插件ID </summary>
        public override string PluginID => "01859BEF-272E-40C1-9395-22CEA90CD9DD";
        private static BusinessSystem businessSystem;
        private static BusinessUser businessUser;
        private static BusinessRole businessRole;
        private static BusinessPrivilege businessPrivilege;
        private static BusinessOrganization businessOrganization;
        private static BusinessItem businessItem;
        /// <summary>
        /// 
        /// </summary>
        public override bool? IsNotAuth => false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        
        /// <summary>  </summary>
        protected override void ConfigLoadAfter(IPluginConfig pluginConfig)
        {
            PolicyPrivilegeManageConfig policyPrivilegeConfig = pluginConfig as PolicyPrivilegeManageConfig;
            PrivilegeManageManager.Init(policyPrivilegeConfig.DbConfig);
            base.ConfigLoadAfter(pluginConfig);
            businessSystem = IocUnity.Get<BusinessSystem>();
            businessUser = IocUnity.Get<BusinessUser>();
            businessRole = IocUnity.Get<BusinessRole>();
            businessPrivilege = IocUnity.Get<BusinessPrivilege>();
            businessOrganization = IocUnity.Get<BusinessOrganization>();
            businessItem = IocUnity.Get<BusinessItem>();
        }

    }

    }
