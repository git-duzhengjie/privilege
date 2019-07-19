using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.OrganizationType;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User;
using Dcp.Net.MQ.Rpc.Contract;
using Dynamic.Core;
using Dynamic.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acb.Plugin.PrivilegeManage.Constract
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPrivilegeManageConstract: IDcpApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="passwordDto"></param>
        /// <returns></returns>
        Task<DResult<int>> UpdatePasswordNew(UserUpdatePasswordNewDto passwordDto);

        /// <summary>
        /// 添加机构
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        Task<DResult<OrganizationAbstractDto>> AddOrganization(OrganizationAddAbstractDto organization);
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="vUser">用户详细信息</param>
        /// <returns></returns>
        Task<DResult<string>> AddUser(UserAddDto vUser);

        /// <summary>
        /// 通过用户ID获取用户信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<DResult<UserInfoDto>> GetUserInfo(string UserId);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<DResult<int>> DeleteUser(string UserId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<DResult<PagedList<OrganizationDto>>> GetSubOrganizationsNoChildren(string organizationId, int page = 0, int size = 0);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="userUpdateDto"></param>
        /// <returns></returns>
        Task<DResult<int>> UpdateUser(UserUpdateDto userUpdateDto);

        /// <summary>
        /// 查找子级机构
        /// </summary>
        /// <param name="ParentId">父级机构ID</param>
        /// <returns></returns>
        Task<DResult<IList<OrganizationDto>>> GetSubOrganization(string ParentId);

        /// <summary>
        /// 判断用户是否具有该权限
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="PrivilegeId"></param>
        /// <param name="PrivilegeCode"></param>
        /// <returns></returns>
        Task<DResult<bool>> IsHasPrivilege(string UserId, string PrivilegeId=null, string PrivilegeCode=null);

        /// <summary>
        /// 分页查找子级机构
        /// </summary>
        /// <param name="ParentId">父级机构ID</param>
        /// <param name="Page">页码</param>
        /// <param name="Size">每页行数</param>
        /// <returns></returns>
        Task<DResult<PagedList<OrganizationDto>>> GetSubOrganizationAsPage(string ParentId, int Page=0, int Size=0);

        /// <summary>
        /// 批量获取用户信息
        /// </summary>
        /// <param name="UserIds">用户id列表</param>
        /// <returns></returns>
        Task<DResult<IList<UserInfoDto>>> GetUsers(IList<string> UserIds);

        Task<DResult<IList<ScsjOrganizationDto>>> GetScsjOrganization(string key);

        Task<DResult<PagedList<V2UserInfoDto>>> GetV2BatchUsersAll(string Name, IList<int> Roles, bool IsBeilian, int Page = 0, int Size = 0);

        Task<DResult<PagedList<V2UserInfoDto>>> GetV2BatchUsers(string UserId, string Name, IList<int> Roles, bool IsBeilian, int Page = 0, int Size = 0);

        /// <summary>
        /// 根据机构类型获取机构列表
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        Task<DResult<PagedList<OrganizationDto>>> GetOrganizationOfTypeAsPage(string TypeId, int Page=0, int Size=0, bool IsLoadAll=false);

        /// <summary>
        /// 获取父级机构对应的所有机构信息
        /// </summary>
        /// <param name="Code">机构Code</param>
        /// <returns></returns>
        Task<DResult<PagedList<OrganizationAbstractDto>>> GetAllOrganizationOfCode(string Code, int Page=0, int Size=0, string OrganizationId=null);

        /// <summary>
        /// 通过用户信息查询用户
        /// </summary>
        /// <param name="userQueryDto"></param>
        /// <returns></returns>
        Task<DResult<PagedList<UserInfoDto>>> GetUsersOfInfo(UserQueryPageDto userQueryDto);

        /// <summary>
        /// 分页查找子级机构
        /// </summary>
        /// <param name="ParentId">父级机构ID</param>
        /// <param name="Page">页码</param>
        /// <param name="Size">每页行数</param>
        /// <returns></returns>
        Task<DResult<PagedList<OrganizationDto>>> GetSubOrganizationAsPageAndParent(string ParentId, int Page=0, int Size=0);


        /// <summary>
        /// 根据机构ID获取机构信息
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        Task<DResult<OrganizationInfoMoreDto>> GetOrganizationInfoOfId(string OrganizationId);

        /// <summary>
        /// 根据用户ID获取权限Code
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<DResult<IList<string>>> GetPrivileges(string UserId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<DResult<IList<string>>> GetRoleCodes(string UserId);


        /// <summary>
        /// 根据系统id获取机构类型列表
        /// </summary>
        /// <param name="SystemId"></param>
        /// <param name="SystemCode"></param>
        /// <returns></returns>
        Task<DResult<IList<OrganizationTypeDto>>> GetOrganizationType(string SystemId, string SystemCode);

        /// <summary>
        /// 关联机构
        /// </summary>
        /// <param name="partnerDto"></param>
        /// <returns></returns>
        Task<DResult<OrganizationDto>> CorrelateOrganization(PartnerDto partnerDto);

        /// <summary>
        /// 为用户绑定角色或者权限
        /// </summary>
        /// <param name="userAddRoleAndPrivilege"></param>
        /// <returns></returns>
        Task<DResult<int>> AddPrivilegeRoleForUser(UserAddRoleAndPrivilege userAddRoleAndPrivilege);

        /// <summary>
        /// 通过DName获取机构
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<DResult<OrganizationDto>> GetOrganizationByDName(string name);

        /// <summary>
        /// 批量获取机构信息
        /// </summary>
        /// <param name="OrganizationIds"></param>
        /// <returns></returns>
        Task<DResult<IList<OrganizationDto>>> GetOrganizations(IList<string> OrganizationIds);

        /// <summary>
        /// 获取与超级合伙人关联机构信息
        /// </summary>
        /// <returns></returns>
        Task<DResult<IList<OrganizationForeignDto>>> GetForeignOrganizations(IList<string> OrganizationIds=null);

        /// <summary>
        /// 获取用户关联机构以及它的子级机构
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Name"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        Task<DResult<PagedList<OrganizationAbstractDto>>> QueryUserOrganizationsByName(string UserId, string Name, int Page, int Size);

        /// <summary>
        /// 获取对应的4s店机构
        /// </summary>
        /// <param name="OrganizationCode"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        Task<DResult<PagedList<OrganizationAbstractDto>>> GetAll4sAllOrganizationsOfCode(string OrganizationCode, int Page = 0, int Size = 0, string OrganizationId = null);

        /// <summary>
        /// 获取机构列表
        /// </summary>
        /// <param name="TypeId"></param>
        /// <param name="Name"></param>
        /// <param name="SystemId"></param>
        /// <param name="OrganizationId"></param>
        /// <param name="TypeName"></param>
        /// <param name="SystemCode"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        Task<DResult<PagedList<OrganizationDto>>> GetOrganizationsByNameAsPage(string TypeId, string Name, string SystemId, string OrganizationId, string TypeName = null, string SystemCode = null, int Page = 0, int Size = 0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<DResult<UserInfoDto>> GetMyInfoOfId(string UserId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Name"></param>
        /// <param name="Role"></param>
        /// 1:车主；8：驻店员；4：销售员；2：安装工
        /// <param name="IsBeilian"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        Task<DResult<PagedList<V2UserInfoDto>>> GetUsers(string UserId, string Name, int Role, bool IsBeilian, int Page, int Size);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Role"></param>
        /// 1:车主；8：驻店员；4：销售员；2：安装工
        /// <param name="IsBeilian"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        Task<DResult<PagedList<V2UserInfoDto>>> GetV2UsersAll(string Name, int Role, bool IsBeilian, int Page = 0, int Size = 0);
        /// <summary>
        /// 
        /// 
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Name"></param>
        /// <param name="IsSub"></param>
        /// <param name="TypeName"></param>
        /// 8:4s店；32：保险公司；4：金融公司
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        Task<DResult<PagedList<OrganizationDto>>> GetUnits(string UserId, string Name, bool IsSub, string TypeName, int Page = 0, int Size = 0);

        /// <summary>
        /// 通过电话或者用户名查询与用户关联机构的用户
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Name"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        Task<DResult<PagedList<UserInfoDto>>> QueryUserRelationUserByName(string UserId, string Name, int Page = 0, int Size = 0);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserIds"></param>
        /// <returns></returns>
        Task<DResult<IList<V2UserInfoDto>>> GetV2UserInfos(IList<string> UserIds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        Task<DResult<int>> DeleteOrganization(string OrganizationId);

        /// <summary>
        /// 获取v2区域分组
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<DResult<PagedList<OrganizationAbstractDto>>> GetV2UnitGroup(string name, int Page, int Size);

        /// <summary>
        /// 获取父级机构列表E:\Acb.UPMS\Acb.UPMS\Acb.Plugin.PrivilegeManage\Acb.Plugin.PrivilegeManage\Plugin\PolicyPrivilegeManagePlugin.User.cs
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        Task<DResult<IList<OrganizationDto>>> GetParentList(string OrganizationId);

        /// <summary>
        /// 获取关联机构列表
        /// </summary>
        /// <param name="OrganizationCode"></param>
        /// <param name="Name"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        Task<DResult<PagedList<RelationOrganizationDetailDto>>> GetRelationOrganizations(string OrganizationCode, string Name, int Page, int Size, string OrganizationId = null, string TypeId=null, string RelationTypeId=null, IList<KeyValueItem> Conditions=null);

        /// <summary>
        /// 查询机构
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        Task<DResult<IList<OrganizationDto>>> QueryOrganization(OrganizationQueryDto queryDto);


    }
}
