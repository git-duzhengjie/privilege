using System;
using Acb.MiddleWare.Core;
using Dynamic.Core.Service;
using Acb.Plugin.PrivilegeManage.Models.Entities;
using System.Collections.Generic;
using Dynamic.Core.Comm;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Privilege;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role;
using AutoMapper;
using Acb.Plugin.PrivilegeManage.Models.Repository;
using Acb.Plugin.PrivilegeManage.Common;
using Acb.Plugin.PrivilegeManage.Models.View.Privilege;

namespace Acb.Plugin.PrivilegeManage.Models.Business
{
    public class BusinessPrivilege
    {
        public BusinessPrivilege() { }

        /// <summary>
        /// 添加权限组
        /// </summary>
        /// <param name="privilegeGroupAddDto"></param>
        /// <returns></returns>
        public int AddPrivilegeGroup(PrivilegeGroupAddDto privilegeGroupAddDto) {
            TPrivilegeGroup privilegeGroup = new TPrivilegeGroup
            {
                Id = IdentityHelper.NewSequentialGuid().ToString("N"),
                Name = privilegeGroupAddDto.Name,
                SystemId = privilegeGroupAddDto.SystemId
            };
            return IocUnity.Get<RepositoryPrivilegeGroup>().Insert(privilegeGroup);
        }

        /// <summary>
        /// 使用已有模块添加权限
        /// </summary>
        /// <param name="privilegeAddDto"></param>
        /// <returns></returns>
        public int AddPrivilege(PrivilegeAddDto privilegeAddDto) {
            TPrivilege privilege = AutoMapperExtensions.MapTo<TPrivilege>(privilegeAddDto);
            privilege.OriginalCode = privilegeAddDto.Code;
            string SystemCode = IocUnity.Get<RepositoryPrivilegeGroup>().GetSystemCode(privilegeAddDto.GroupId);
            privilege.Code = $"{SystemCode}-{privilege.OriginalCode}";
            privilege.Id = IdentityHelper.NewSequentialGuid().ToString("N");
            return IocUnity.Get<RepositoryPrivilege>().Insert(privilege);
        }

        /// <summary>
        /// 根据权限组ID获取权限内容
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public IList<PrivilegeDto> GetPrivilegeOfGroup(string GroupId) {
            IList<TPrivilege> privileges = IocUnity.Get<RepositoryPrivilege>().GetByGroupId(GroupId);
            return AutoMapperExtensions.MapTo<PrivilegeDto>(privileges);
        }

        /// <summary>
        /// 获取权限组信息
        /// </summary>
        /// <param name="SystemId"></param>
        /// <returns></returns>
        public IList<TPrivilegeGroup> GetPrivilegeGroups(string SystemId) {
            return IocUnity.Get<RepositoryPrivilegeGroup>().GetPrivilegeGroups(SystemId);
        }

        /// <summary>
        /// 为角色关联权限
        /// </summary>
        /// <param name="vRelationRolePrivilegeAdds"></param>
        /// <returns></returns>
        public int AddPrivilegeForRole(RelationRolePrivilegeAddDto vRelationRolePrivilegeAdds) {
            IList<TRelationRolePrivilege> relationRolePrivileges = new List<TRelationRolePrivilege>();
            foreach (string  privilege in vRelationRolePrivilegeAdds.PrivilegeIds) {
                TRelationRolePrivilege relationRolePrivilege = new TRelationRolePrivilege {
                    Id = IdentityHelper.NewSequentialGuid().ToString("N"),
                    RoleId = vRelationRolePrivilegeAdds.RoleId,
                    PrivilegeId = privilege,
                    PrivilegeCode = IocUnity.Get<RepositoryPrivilege>().GetPrivilegeCode(privilege)
                };
                relationRolePrivileges.Add(relationRolePrivilege);
            }
            int count = 0;
            IocUnity.Get<RepositoryRelationRolePrivilege>().DapperRepository.ExcuteTransaction(tranc=> {
                IocUnity.Get<RepositoryRelationRolePrivilege>().Delete(vRelationRolePrivilegeAdds.RoleId);
                count = IocUnity.Get<RepositoryRelationRolePrivilege>().Insert(relationRolePrivileges);
            });
            return count;
        }

