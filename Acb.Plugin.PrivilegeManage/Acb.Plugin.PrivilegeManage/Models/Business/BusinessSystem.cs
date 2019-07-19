using Acb.MiddleWare.Core;
using Acb.Plugin.PrivilegeManage.Models.Entities;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.System;
using AutoMapper;
using Dynamic.Core.Service;
using System.Collections.Generic;
using Dynamic.Core.Comm;
using Acb.Plugin.PrivilegeManage.Models.Repository;
using Acb.Plugin.PrivilegeManage.Common;
using System;

namespace Acb.Plugin.PrivilegeManage.Models.Business
{
    /// <summary>
    /// 跟系统相关业务处理
    /// </summary>
    public class BusinessSystem
    {
        /// <summary>
        /// 
        /// </summary>
        public BusinessSystem() { }

        /// <summary>
        /// 获取所有系统信息
        /// </summary>
        /// <returns></returns>
        public IList<SystemDto> GetSystemInfoAll() {
            var systems = IocUnity.Get<RepositorySystem>().GetAll();
            return AutoMapperExtensions.MapTo<SystemDto>(systems);
        }

        /// <summary>
        /// 根据系统ID查找系统信息
        /// </summary>
        /// <param name="SystemId"></param>
        /// <returns></returns>
        public TSystem GetSystemInfoById(string SystemId) {
            return IocUnity.Get<RepositorySystem>().GetById(SystemId);
        }

        /// <summary>
        /// 添加系统信息
        /// </summary>
        /// <param name="systemAddDto"></param>
        /// <returns></returns>
        public int AddSystemInfo(SystemAddDto systemAddDto) {
            string Code;
            if (string.IsNullOrEmpty(systemAddDto.Code))
                Code = IocUnity.Get<RepositorySystem>().GetNextSystemCode();
            else
                Code = systemAddDto.Code;
            TSystem system = new TSystem
            {
                Id = IdentityHelper.NewSequentialGuid().ToString("N"),
                Name = systemAddDto.Name,
                EnglishName = systemAddDto.EnglishName,
                Instruction = systemAddDto.Instruction,
                LogoUrl = systemAddDto.LogoUrl,
                Code = Code
            };
            return IocUnity.Get<RepositorySystem>().Insert(system);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SystemId"></param>
        /// <returns></returns>
        public int DeleteInfoById(string SystemId) {
            return IocUnity.Get<RepositorySystem>().DeleteById(SystemId);
        }

        /// <summary>
        /// 更新系统信息
        /// </summary>
        /// <param name="systemUpdateDto"></param>
        /// <returns></returns>
        public int UpdateSystem(SystemUpdateDto systemUpdateDto) {
            TSystemUpdate system = AutoMapperExtensions.MapTo<TSystemUpdate>(systemUpdateDto);
            system.UpdateTime = DateTime.Now;
            return IocUnity.Get<RepositorySystem>().Update(system);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SystemId"></param>
        /// <param name="ModuleName"></param>
        /// <returns></returns>
        public int Clone(string SystemId, string ModuleName) {
            if (ModuleName.Equals("机构管理")) {
                return CloneOrganization(SystemId);
            }
            if (ModuleName.Equals("用户管理")) {
                return CloneUser(SystemId);
            }
            if (ModuleName.Equals("角色管理")) {
                return CloneRole(SystemId);
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SystemId"></param>
        /// <returns></returns>
        public int CloneOrganization(string SystemId) {
            //克隆机构类型

                //克隆机构类型对应的属性类型

                //克隆机构类型下对应的机构

                    //克隆机构
            //TODO:
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SystemId"></param>
        /// <returns></returns>
        public int CloneUser(string SystemId) {
            return 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="SystemId"></param>
        /// <returns></returns>
        public int CloneRole(string SystemId) {
            return 0;
        }
    }
}
