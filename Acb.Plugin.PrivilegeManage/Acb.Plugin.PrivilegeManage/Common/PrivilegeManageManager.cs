
using Dynamic.Core.Service;
using Dynamic.Core.ViewModel;

using Acb.Plugin.PrivilegeManage.Models.Repository;
using Acb.Plugin.PrivilegeManage.Models.Business;
using Acb.Plugin.PrivilegeManage.Models.Entities;

namespace Acb.Plugin.PrivilegeManage.Common
{
    /// <summary> 控件注入 </summary>
    public static class PrivilegeManageManager
    {
        static DBCfgViewModel _DBCfgViewModel = null;
        /// <summary>  </summary>
        public static void Init(DBCfgViewModel dBConfig)
        {
            _DBCfgViewModel = dBConfig;
            RepositorySystem repositorySystem = new RepositorySystem(dBConfig);
            RepositoryOrganizationType repositoryOrganizationType = new RepositoryOrganizationType(dBConfig);
            RepositoryAttributionType repositoryAttributionType = new RepositoryAttributionType(dBConfig);
            RepositoryOrganization repositoryOrganization = new RepositoryOrganization(dBConfig);
            RepositoryPrivilegeGroup repositoryPrivilegeGroup = new RepositoryPrivilegeGroup(dBConfig);
            RepositoryPrivilege repositoryPrivilege = new RepositoryPrivilege(dBConfig);
            RepositoryRelationPositionRole repositoryRelationPositionRole = new RepositoryRelationPositionRole(dBConfig);
            RepositoryUser repositoryUser = new RepositoryUser(dBConfig);
            IocUnity.AddTransient<BusinessSystem>();
            IocUnity.AddTransient<BusinessUser>();
            IocUnity.AddTransient<BusinessRole>();
            IocUnity.AddTransient<BusinessPrivilege>();
            IocUnity.AddTransient<BusinessOrganization>();
            IocUnity.AddTransient<BusinessItem>();
            RepositoryRelationUserOrganization repositoryRelationUserOrganization = new RepositoryRelationUserOrganization(dBConfig);
            RepositoryRole repositoryRole = new RepositoryRole(dBConfig);
            RepositoryRelationRolePrivilege repositoryRelationRolePrivilege = new RepositoryRelationRolePrivilege(dBConfig);
            RepositoryRelationUserPrivilege repositoryRelationUserPrivilege = new RepositoryRelationUserPrivilege(dBConfig);
            RepositoryRelationUserRole repositoryRelationUserRole = new RepositoryRelationUserRole(dBConfig);
            RepositoryRelationOrganization repositoryRelationOrganization = new RepositoryRelationOrganization(dBConfig);
            RepositioryRelationOrganizationForeign repositioryRelationOrganizationForeign = new RepositioryRelationOrganizationForeign(dBConfig);
            RepositoryItem repositoryItem = new RepositoryItem(dBConfig);
            RepositoryItemContent repositoryItemContent = new RepositoryItemContent(dBConfig);
            BaseData<TRelationRoleItem> rBaseData = new BaseData<TRelationRoleItem>(dBConfig);
            IocUnity.AddSingleton(rBaseData);
            IocUnity.AddSingleton(repositoryItem);
            IocUnity.AddSingleton(repositoryItemContent);
            IocUnity.AddSingleton(repositoryRelationOrganization);
            IocUnity.AddSingleton(repositoryRelationUserRole);
            IocUnity.AddSingleton(repositoryRelationUserOrganization);
            IocUnity.AddSingleton(repositorySystem);
            IocUnity.AddSingleton(repositoryOrganizationType);
            IocUnity.AddSingleton(repositoryAttributionType);
            IocUnity.AddSingleton(repositoryOrganization);
            IocUnity.AddSingleton(repositoryPrivilegeGroup);
            IocUnity.AddSingleton(repositoryPrivilege);
            IocUnity.AddSingleton(repositoryRelationPositionRole);
            IocUnity.AddSingleton(repositoryUser);
            IocUnity.AddSingleton(repositoryRole);
            IocUnity.AddSingleton(repositoryRelationRolePrivilege);
            IocUnity.AddSingleton(repositoryRelationUserPrivilege);
            IocUnity.AddSingleton(repositioryRelationOrganizationForeign);
        }
    }
}
