using System;
using Acb.MiddleWare.Core.Plugin;
using Microsoft.AspNetCore.Mvc;
using Acb.Plugin.PrivilegeManage.Common;
using System.Collections.Generic;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User;
using Dynamic.Core;
using System.Threading.Tasks;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos;
using Acb.MiddleWare.Core.Authorize;
using Dynamic.Core.Service;
using Acb.Core.Models;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization;
using Dynamic.Core.Extensions;
using Dynamic.Core.Runtime;

namespace Acb.Plugin.PrivilegeManage.Plugin
{

    public partial class PolicyPrivilegeManagePlugin
    {

        
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="vUser">用户详细信息</param>
        /// <returns></returns>
        [HttpPost("AddUser")]
        public async Task<DResult<string>> AddUser([FromBody]UserAddDto vUser)
        {
            try
            {
                return DResult.Succ(businessUser.AddUser(vUser));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<string>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }
        
        /// <summary>
        /// 通过用户信息查询用户
        /// </summary>
        /// <param name="userQueryDto"></param>
        /// <returns></returns>
        [HttpPost("GetUsersOfInfo")]
        public async Task<DResult<PagedList<UserInfoDto>>> GetUsersOfInfo([FromBody]UserQueryPageDto userQueryDto)
        {
            try
            {
                return DResult.Succ(businessUser.GetUsersOfInfo(userQueryDto));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<UserInfoDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 通过用户信息查询用户
        /// </summary>
        /// <param name="userQueryDto"></param>
        /// <returns></returns>
        [HttpPost("QueryUser")]
        public async Task<DResult<PagedList<UserInfoDto>>> QueryUser([FromBody]UserQueryPageDto userQueryDto)
        {
            try
            {
                return DResult.Succ(businessUser.GetUsersOfInfo(userQueryDto));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<UserInfoDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 通过用户ID获取用户信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet("GetUserInfo")]
        public async Task<DResult<UserInfoDto>> GetUserInfo(string UserId)
        {
            try
            {
                return DResult.Succ(businessUser.GetUserInfoOfId(UserId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<UserInfoDto>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteUser")]
        public async Task<DResult<int>> DeleteUser(string UserId)
        {
            try
            {
                return DResult.Succ(businessUser.DeleteUser(UserId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="userUpdateDto"></param>
        /// <returns></returns>
        [HttpPost("UpdateUser")]
        public async Task<DResult<int>> UpdateUser([FromBody]UserUpdateDto userUpdateDto)
        {
            try
            {
                return DResult.Succ(businessUser.UpdateUser(userUpdateDto));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        
        /// <summary>
        /// 判断用户是否拥有该权限
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="PrivilegeId"></param>
        /// <param name="PrivilegeCode"></param>
        /// <returns></returns>
        [HttpGet("IsHasPrivilege")]
        public async Task<DResult<bool>> IsHasPrivilege(string UserId, string PrivilegeId=null, string PrivilegeCode=null)
        {
            try
            {
                return DResult.Succ(businessUser.IsHasPrivilege(UserId, PrivilegeId, PrivilegeCode));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<bool>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }


        /// <summary>
        /// 批量获取用户信息
        /// </summary>
        /// <param name="UserIds">用户id列表</param>
        /// <returns></returns>
        [HttpGet("GetUsers")]
        public async Task<DResult<IList<UserInfoDto>>> GetUsers(IList<string> UserIds)
        {
            try
            {
                return DResult.Succ(businessUser.GetUsers(UserIds));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<UserInfoDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }



        /// <summary>
        /// 获取APP用户列表
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="Key">电话或者用户名关键字</param>
        /// <param name="SystemCode"></param>
        /// <returns></returns>
        [HttpGet("GetAppUsers")]
        public async Task<DResult<PagedList<UserInfoDto>>> GetAppUsers(string Key, int Page=0, int Size=0, string SystemCode="00") {
            try
            {
                return DResult.Succ(businessUser.GetAppUsers(Page, Size, Key, SystemCode));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<UserInfoDto>>(ex.Message, 500);
            }
        }


        /// <summary>
        /// 获取机构对应的用户信息
        /// </summary>
        /// <param name="OrganizationCode">机构ID</param>
        /// <param name="HierarchyType">
        /// 层级类型
        /// 0：机构；1：部门；2：岗位
        /// </param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        [HttpGet("GetUsersOfOrganization")]
        public async Task<DResult<PagedList<UserInfoDto>>> GetUsersOfOrganization(string OrganizationCode, int HierarchyType, int Page, int Size)
        {
            try
            {
                if (HierarchyType == 0)
                    return DResult.Succ(businessUser.GetUsersOfInfo(new UserQueryPageDto
                    {
                        Page = Page,
                        Size = Size,
                        UserQuery = new UserQueryDto
                        {
                            OrganizationCode = OrganizationCode
                        }
                    }));
                else if (HierarchyType == 1)
                    return DResult.Succ(businessUser.GetUsersOfInfo(new UserQueryPageDto
                    {
                        Page = Page,
                        Size = Size,
                        UserQuery = new UserQueryDto
                        {
                            DepartmentCode = OrganizationCode
                        }
                    }));
                else if (HierarchyType == 2)
                    return DResult.Succ(businessUser.GetUsersOfInfo(new UserQueryPageDto
                    {
                        Page = Page,
                        Size = Size,
                        UserQuery = new UserQueryDto
                        {
                            PositionCode = OrganizationCode
                        }
                    }));
                else
                    return DResult.Error<PagedList<UserInfoDto>>("不支持的层级类型。注:0：机构；1：部门；2：岗位", 400);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<UserInfoDto>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 获取用户的角色和权限
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet("GetUserRolePrivilege")]
        public async Task<DResult<UserRolePrivilege>> GetUserRolePrivilege(string UserId) {
            try
            {
                return DResult.Succ(businessUser.GetUserRolePrivilege(UserId));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<UserRolePrivilege>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 为用户绑定角色和权限
        /// </summary>
        /// <param name="userAddRoleAndPrivilege"></param>
        /// <returns></returns>
        [HttpPost("AddPrivilegeRoleForUser")]
        public async Task<DResult<int>> AddPrivilegeRoleForUser([FromBody]UserAddRoleAndPrivilege userAddRoleAndPrivilege) {
            try
            {
                return DResult.Succ(businessUser.AddPrivilegeRoleForUser(userAddRoleAndPrivilege));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 获取需要关联的机构的人员信息
        /// </summary>
        /// <param name="OrganizationCode"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [HttpGet("GetRelationUsersOfOrganizationCode")]
        public async Task<DResult<IList<AbstractDto>>> GetRelationUsersOfOrganizationCode(string OrganizationCode, string UserName) {
            try
            {
                return DResult.Succ(businessUser.GetRelationUsersOfOrganizationCode(OrganizationCode, UserName));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<AbstractDto>>(ex.Message, 500);
            }
        }


        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="passwordDto"></param>
        /// <returns></returns>
        [HttpPost("UpdatePassword")]
        public async Task<DResult<int>> UpdatePassword([FromBody]UserUpdatePasswordDto passwordDto) {
            try
            {
                return DResult.Succ(businessUser.UpdatePassword(passwordDto));

            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="passwordDto"></param>
        /// <returns></returns>
        [HttpPost("UpdatePasswordNew")]
        public async Task<DResult<int>> UpdatePasswordNew([FromBody]UserUpdatePasswordNewDto passwordDto)
        {
            try
            {
                string userId=passwordDto.UserId;
                if (passwordDto.UserId.IsNullOrEmpty())
                {
                    UserSessionInfo session = IocUnity.Get<SessionManager>().GetSessionByRequest(Request);
                    userId = session.id;
                }
                int r = businessUser.UpdatePassword(passwordDto, userId);
                if (r == -1)
                    return DResult.Error<int>("原始密码错误！", 503);
                return DResult.Succ(r);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMyInfo")]
        public async Task<DResult<UserInfoDto>> GetMyInfo() {
            try
            {
                UserSessionInfo session = IocUnity.Get<SessionManager>().GetSessionByRequest(Request);
                return DResult.Succ(businessUser.GetMyInfo(session.id));

            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<UserInfoDto> (ex.Message, 500);
            }
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMyInfoOfId")]
        public async Task<DResult<UserInfoDto>> GetMyInfoOfId(string UserId)
        {
            try
            {
                return DResult.Succ(businessUser.GetMyInfo(UserId));

            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<UserInfoDto>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet("GetPrivileges")]
        public async Task<DResult<IList<string>>> GetPrivileges(string UserId) {
            try
            {
                return DResult.Succ(businessUser.GetPrivileges(UserId));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<string>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet("GetRoleCodes")]
        public async Task<DResult<IList<string>>> GetRoleCodes(string UserId)
        {
            try
            {
                return DResult.Succ(businessUser.GetRoles(UserId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<string>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 获取用户关联的机构以及它的子级机构
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Name"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        [HttpGet("QueryUserOrganizationsByName")]
        public async Task<DResult<PagedList<OrganizationAbstractDto>>> QueryUserOrganizationsByName(string UserId, string Name, int Page, int Size)
        {
            try
            {
                return DResult.Succ(businessUser.QueryUserOrganizationsByName(UserId, Name, Page, Size));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<OrganizationAbstractDto>>(ex.Message, 500);
            }
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="UserId"></param>
        /// <param name="Role"></param>
        /// 1:车主；8：驻店员；4：销售员；2：安装工
        /// <param name="IsBeilian"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        [HttpGet("GetV2Users")]
        public async Task<DResult<PagedList<V2UserInfoDto>>> GetUsers(string UserId, string Name, int Role, bool IsBeilian, int Page=0, int Size=0)
        {
            try
            {
                return DResult.Succ(businessUser.GetUsers(UserId, Name, new List<int> { Role }, IsBeilian, Page, Size));
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
                return DResult.Error<PagedList<V2UserInfoDto>>(e.Message, 500);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="UserId"></param>
        /// <param name="Role"></param>
        /// 1:车主；8：驻店员；4：销售员；2：安装工
        /// <param name="IsBeilian"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        [HttpGet("GetV2BatchUsers")]
        public async Task<DResult<PagedList<V2UserInfoDto>>> GetV2BatchUsers(string UserId, string Name, IList<int> Roles, bool IsBeilian, int Page = 0, int Size = 0)
        {
            try
            {
                return DResult.Succ(businessUser.GetUsers(UserId, Name, Roles, IsBeilian, Page, Size));
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
                return DResult.Error<PagedList<V2UserInfoDto>>(e.Message, 500);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Role"></param>
        /// 1:车主；8：驻店员；4：销售员；2：安装工
        /// <param name="IsBeilian"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        [HttpGet("GetV2UsersAll")]
        public async Task<DResult<PagedList<V2UserInfoDto>>> GetV2UsersAll(string Name, int Role, bool IsBeilian, int Page = 0, int Size = 0)
        {
            try
            {
                return DResult.Succ(businessUser.GetV2UsersAll(Name, new List<int> { Role }, IsBeilian, Page, Size));
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
                return DResult.Error<PagedList<V2UserInfoDto>>(e.Message, 500);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Role"></param>
        /// 1:车主；8：驻店员；4：销售员；2：安装工
        /// <param name="IsBeilian"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        [HttpGet("GetV2BatchUsersAll")]
        public async Task<DResult<PagedList<V2UserInfoDto>>> GetV2BatchUsersAll(string Name, IList<int> Roles, bool IsBeilian, int Page = 0, int Size = 0)
        {
            try
            {
                return DResult.Succ(businessUser.GetV2UsersAll(Name, Roles, IsBeilian, Page, Size));
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
                return DResult.Error<PagedList<V2UserInfoDto>>(e.Message, 500);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Name"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        [HttpGet("QueryUserRelationUserByName")]
        public async Task<DResult<PagedList<UserInfoDto>>> QueryUserRelationUserByName(string UserId, string Name, int Page = 0, int Size = 0) {
            try
            {
                return DResult.Succ(businessUser.QueryUserRelationUserByName(UserId, Name, Page, Size));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<UserInfoDto>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 获取v2用户信息接口
        /// </summary>
        /// <param name="UserIds"></param>
        /// <returns></returns>
        [HttpPost("GetV2UserInfos")]
        public async Task<DResult<IList<V2UserInfoDto>>> GetV2UserInfos([FromBody]IList<string> UserIds) {
            try
            {
                return DResult.Succ(businessUser.GetV2UserInfos(UserIds));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<V2UserInfoDto>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 根据角色code获取用户列表
        /// </summary>
        /// <param name="SystemCode"></param>
        /// <param name="RoleCode"></param>
        /// <param name="Key"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        [HttpGet("GetRoleUsers")]
        public async Task<DResult<PagedList<UserInfoDto>>> GetRoleUsers(string SystemCode, string RoleCode, string Key, int Page, int Size) {
            try
            {
                string UnitCode = GetUnitCode();
                return DResult.Succ(businessUser.GetRoleUsers(SystemCode, RoleCode, Key, UnitCode, Page, Size));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<UserInfoDto>>(ex.Message, 500);
            }
        }

        private string GetUnitCode()
        {
            UserSessionInfo session = IocUnity.Get<SessionManager>().GetSessionByRequest(Request);
            return SerializationUtility.JsonToObject<IList<UserPositionDto>>(session.GetExtionValue("OrganizationIds"))[0].OrganizationCode;
        }

    }

}
