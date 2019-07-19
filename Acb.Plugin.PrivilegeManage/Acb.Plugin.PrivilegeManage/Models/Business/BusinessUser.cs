using System;
using Dynamic.Core.Service;
using Acb.Plugin.PrivilegeManage.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using Dynamic.Core.Comm;
using Acb.Plugin.PrivilegeManage.Models.Repository;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User;
using Acb.Plugin.PrivilegeManage.Common;
using Dynamic.Core.Runtime;
using Dynamic.Core;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Privilege;
using Dynamic.Core.Models;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization;
using Acb.Plugin.PrivilegeManage.Constract.Models.Enum;

namespace Acb.Plugin.PrivilegeManage.Models.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessUser
    {
        /// <summary>
        /// 
        /// </summary>
        public BusinessUser() { }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="vUser">用户信息</param>
        /// <returns></returns>
        public string AddUser(UserAddDto vUser) {
            vUser = StringManage.EmptyStringSetNull(vUser);
            if(vUser.Password != null && !vUser.IsNoEncryptPassword)
                vUser.Password = StringManage.EncryptPassword(vUser.Password);
            if (vUser.UserType == 1)
            {
                vUser.OrganizationIds = new List<UserPositionDto>();
                vUser.OrganizationIds.Add(new UserPositionDto
                {
                    OrganizationId = IocUnity.Get<RepositoryOrganization>().GetOrganizationId("非机构", vUser.SystemCode, "APP"),
                    OrganizationName = "APP"
                });
            }
            else if (vUser.UserType == 2 || vUser.UserType == 4)
            {
                vUser.OrganizationIds = new List<UserPositionDto>();
                vUser.OrganizationIds.Add(new UserPositionDto
                {
                    OrganizationId = IocUnity.Get<RepositoryOrganization>().GetOrganizationId("非机构", vUser.SystemCode, "微信"),
                    OrganizationName = "微信"
                });
            }
            else if (vUser.UserType == 3)
            {
                vUser.OrganizationIds = new List<UserPositionDto>();
                vUser.OrganizationIds.Add(new UserPositionDto
                {
                    OrganizationId = IocUnity.Get<RepositoryOrganization>().GetOrganizationId("非机构", vUser.SystemCode, "QQ"),
                    OrganizationName = "QQ"
                });
            }
           
            vUser.OrganizationIds = CompleteUserPositionDto(vUser.OrganizationIds);
            TUser user = AutoMapperExtensions.MapTo<TUser>(vUser);
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            if (vUser.ExtendAttribution != null)
            {
                foreach (KeyValueItem keyValue in vUser.ExtendAttribution)
                {
                    keyValuePairs.Add(keyValue.Key, keyValue.Value);
                }
            }
            user.ExtendAttribution = SerializationUtility.ObjectToJson(keyValuePairs);
            if(string.IsNullOrEmpty(user.Id))
                user.Id = IdentityHelper.NewSequentialGuid().ToString("N");
            if (string.IsNullOrEmpty(user.SystemId) && !string.IsNullOrEmpty(user.SystemCode))
                user.SystemId = IocUnity.Get<RepositorySystem>().GetId(user.SystemCode);
            if (!string.IsNullOrEmpty(user.SystemId) && string.IsNullOrEmpty(user.SystemCode))
                user.SystemCode = IocUnity.Get<RepositorySystem>().GetCode(user.SystemId);
            IList<TRelationUserOrganization> relationUserPositions = new List<TRelationUserOrganization>();
            if (vUser.OrganizationIds != null) { 
                foreach (UserPositionDto od in vUser.OrganizationIds)
                {
                    TRelationUserOrganization relationUserOrganization = AutoMapperExtensions.MapTo<TRelationUserOrganization>(od);
                    relationUserOrganization.Id = IdentityHelper.NewSequentialGuid().ToString("N");
                    relationUserOrganization.UserId = user.Id;
                    if (!string.IsNullOrEmpty(relationUserOrganization.OrganizationId) && !string.IsNullOrEmpty(relationUserOrganization.DepartmentId) && !string.IsNullOrEmpty(relationUserOrganization.PositionId))
                    {
                        relationUserOrganization.OrganizationIdO = relationUserOrganization.PositionId;
                    }
                    else if (!string.IsNullOrEmpty(relationUserOrganization.OrganizationId) && !string.IsNullOrEmpty(relationUserOrganization.DepartmentId) && string.IsNullOrEmpty(relationUserOrganization.PositionId))
                        relationUserOrganization.OrganizationIdO = relationUserOrganization.DepartmentId;
                    else if (!string.IsNullOrEmpty(relationUserOrganization.OrganizationId)  && string.IsNullOrEmpty(relationUserOrganization.DepartmentId) && string.IsNullOrEmpty(relationUserOrganization.PositionId))
                        relationUserOrganization.OrganizationIdO = relationUserOrganization.OrganizationId;
                    else if (!string.IsNullOrEmpty(relationUserOrganization.OrganizationId) && string.IsNullOrEmpty(relationUserOrganization.DepartmentId) && !string.IsNullOrEmpty(relationUserOrganization.PositionId))
                        relationUserOrganization.OrganizationIdO = relationUserOrganization.PositionId;
                    relationUserPositions.Add(relationUserOrganization);
                }
             }
            IocUnity.Get<RepositoryUser>().DapperRepository.ExcuteTransaction(tranc =>
            {
                IocUnity.Get<RepositoryUser>().Insert(user);
                IocUnity.Get<RepositoryRelationUserOrganization>().Insert(relationUserPositions);
            });
            return user.Id;
        }

        private IList<UserPositionDto> CompleteUserPositionDto(IList<UserPositionDto> userPositionDtos) {
            if (userPositionDtos == null)
                return userPositionDtos;
            IList<UserPositionDto> newUserPosition = new List<UserPositionDto>();
            var r = IocUnity.Get<RepositoryOrganization>();
            foreach (UserPositionDto userPositionDto in userPositionDtos) {
                if (string.IsNullOrEmpty(userPositionDto.OrganizationId) && !string.IsNullOrEmpty(userPositionDto.OrganizationCode))
                {
                    userPositionDto.OrganizationId = r.GetId(userPositionDto.OrganizationCode);
                    if (string.IsNullOrEmpty(userPositionDto.OrganizationName))
                        userPositionDto.OrganizationName = r.GetNameById(userPositionDto.OrganizationId);
                }
                if (!string.IsNullOrEmpty(userPositionDto.OrganizationId) && string.IsNullOrEmpty(userPositionDto.OrganizationCode))
                {
                    userPositionDto.OrganizationCode = r.GetCode(userPositionDto.OrganizationId);
                    if (string.IsNullOrEmpty(userPositionDto.OrganizationName))
                        userPositionDto.OrganizationName = r.GetNameByCode(userPositionDto.OrganizationCode);
                }
                if (string.IsNullOrEmpty(userPositionDto.DepartmentId) && !string.IsNullOrEmpty(userPositionDto.DepartmentCode))
                {
                    userPositionDto.DepartmentId = r.GetId(userPositionDto.DepartmentCode);
                    if (string.IsNullOrEmpty(userPositionDto.DepartmentName))
                        userPositionDto.DepartmentName = r.GetNameById(userPositionDto.DepartmentId);
                }
                if (!string.IsNullOrEmpty(userPositionDto.DepartmentId) && string.IsNullOrEmpty(userPositionDto.DepartmentCode))
                {
                    userPositionDto.DepartmentCode = r.GetCode(userPositionDto.DepartmentId);
                    if (string.IsNullOrEmpty(userPositionDto.DepartmentName))
                        userPositionDto.DepartmentName = r.GetNameByCode(userPositionDto.DepartmentCode);
                }
                if (string.IsNullOrEmpty(userPositionDto.PositionId) && !string.IsNullOrEmpty(userPositionDto.PositionCode))
                {
                    userPositionDto.PositionId = r.GetId(userPositionDto.PositionCode);
                    if (string.IsNullOrEmpty(userPositionDto.PositionName))
                        userPositionDto.PositionName = r.GetNameById(userPositionDto.PositionId);
                }
                if (!string.IsNullOrEmpty(userPositionDto.PositionId) && string.IsNullOrEmpty(userPositionDto.PositionCode))
                {
                    userPositionDto.PositionCode = r.GetCode(userPositionDto.PositionId);
                    if (string.IsNullOrEmpty(userPositionDto.PositionName))
                        userPositionDto.PositionName = r.GetNameByCode(userPositionDto.PositionCode);
                }
                newUserPosition.Add(userPositionDto);
            }
            return newUserPosition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userQueryDto"></param>
        /// <returns></returns>
        public PagedList<UserInfoDto> GetUsersOfInfo(UserQueryPageDto userQueryDto)
        {
            var q = Dynamic.Core.Runtime.SerializationUtility.ObjectToJson(userQueryDto);
            string password=userQueryDto.UserQuery.Password;
            if (userQueryDto.UserQuery.Password != null)
                userQueryDto.UserQuery.Password = StringManage.EncryptPassword(password);
            int ?userType = userQueryDto.UserQuery.UserType;
            userQueryDto.UserQuery.UserType = null;
            var data = IocUnity.Get<RepositoryUser>().QueryInfo(userQueryDto.UserQuery, userQueryDto.Page, userQueryDto.Size);
            var d = Dynamic.Core.Runtime.SerializationUtility.ObjectToJson(data);
            if (userQueryDto.UserQuery.Password != null && data.DataList.Count == 0)
            {
                userQueryDto.UserQuery.Password = SecurityHelper.Md5(password);
                data = IocUnity.Get<RepositoryUser>().QueryInfo(userQueryDto.UserQuery, userQueryDto.Page, userQueryDto.Size);
            }
            if (userQueryDto.UserQuery.Password != null && data.DataList.Count == 0)
            {
                userQueryDto.UserQuery.Password = SecurityHelper.Encrypt(password);
                data = IocUnity.Get<RepositoryUser>().QueryInfo(userQueryDto.UserQuery, userQueryDto.Page, userQueryDto.Size);
            }
            if (userType != null && (userType==(int)UserTypeEnum.Factory || userType==(int)UserTypeEnum.Dealer) && data.DataList!=null && data.DataList.Count == 1)
            {
                IocUnity.Get<RepositoryUser>().VerifyUser(data.DataList[0].Id, (int)userType);
            }
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserInfoDto GetUserInfoOfId(string userId) {
            return IocUnity.Get<RepositoryUser>().GetUserInfoOfId(userId).MapTo<UserInfoDto>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public IList<UserInfoDto> GetUsers(IList<string> userIds) {
            return IocUnity.Get<RepositoryUser>().GetUsers(userIds).MapTo<UserInfoDto>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int DeleteUser(string userId) {
            return IocUnity.Get<RepositoryUser>().DeleteUser(userId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userUpdateDto"></param>
        /// <returns></returns>
        public int UpdateUser(UserUpdateDto userUpdateDto) {

            userUpdateDto = StringManage.EmptyStringSetNull(userUpdateDto);

            if (userUpdateDto.UserType == 1)
            {
                userUpdateDto.OrganizationIds = new List<UserPositionDto>
                {
                    new UserPositionDto
                    {
                        OrganizationId = IocUnity.Get<RepositoryOrganization>().GetOrganizationId("非机构",
                            IocUnity.Get<RepositoryUser>().GetUserSystemCode(userUpdateDto.Id), "APP"),
                        OrganizationName = "APP"
                    }
                };
            }
            else if (userUpdateDto.UserType == 2)
            {
                userUpdateDto.OrganizationIds = new List<UserPositionDto>
                {
                    new UserPositionDto
                    {
                        OrganizationId = IocUnity.Get<RepositoryOrganization>().GetOrganizationId("非机构",
                            IocUnity.Get<RepositoryUser>().GetUserSystemCode(userUpdateDto.Id), "微信"),
                        OrganizationName = "微信"
                    }
                };
            }
            else if (userUpdateDto.UserType == 3)
            {
                userUpdateDto.OrganizationIds = new List<UserPositionDto>
                {
                    new UserPositionDto
                    {
                        OrganizationId = IocUnity.Get<RepositoryOrganization>().GetOrganizationId("非机构",
                            IocUnity.Get<RepositoryUser>().GetUserSystemCode(userUpdateDto.Id), "QQ"),
                        OrganizationName = "QQ"
                    }
                };
            }

            userUpdateDto.OrganizationIds = CompleteUserPositionDto(userUpdateDto.OrganizationIds);
            TUserUpdate user = userUpdateDto.MapTo<TUserUpdate>();
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            if (userUpdateDto.ExtendAttribution != null)
            {
                foreach (KeyValueItem keyValue in userUpdateDto.ExtendAttribution)
                {
                    keyValuePairs.Add(keyValue.Key, keyValue.Value);
                }
            }
            user.ExtendAttribution = SerializationUtility.ObjectToJson(keyValuePairs);
            IList<TRelationUserOrganization> relationUserPositions = new List<TRelationUserOrganization>();
            if (userUpdateDto.OrganizationIds != null)
            {
                foreach (UserPositionDto od in userUpdateDto.OrganizationIds)
                {
                    TRelationUserOrganization relationUserOrganization = od.MapTo<TRelationUserOrganization>();
                    relationUserOrganization.Id = IdentityHelper.NewSequentialGuid().ToString("N");
                    relationUserOrganization.UserId = user.Id;
                    if (relationUserOrganization.OrganizationId != null && relationUserOrganization.DepartmentId != null && relationUserOrganization.PositionId != null)
                    {
                        relationUserOrganization.OrganizationIdO = relationUserOrganization.PositionId;
                    }
                    else if (relationUserOrganization.OrganizationId != null && relationUserOrganization.DepartmentId != null && relationUserOrganization.PositionId == null)
                        relationUserOrganization.OrganizationIdO = relationUserOrganization.DepartmentId;
                    else if (relationUserOrganization.OrganizationId != null && relationUserOrganization.DepartmentId == null && relationUserOrganization.PositionId == null)
                        relationUserOrganization.OrganizationIdO = relationUserOrganization.OrganizationId;
                    relationUserPositions.Add(relationUserOrganization);
                }
            }
            int count=0;
            IocUnity.Get<RepositoryUser>().DapperRepository.ExcuteTransaction(tranc =>
            {
                IocUnity.Get<RepositoryRelationUserOrganization>().DeleteUserOrganization(userUpdateDto.Id);
                IocUnity.Get<RepositoryRelationUserOrganization>().Insert(relationUserPositions);
                count = IocUnity.Get<RepositoryUser>().UpdateUser(user);
            });
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="privilegeId"></param>
        /// <param name="privilegeCode"></param>
        /// <returns></returns>
        public bool IsHasPrivilege(string userId, string privilegeId=null, string privilegeCode=null)
        {
            if (IocUnity.Get<RepositoryRelationUserPrivilege>().Count(userId, privilegeId, privilegeCode) == 0 &&
                IocUnity.Get<RepositoryUser>().CountUserPrivilege(userId, privilegeId, privilegeCode) == 0 && 
                IocUnity.Get<RepositoryRelationUserRole>().CountUserPrivilege(userId, privilegeId, privilegeCode) ==0)
                return false;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="key"></param>
        /// <param name="systemCode"></param>
        /// <returns></returns>
        public PagedList<UserInfoDto> GetAppUsers(int page, int size, string key, string systemCode) {
            return IocUnity.Get<RepositoryUser>().GetAppUsers(page, size, key, systemCode, IocUnity.Get<RepositoryOrganization>().GetOrganizationId("非机构", systemCode, "APP"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relationUserRoleDto"></param>
        /// <returns></returns>
        public int AddRoleForUser(RelationUserRoleDto relationUserRoleDto)
        {
            IList<TRelationUserRole> relationUserRoles = new List<TRelationUserRole>();
            foreach (string role in relationUserRoleDto.RoleIds) {
                TRelationUserRole relationUserRole = new TRelationUserRole {
                    Id = IdentityHelper.NewSequentialGuid().ToString("N"),
                    UserId = relationUserRoleDto.UserId,
                    RoleId = role,
                    RoleCode = IocUnity.Get<RepositoryRole>().GetCode(role)
                };
                relationUserRoles.Add(relationUserRole);
            }
            int count = 0;
            IocUnity.Get<RepositoryRelationUserRole>().DapperRepository.ExcuteTransaction(tranc =>
            {
                IocUnity.Get<RepositoryRelationUserRole>().Delete(relationUserRoleDto.UserId);
                count = IocUnity.Get<RepositoryRelationUserRole>().Insert(relationUserRoles);
            });
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relationUserPrivilegeDto"></param>
        /// <returns></returns>
        public int AddPrivilegeRoleForUser(UserAddRoleAndPrivilege relationUserPrivilegeDto) {
            IList<TRelationUserPrivilege> relationUserPrivileges = new List<TRelationUserPrivilege>();
            if (relationUserPrivilegeDto.Privileges != null)
            {
                foreach (string Privilege in relationUserPrivilegeDto.Privileges)
                {
                    TRelationUserPrivilege relationUserPrivilege = new TRelationUserPrivilege
                    {
                        Id = IdentityHelper.NewSequentialGuid().ToString("N"),
                        UserId = relationUserPrivilegeDto.UserId,
                        PrivilegeId = Privilege,
                        PrivilegeCode=IocUnity.Get<RepositoryPrivilege>().GetPrivilegeCode(Privilege)
                    };
                    relationUserPrivileges.Add(relationUserPrivilege);
                }
            }
            if (relationUserPrivilegeDto.PrivilegeCodes != null)
            {
                foreach (string privilege in relationUserPrivilegeDto.PrivilegeCodes)
                {
                    TRelationUserPrivilege relationUserPrivilege = new TRelationUserPrivilege
                    {
                        Id = IdentityHelper.NewSequentialGuid().ToString("N"),
                        UserId = relationUserPrivilegeDto.UserId,
                        PrivilegeId = IocUnity.Get<RepositoryPrivilege>().GetPrivilegeId(privilege),
                        PrivilegeCode = privilege
                    };
                    relationUserPrivileges.Add(relationUserPrivilege);
                }
            }
            IList<TRelationUserRole> relationUserRoles = new List<TRelationUserRole>();
            if (relationUserPrivilegeDto.Roles != null)
            {
                foreach (string role in relationUserPrivilegeDto.Roles)
                {
                    TRelationUserRole relationUserRole = new TRelationUserRole
                    {
                        Id = IdentityHelper.NewSequentialGuid().ToString("N"),
                        UserId = relationUserPrivilegeDto.UserId,
                        RoleId = role,
                        RoleCode=IocUnity.Get<RepositoryRole>().GetCode(role)
                    };
                    relationUserRoles.Add(relationUserRole);
                }
            }
            if (relationUserPrivilegeDto.RoleCodes != null)
            {
                foreach (string role in relationUserPrivilegeDto.RoleCodes)
                {
                    TRelationUserRole relationUserRole = new TRelationUserRole {
                        Id = IdentityHelper.NewSequentialGuid().ToString("N"),
                        UserId = relationUserPrivilegeDto.UserId,
                        RoleCode = role,
                        RoleId = IocUnity.Get<RepositoryRole>().GetId(role)
                    };
                    relationUserRoles.Add(relationUserRole);
                }
            }
            int count = 0;
            IocUnity.Get<RepositoryRelationUserPrivilege>().DapperRepository.ExcuteTransaction(tranc =>
            {
                IocUnity.Get<RepositoryRelationUserPrivilege>().Delete(relationUserPrivilegeDto.UserId);
                count = IocUnity.Get<RepositoryRelationUserPrivilege>().Insert(relationUserPrivileges);
                IocUnity.Get<RepositoryRelationUserRole>().Delete(relationUserPrivilegeDto.UserId);
                count += IocUnity.Get<RepositoryRelationUserRole>().Insert(relationUserRoles);
            });
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserRolePrivilege GetUserRolePrivilege(string userId) {
            PositionRolePrivilege positionRolePrivilege = new PositionRolePrivilege
            {
                PositionRoles = IocUnity.Get<RepositoryRelationUserOrganization>().GetUserPositionRoles(userId),
                PositionPrivileges = IocUnity.Get<RepositoryRelationUserOrganization>().GetPositionPrivileges(userId)
            };
            UserSelfRolePrivilege userSelfRolePrivilege = new UserSelfRolePrivilege {
                Roles = IocUnity.Get<RepositoryRelationUserRole>().GetRoles(userId),
                Privileges=IocUnity.Get<RepositoryRelationUserRole>().GetPrivileges(userId)
            };
            return  new UserRolePrivilege
            {
                Id = userId,
                Name = IocUnity.Get<RepositoryUser>().GetName(userId),
                RolePrivilegePosition = positionRolePrivilege,
                Privileges = IocUnity.Get<RepositoryRelationUserPrivilege>().GetPrivileges(userId),
                RolePrivilegeSelf= userSelfRolePrivilege
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizationCode"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IList<AbstractDto> GetRelationUsersOfOrganizationCode(string organizationCode, string userName) {
            return IocUnity.Get<RepositoryUser>().GetRelationOrganizationUsers(organizationCode, userName);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserInfoLoginDto Login(string account, string password) {
            if(string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
                throw new Exception("账号或者密码错误");
            var r = GetUsersOfInfo(new UserQueryPageDto
            {
                Page = 1,
                Size = 1,
                UserQuery = new UserQueryDto
                {
                    Account = account,
                    State = 1,
                    SystemId = "d6cf967def18c2213b7908d63b35045b"
                }
            });
            if (r.DataList.Count == 0) {
                throw new Exception("账号不存在");
            }
            var r2 = GetUsersOfInfo(new UserQueryPageDto
            {
                Page = 1,
                Size = 1,
                UserQuery = new UserQueryDto
                {
                    Account = account,
                    Password = password,
                    State = 1,
                    SystemId = "d6cf967def18c2213b7908d63b35045b"
                }
            });
            if (r2.DataList.Count != 0)
            {
                string token = IdentityHelper.NewSequentialGuid().ToString("N");
                UserInfoLoginDto userInfoLoginDto = r.DataList[0].MapTo<UserInfoLoginDto>();
                userInfoLoginDto.Token = token;
                return userInfoLoginDto;
            }
            else
                throw new Exception("密码错误");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="passwordDto"></param>
        /// <returns></returns>
        public int UpdatePassword(UserUpdatePasswordDto passwordDto) {
            TUserUpdatePassword updatePassword = passwordDto.MapTo<TUserUpdatePassword>();
            updatePassword.Id = passwordDto.UserId;
            updatePassword.UpdateTime = DateTime.Now;
            updatePassword.Password = StringManage.EncryptPassword(updatePassword.Password);
            return IocUnity.Get<RepositoryUser>().Update(updatePassword);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="passwordDto"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int UpdatePassword(UserUpdatePasswordNewDto passwordDto, string userId)
        {
            PagedList<UserInfoDto> pagedList = GetUsersOfInfo(new UserQueryPageDto
            {
                UserQuery = new UserQueryDto
                {
                    Id = userId,
                    Password = passwordDto.OldPassword,
                    State=1
                },
                Page = 1,
                Size = 1
            });
            if (pagedList.Total == 0) {
                return -1;
            }
            TUserUpdatePassword updatePassword = new TUserUpdatePassword {
                Id=userId,
                Password=StringManage.EncryptPassword(passwordDto.NewPassword),
                UpdateTime=DateTime.Now
            };
            return IocUnity.Get<RepositoryUser>().Update(updatePassword);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserInfoDto GetMyInfo(string userId) {
            List<PrivilegeDto> privileges = new List<PrivilegeDto>();
            List<RoleDto> roles = new List<RoleDto>();
            privileges.AddRange(IocUnity.Get<RepositoryRelationUserOrganization>().GetPositionPrivileges(userId));
            privileges.AddRange(IocUnity.Get<RepositoryRelationUserRole>().GetPrivileges(userId));
            privileges.AddRange(IocUnity.Get<RepositoryRelationUserPrivilege>().GetPrivileges(userId));
            roles.AddRange(IocUnity.Get<RepositoryRelationPositionRole>().GetRolesByUserId(userId));
            roles.AddRange(IocUnity.Get<RepositoryRelationUserRole>().GetRoles(userId));
            IList<string> prs = new List<string>();
            IList<string> prsCode = new List<string>();
            IList<string> rc = new List<string>();
            foreach (PrivilegeDto privilege in privileges) {
                prs.Add(privilege.Id);
                prsCode.Add(privilege.Code);
            }
            foreach (RoleDto role in roles) {
                rc.Add(role.Code);
            }
            UserInfoDto userInfoDto = IocUnity.Get<RepositoryUser>().GetUserInfoOfId(userId);
            if (userInfoDto == null)
                return null;
            userInfoDto.Privileges = prs;
            userInfoDto.PrivilegesCode = prsCode;
            IList<string> jsons = IocUnity.Get<RepositoryRelationUserRole>().GetUserJsons(userId);
            List<JsonItem> jsonItems = ToJsonItems(jsons);
            List<SystemJsonItem> systemJsonItems = ToSystemJsonItems(jsons);
            userInfoDto.JsonItems = MergeJsonItems(jsonItems);
            //List<SystemJsonItem> systemJsonItems = IocUnity.Get<RepositoryRelationUserRole>().GetUserJsonItems(userId).ToList();
            userInfoDto.SystemJsonItems = MergeSystemJsonItems(systemJsonItems);
            userInfoDto.RolesCode = rc;
            return userInfoDto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<string> GetPrivileges(string userId) {
            List<PrivilegeDto> privileges = new List<PrivilegeDto>();
            privileges.AddRange(IocUnity.Get<RepositoryRelationUserOrganization>().GetPositionPrivileges(userId));
            privileges.AddRange(IocUnity.Get<RepositoryRelationUserRole>().GetPrivileges(userId));
            privileges.AddRange(IocUnity.Get<RepositoryRelationUserPrivilege>().GetPrivileges(userId));
            IList<string> prsCode = new List<string>();
            foreach (PrivilegeDto privilege in privileges)
            {
                prsCode.Add(privilege.Code);
            }
            return prsCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<string> GetRoles(string userId)
        {
            List<RoleDto> roles = new List<RoleDto>();
            roles.AddRange(IocUnity.Get<RepositoryRelationPositionRole>().GetRolesByUserId(userId));
            roles.AddRange(IocUnity.Get<RepositoryRelationUserRole>().GetRoles(userId));
            IList<string> codes = new List<string>();
            foreach (RoleDto role in roles) {
                codes.Add(role.Code);
            }
            return codes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsons"></param>
        /// <returns></returns>
        public List<JsonItem> ToJsonItems(IList<string> jsons) {
            if (jsons == null)
                return null;
            List<JsonItem> JsonItems = new List<JsonItem>();
            foreach (string json in jsons) {
                if (json != null)
                {
                    var r = SerializationUtility.JsonToObject<List<JsonItem>>(json);
                    if(r != null)
                        JsonItems.AddRange(r);
                }
            }
            return JsonItems;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsons"></param>
        /// <returns></returns>
        public List<SystemJsonItem> ToSystemJsonItems(IList<string> jsons)
        {
            if (jsons == null)
                return null;
            List<SystemJsonItem> jsonItems = new List<SystemJsonItem>();
            foreach (string json in jsons)
            {
                if (json != null)
                {
                    var r = SerializationUtility.JsonToObject<List<SystemJsonItem>>(json);
                    if(r != null)
                        jsonItems.AddRange(r);
                }
            }
            return jsonItems;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsons"></param>
        /// <returns></returns>
        public List<JsonItem> MergeJsonItems(List<JsonItem> jsons) {
            if (jsons == null)
                return null;
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            foreach (JsonItem jsonItem in jsons) {
                if(!string.IsNullOrEmpty(jsonItem.Path) && !keyValues.ContainsKey(jsonItem.Path))
                    keyValues.Add(jsonItem.Path, null);
            }
            List<JsonItem> jsonItems = new List<JsonItem>();
            foreach (string key in keyValues.Keys) {
                JsonItem item=null;
                foreach (JsonItem jsonItem in jsons) {
                    if (jsonItem.Path == key) {
                        if (item == null)
                            item = jsonItem;
                        else
                        {
                            if (jsonItem.Children != null)
                            {
                                if (item.Children != null)
                                    item.Children.AddRange(jsonItem.Children);
                                else
                                    item.Children = jsonItem.Children;
                            }
                        }
                    }
                }

                if (item != null)
                {
                    item.Children = MergeJsonItems(item.Children);
                    jsonItems.Add(item);
                }
            }
            return jsonItems;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsons"></param>
        /// <returns></returns>
        public List<SystemJsonItem> MergeSystemJsonItems(List<SystemJsonItem> jsons)
        {
            if (jsons == null)
                return null;
            List<SystemJsonItem> jsonItems = new List<SystemJsonItem>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            foreach (SystemJsonItem systemJsonItem in jsons) {
                if (!string.IsNullOrEmpty(systemJsonItem.FrontSystemCode) && !keyValues.ContainsKey(systemJsonItem.FrontSystemCode))
                    keyValues.Add(systemJsonItem.FrontSystemCode, null);
            }
            foreach (string key in keyValues.Keys) {
                List<JsonItem> items = new List<JsonItem>();
                foreach (SystemJsonItem sj in jsons) {
                    if (sj.FrontSystemCode == key)
                        items.AddRange(sj.Items);
                }
                items = MergeJsonItems(items);
                SystemJsonItem systemJsonItem = new SystemJsonItem { FrontSystemCode = key, Items = items };
                jsonItems.Add(systemJsonItem);
            }
            return jsonItems;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public PagedList<OrganizationAbstractDto> QueryUserOrganizationsByName(string userId, string name, int page, int size)
        {
            return IocUnity.Get<RepositoryUser>().QueryUserOrganizationsByName(userId, name, page, size);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="role"></param>
        /// <param name="isBeilian"></param>
        /// <param name="size"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public PagedList<V2UserInfoDto> GetUsers(string userId, string name, IList<int> role, bool isBeilian, int page, int size)
        {
            return IocUnity.Get<RepositoryUser>().GetUsers(userId, name, role, isBeilian, page, size);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="role"></param>
        /// <param name="isBeilian"></param>
        /// <param name="size"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public PagedList<V2UserInfoDto> GetV2UsersAll(string name, IList<int> role, bool isBeilian, int page, int size)
        {
            return IocUnity.Get<RepositoryUser>().GetV2UsersAll(name, role, isBeilian, page, size);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public PagedList<UserInfoDto> QueryUserRelationUserByName(string userId, string name, int page, int size) {
            return IocUnity.Get<RepositoryUser>().QueryUserRelationUserByName(userId, name, page, size);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public IList<V2UserInfoDto> GetV2UserInfos(IList<string> userIds) {
            return IocUnity.Get<RepositoryUser>().GetV2UserInfos(userIds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemCode"></param>
        /// <param name="roleCode"></param>
        /// <param name="key"></param>
        /// <param name="unitCode"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public PagedList<UserInfoDto> GetRoleUsers(string systemCode, string roleCode, string key, string unitCode, int page, int size) {
            return IocUnity.Get<RepositoryUser>().GetRoleUsers(systemCode, roleCode, key, unitCode, page, size);
        }
    }
}
