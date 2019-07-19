using System;
using Acb.MiddleWare.Core.Plugin;
using Microsoft.AspNetCore.Mvc;
using Acb.Plugin.PrivilegeManage.Common;
using System.Collections.Generic;
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
        /// 添加权限组
        /// </summary>
        /// <param name="privilegeGroupAddDto"></param>
        /// <returns></returns>
        [HttpPost("AddPrivilegeGroup")]
        public async Task<DResult<int>> AddPrivilegeGroup([FromBody]PrivilegeGroupAddDto privilegeGroupAddDto)
        {
            try
            {
                return DResult.Succ(businessPrivilege.AddPrivilegeGroup(privilegeGroupAddDto));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 使用已有模块添加权限
        /// </summary>
        /// <param name="privilegeAddDto"></param>
        /// <returns></returns>
        [HttpPost("AddPrivilege")]
        public async Task<DResult<int>> AddPrivilege([FromBody] PrivilegeAddDto privilegeAddDto)
        {
            try
            {
                return DResult.Succ(businessPrivilege.AddPrivilege(privilegeAddDto));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 根据权限组ID获取权限
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        [HttpGet("GetPrivilegesOfGroup")]
        public async Task<DResult<IList<PrivilegeDto>>> GetPrivilegesOfGroup(string GroupId)
        {
            try
            {
                return DResult.Succ(businessPrivilege.GetPrivilegeOfGroup(GroupId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<PrivilegeDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 根据系统ID查找权限组
        /// </summary>
        /// <param name="SystemId"></param>
        /// <returns></returns>
        [HttpGet("GetPrivilegeGroups")]
        public async Task<DResult<IList<TPrivilegeGroup>>> GetPrivilegeGroups(string SystemId)
        {
            try
            {
                return DResult.Succ(businessPrivilege.GetPrivilegeGroups(SystemId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<TPrivilegeGroup>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

       

        /// <summary>
        /// 使用新模块创建权限
        /// </summary>
        /// <param name="privileges"></param>
        /// <returns></returns>
        [HttpPost("AddPrivilegeForNewGroup")]
        public async Task<DResult<int>> AddPrivilegeForNewGroup([FromBody] PrivilegeAddForNewGroupDto privileges)
        {
            try
            {
                return DResult.Succ(businessPrivilege.AddPrivilegeForNewGroup(privileges));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 获取当前系统下的所有权限信息
        /// </summary>
        /// <param name="SystemId">系统 ID</param>
        /// <returns></returns>
        [HttpGet("GetAllPrivileges")]
        public async Task<DResult<IList<PrivilegeGroupAllDto>>> GetAllPrivileges(string SystemId)
        {
            try
            {
                return DResult.Succ(businessPrivilege.GetAllPrivileges(SystemId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<PrivilegeGroupAllDto>>(ex.Message, 500);
            }
        }

        

        /// <summary>
        /// 更新权限模块
        /// </summary>
        /// <param name="groupUpdateDto">权限模块信息</param>
        /// <returns></returns>
        [HttpPost("UpdatePrivileGroup")]
        public async Task<DResult<int>> UpdatePrivilegeGroup([FromBody]PrivilegeGroupUpdateDto groupUpdateDto)
        {
            try
            {
                return DResult.Succ(businessPrivilege.UpdatePrivilegeGroupUpdate(groupUpdateDto));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="privilegeUpdateDto"></param>
        /// <returns></returns>
        [HttpPost("UpdatePrivilege")]
        public async Task<DResult<int>> UpdatePrivilege([FromBody]PrivilegeUpdateDto privilegeUpdateDto)
        {
            try
            {
                return DResult.Succ(businessPrivilege.UpdatePrivilege(privilegeUpdateDto));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 删除权限模块
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        [HttpDelete("DeletePrivilegeGroup")]
        public async Task<DResult<int>> DeletePrivileteGroup(string GroupId)
        {
            try
            {
                return DResult.Succ(businessPrivilege.DeletePrivilegeGroup(GroupId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="PrivilegeId"></param>
        /// <returns></returns>
        [HttpDelete("DeletePrivilege")]
        public async Task<DResult<int>> DeletePrivilege(string PrivilegeId)
        {
            try
            {
                return DResult.Succ(businessPrivilege.DeletePrivilege(PrivilegeId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

    }

    }