        /// <summary>
        /// 使用新模块创建权限
        /// </summary>
        /// <param name="privilegeAdd"></param>
        /// <returns></returns>
        public int AddPrivilegeForNewGroup(PrivilegeAddForNewGroupDto privilegeAdd) {
            TPrivilegeGroup privilegeGroup = new TPrivilegeGroup
            {
                Id = IdentityHelper.NewSequentialGuid().ToString("N"),
                Name = privilegeAdd.GroupName,
                SystemId = privilegeAdd.SystemId
            };
            string SystemCode = IocUnity.Get<RepositorySystem>().GetCode(privilegeAdd.SystemId);
            TPrivilege privilege = new TPrivilege
            {
                Id = IdentityHelper.NewSequentialGuid().ToString("N"),
                Code=$"{SystemCode}-{privilegeAdd.Code}",
                OriginalCode=privilegeAdd.Code,
                Name = privilegeAdd.Name,
                GroupId = privilegeGroup.Id,
                Instruction=privilegeAdd.Instruction
            };
            int count = 0;
            IocUnity.Get<RepositoryPrivilege>().DapperRepository.ExcuteTransaction(tranc =>
            {
                IocUnity.Get<RepositoryPrivilegeGroup>().Insert(privilegeGroup);
                count += IocUnity.Get<RepositoryPrivilege>().Insert(privilege);
            });
            return count;
        }

        /// <summary>
        /// 获取当前系统下的所有权限
        /// </summary>
        /// <param name="SystemId"></param>
        /// <returns></returns>
        public IList<PrivilegeGroupAllDto> GetAllPrivileges(string SystemId) {
            IList<PrivilegeGroupAllDto> privileges = new List<PrivilegeGroupAllDto>();
            IList<TPrivilegeGroup> privilegeGroups = IocUnity.Get<RepositoryPrivilegeGroup>().GetPrivilegeGroups(SystemId);
            IList<PrivilegeAllView> privilegeAllViews = IocUnity.Get<RepositoryPrivilegeGroup>().GetAllPrivileges(SystemId);
            int i = 0;
            foreach (TPrivilegeGroup group in privilegeGroups)
            {
                IList<PrivilegeDto> privilegeDtos = new List<PrivilegeDto>();
                for (int j = i; j < privilegeAllViews.Count; j++)
                {
                    if (privilegeAllViews[j].PrivilegeId != null)
                    {
                        PrivilegeDto privilegeDto = new PrivilegeDto
                        {
                            Id = privilegeAllViews[j].PrivilegeId,
                            Code= privilegeAllViews[j].PrivilegeCode,
                            OriginalCode=privilegeAllViews[j].OriginalCode,
                            Name = privilegeAllViews[j].PrivilegeName,
                            CreateTime = privilegeAllViews[j].PrivilegeCreateTime,
                            UpdateTime = privilegeAllViews[j].PrivilegeUpdateTime,
                            GroupId = privilegeAllViews[j].GroupId,
                            Instruction = privilegeAllViews[j].Instruction
                        };
                        privilegeDtos.Add(privilegeDto);
                    }
                    if (j+1 == privilegeAllViews.Count || privilegeAllViews[j + 1].GroupId != privilegeAllViews[j].GroupId)
                    {
                        i = j + 1;
                        break;
                    }
                }
                privileges.Add(new PrivilegeGroupAllDto
                {
                    Id = group.Id,
                    Name = group.Name,
                    UpdateTime = group.UpdateTime,
                    CreateTime = group.CreateTime,
                    Privileges = privilegeDtos
                });

            }
            return privileges;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public IList<PrivilegeDto> GetPrivilegeOfRole(string RoleId) {
            return IocUnity.Get<RepositoryRelationRolePrivilege>().GetPrivilegeOfRole(RoleId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupUpdateDto"></param>
        /// <returns></returns>
        public int UpdatePrivilegeGroupUpdate(PrivilegeGroupUpdateDto groupUpdateDto) {
            TPrivilegeGroupUpdate privilegeGroup = AutoMapperExtensions.MapTo<TPrivilegeGroupUpdate>(groupUpdateDto);
            privilegeGroup.UpdateTime = DateTime.Now;
            return IocUnity.Get<RepositoryPrivilegeGroup>().Update(privilegeGroup);
        }

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="privilegeUpdateDto"></param>
        /// <returns></returns>
        public int UpdatePrivilege(PrivilegeUpdateDto privilegeUpdateDto) {
            TPrivilegeUpdate update = AutoMapperExtensions.MapTo<TPrivilegeUpdate>(privilegeUpdateDto);
            update.OriginalCode = privilegeUpdateDto.Code;
            string SystemCode = IocUnity.Get<RepositoryPrivilege>().GetSystemCode(privilegeUpdateDto.Id);
            update.Code = $"{SystemCode}-{update.OriginalCode}";
            int count = 0;
            IocUnity.Get<RepositoryPrivilege>().DapperRepository.ExcuteTransaction(tranc => {
                count = IocUnity.Get<RepositoryPrivilege>().Update(update);
                IocUnity.Get<RepositoryPrivilege>().UpdateCode(update.Id, update.Code);
            });
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public int DeletePrivilegeGroup(string GroupId) {
            return IocUnity.Get<RepositoryPrivilegeGroup>().Delete(GroupId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PrivilegeId"></param>
        /// <returns></returns>
        public int DeletePrivilege(string PrivilegeId)
        {
            return IocUnity.Get<RepositoryPrivilege>().Delete(PrivilegeId);
        }
    }
}
