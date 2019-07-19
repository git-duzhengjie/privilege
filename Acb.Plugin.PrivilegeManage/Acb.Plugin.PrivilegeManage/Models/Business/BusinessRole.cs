using System;
using Acb.MiddleWare.Core;
using Dynamic.Core.Service;
using Acb.Plugin.PrivilegeManage.Models.Entities;
using System.Collections.Generic;
using Dynamic.Core.Comm;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Attribution;
using AutoMapper;
using Acb.Plugin.PrivilegeManage.Models.Repository;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role;
using Acb.Plugin.PrivilegeManage.Common;
using Dynamic.Core.Extensions;
using Dynamic.Core.Runtime;

namespace Acb.Plugin.PrivilegeManage.Models.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessRole
    {
        /// <summary>
        /// 
        /// </summary>
        public BusinessRole() { }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="roleAddDto"></param>
        /// <returns></returns>
        public int AddRole(RoleAddDto roleAddDto) {
            var systemCode = IocUnity.Get<RepositorySystem>().GetCode(roleAddDto.SystemId);
            TRole role = new TRole {
                Id = IdentityHelper.NewSequentialGuid().ToString("N"),
                OriginalCode = string.IsNullOrEmpty(roleAddDto.Code)?null:roleAddDto.Code,
                Code= string.IsNullOrEmpty(roleAddDto.Code) ? null : $"{systemCode}-{roleAddDto.Code}",
                Name = roleAddDto.Name,
                State = roleAddDto.State,
                SystemId = roleAddDto.SystemId,
                ItemId = roleAddDto.ItemId,
                JsonItem = roleAddDto.JsonItem
            };
            if (roleAddDto.Items.IsNullOrEmpty() && roleAddDto.Items.Count > 0)
            {
                IList<TRelationRoleItem> roleItems = new List<TRelationRoleItem>();
                foreach (var d in roleAddDto.Items)
                {
                    TRelationRoleItem relation = new TRelationRoleItem
                    {
                        Id = IdentityHelper.NewSequentialGuid().ToString("N"),
                        RoleId = role.Id,
                        ItemId = d,
                        CreateTime = DateTime.Now,
                        UpdateTime = null
                    };
                    roleItems.Add(relation);
                }

                int count = 0;
                IocUnity.Get<BaseData<TRelationRoleItem>>().DapperRepository.ExcuteTransaction(c =>
                    {
                        count = IocUnity.Get<RepositoryRole>().Insert(role);
                        IocUnity.Get<BaseData<TRelationRoleItem>>().Insert(roleItems);
                    });
                return count;
            }

            return IocUnity.Get<RepositoryRole>().Insert(role);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public int DeleteRole(string RoleId) {
            return IocUnity.Get<RepositoryRole>().Delete(RoleId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SystemId"></param>
        /// <returns></returns>
        public IList<RoleDto> GetRoles(string SystemId) {
            return IocUnity.Get<RepositoryRole>().GetRoles(SystemId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleUpdateDto"></param>
        /// <returns></returns>
        public int UpdateRole(RoleUpdateDto roleUpdateDto) {
            TRoleUpdate roleUpdate = AutoMapperExtensions.MapTo<TRoleUpdate>(roleUpdateDto);
            if (string.IsNullOrEmpty(roleUpdate.JsonItem))
                roleUpdate.JsonItem = "[]";
            roleUpdate.UpdateTime = DateTime.Now;
            var SystemCode = IocUnity.Get<RepositoryRole>().GetSystemCode(roleUpdate.Id);
            if (!string.IsNullOrEmpty(roleUpdateDto.Code))
            {
                roleUpdate.OriginalCode = roleUpdateDto.Code;
                roleUpdate.Code = $"{SystemCode}-{roleUpdate.OriginalCode}";
            }
            else
                roleUpdate.Code = null;

            int count = 0;
            IocUnity.Get<RepositoryRole>().DapperRepository.ExcuteTransaction(r =>
            {
                count = IocUnity.Get<RepositoryRole>().Update(roleUpdate);
                IocUnity.Get<RepositoryRole>().UpdateCode(roleUpdate.Id, roleUpdate.Code);
            });
            return count;
        }

    }
}
