using System;

using Acb.MiddleWare.Core.Plugin;
using Microsoft.AspNetCore.Mvc;
using Acb.Plugin.PrivilegeManage.Common;
using System.Collections.Generic;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.System;
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
        /// 查找系统信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSystemInfoAll")]
        public async Task<DResult<IList<SystemDto>>> GetSystemInfoAll()
        {
            try
            {
                return DResult.Succ(businessSystem.GetSystemInfoAll());
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<SystemDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }
        /// <summary>
        /// 根据ID查找系统信息
        /// </summary>
        /// <param name="SystemId">系统ID</param>
        /// <returns></returns>
        [HttpGet("GetSystemInfoById")]
        public async Task<DResult<TSystem>> GetSystemInfoById(string SystemId)
        {
            try
            {
                return DResult.Succ(businessSystem.GetSystemInfoById(SystemId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<TSystem>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 添加系统
        /// </summary>
        /// <param name="systemAddDto"></param>
        /// <returns></returns>
        [HttpPost("AddSystemInfo")]
        public async Task<DResult<int>> AddSystemInfo([FromBody]SystemAddDto systemAddDto)
        {
            try
            {
                return DResult.Succ(businessSystem.AddSystemInfo(systemAddDto));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 根据ID删除系统
        /// </summary>
        /// <param name="SystemId">系统ID</param>
        /// <returns></returns>
        [HttpDelete("DeleteSystemInfoById")]
        public async Task<DResult<int>> DeleteSystemInfoById(string SystemId)
        {
            try
            {
                return DResult.Succ(businessSystem.DeleteInfoById(SystemId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }
        
        
        /// <summary>
        /// 更新系统信息
        /// </summary>
        /// <param name="systemUpdateDto"></param>
        /// <returns></returns>
        [HttpPost("UpdateSystem")]
        public async Task<DResult<int>> UpdateSystem([FromBody] SystemUpdateDto systemUpdateDto)
        {
            try
            {
                return DResult.Succ(businessSystem.UpdateSystem(systemUpdateDto));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }
    }

    }
