using System;
using Acb.MiddleWare.Core.Plugin;
using Microsoft.AspNetCore.Mvc;
using Acb.Plugin.PrivilegeManage.Common;
using System.Collections.Generic;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Privilege;
using Dynamic.Core;
using Acb.Plugin.PrivilegeManage.Models.Entities;
using System.Threading.Tasks;
using Acb.Plugin.PrivilegeManage.Constract;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User;

namespace Acb.Plugin.PrivilegeManage.Plugin
{

    public partial class PolicyPrivilegeManagePlugin 
    {
        

        /// <summary>
        /// 角色关联权限
        /// </summary>
        /// <param name="relationRolePrivileges">角色权限关系列表对象</param>
        /// <returns></returns>
        [HttpPost("AddPrivilegeForRole")]
        public async Task<DResult<int>> AddPrivilegeForRole([FromBody]RelationRolePrivilegeAddDto relationRolePrivileges)
        {
            try
            {
                return DResult.Succ(businessPrivilege.AddPrivilegeForRole(relationRolePrivileges));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="roleAddDto"></param>
        /// <returns></returns>
        [HttpPost("AddRole")]
        public async Task<DResult<int>> AddRole([FromBody] RoleAddDto roleAddDto)
        {
            try
            {
                return DResult.Succ(businessRole.AddRole(roleAddDto));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="RoleId">角色ID</param>
        /// <returns></returns>
        [HttpDelete("DeleteRole")]
        public async Task<DResult<int>> DeleteRole(string RoleId)
        {
            try
            {
                return DResult.Succ(businessRole.DeleteRole(RoleId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

       

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="SystemId">系统ID</param>
        /// <returns></returns>
        [HttpGet("GetRoles")]
        public async Task<DResult<IList<RoleDto>>> GetRoles(string SystemId)
        {
            try
            {
                return DResult.Succ(businessRole.GetRoles(SystemId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<RoleDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="roleUpdateDto"></param>
        /// <returns></returns>
        [HttpPost("UpdateRole")]
        public async Task<DResult<int>> UpdateRole([FromBody] RoleUpdateDto roleUpdateDto)
        {
            try
            {
                return DResult.Succ(businessRole.UpdateRole(roleUpdateDto));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        
        /// <summary>
        /// 获取角色关联的权限
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpGet("GetPrivilegeOfRole")]
        public async Task<DResult<IList<PrivilegeDto>>> GetPrivilegeOfRole(string RoleId)
        {
            try
            {
                return DResult.Succ(businessPrivilege.GetPrivilegeOfRole(RoleId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<PrivilegeDto>>(ex.Message, 500);
            }
        }
    }

    }
