using System;
using Acb.MiddleWare.Core;
using Acb.MiddleWare.Core.Plugin;
using Microsoft.AspNetCore.Mvc;
using Acb.Plugin.PrivilegeManage.Common;
using System.Collections.Generic;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Attribution;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.OrganizationType;
using Dynamic.Core;
using Acb.Plugin.PrivilegeManage.Models.Entities;
using System.Threading.Tasks;
using Acb.Plugin.PrivilegeManage.Constract;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Relation;
using Acb.Core.Models;
using Dynamic.Core.Service;
using Acb.MiddleWare.Core.Authorize;
using Dynamic.Core.Models;

namespace Acb.Plugin.PrivilegeManage.Plugin
{

    /// <summary>
    /// 
    /// </summary>
    public partial class PolicyPrivilegeManagePlugin
    {
        /// <summary>
        /// 获取机构对应的用户数
        /// </summary>
        /// <param name="OrganizationId">机构ID</param>
        /// <param name="HierarchyType">
        /// 层级类型：0：机构；1：部门；2：岗位
        /// </param>
        /// <returns></returns>
        [HttpGet("GetOrganizationUserCount")]
        public async Task<DResult<long>> GetOrganizationUserCount(string OrganizationId, int HierarchyType)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetOrganizationUserCount(OrganizationId, HierarchyType));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<long>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 添加机构
        /// </summary>
        /// <param name="organization">机构对象信息</param>
        /// <returns></returns>
        [HttpPost("AddOrganization")]
        public async Task<DResult<OrganizationAbstractDto>> AddOrganization([FromBody]OrganizationAddAbstractDto organization)
        {
            try
            {
                var r = businessOrganization.AddOrganization(organization);
                if(r == null)
                    return DResult.Error<OrganizationAbstractDto>("机构名重复", 500);
                return DResult.Succ(r);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<OrganizationAbstractDto>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }
        /// <summary>
        /// 查找机构类型
        /// </summary>
        /// <param name="SystemId">系统ID</param>
        /// <param name="SystemCode">系统Code</param>
        /// <returns></returns>
        [HttpGet("GetOrganizationType")]
        public async Task<DResult<IList<OrganizationTypeDto>>> GetOrganizationType(string SystemId, string SystemCode)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetOrganizationType(SystemId, SystemCode));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<OrganizationTypeDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 查找子级机构
        /// </summary>
        /// <param name="ParentId">父级机构ID</param>
        /// <returns></returns>
        [HttpGet("GetSubOrganization")]
        public async Task<DResult<IList<OrganizationDto>>> GetSubOrganization(string ParentId)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetSubOrganization(ParentId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<OrganizationDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 查找子级
        /// </summary>
        /// <param name="ParentId">父级机构ID</param>
        /// <returns></returns>
        [HttpGet("GetSub")]
        public async Task<DResult<IList<OrganizationDto>>> GetSub(string ParentId)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetSub(ParentId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<OrganizationDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 分页查找子级机构
        /// </summary>
        /// <param name="ParentId">父级机构ID</param>
        /// <param name="Page">页码</param>
        /// <param name="Size">每页行数</param>
        /// <returns></returns>
        [HttpGet("GetSubOrganizationAsPage")]

        public async Task<DResult<PagedList<OrganizationDto>>> GetSubOrganizationAsPage(string ParentId, int Page, int Size)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetSubOrganization(ParentId, Page, Size));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<OrganizationDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 分页查找子级机构
        /// </summary>
        /// <param name="ParentId">父级机构ID</param>
        /// <param name="Page">页码</param>
        /// <param name="Size">每页行数</param>
        /// <returns></returns>
        [HttpGet("GetSubOrganizationAsPageAndParent")]

        public async Task<DResult<PagedList<OrganizationDto>>> GetSubOrganizationAsPageAndParent(string ParentId, int Page, int Size)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetSubOrganizationAndParent(ParentId, Page, Size));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<OrganizationDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 根据系统ID查找所有机构类型
        /// </summary>
        /// <param name="SystemId"></param>
        /// <param name="SystemCode"></param>
        /// <returns></returns>
        public async Task<DResult<IList<OrganizationTypeDto>>> GetOrganizationTypeOfSystem(string SystemId, string SystemCode=null)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetOrganizationTypeOfSystem(SystemId, SystemCode));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<OrganizationTypeDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 查找机构类型对应的属性类型
        /// </summary>
        /// <param name="TypeId">机构类型ID</param>
        /// <returns></returns>
        [HttpGet("GetOrganizationAttributionTypeOfTypeId")]
        public async Task<DResult<IList<AttributionTypeAddDto>>> GetOrganizationAttributionTypeOfTypeId(string TypeId)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetOrganizationAttributionTypeOfTypeId(TypeId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<AttributionTypeAddDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 获取机构类型详情
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        [HttpGet("GetOrganizationTypeInfo")]
        public async Task<DResult<OrganizationTypeDto>> GeetOrganizationTypeInfo(string TypeId)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetOrganizationTypeInfo(TypeId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<OrganizationTypeDto>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 添加机构类型
        /// </summary>
        /// <param name="organizationTypeAddDto"></param>
        /// <returns></returns>
        [HttpPost("AddOrganizationType")]
        public async Task<DResult<string>> AddOrganizationType([FromBody]OrganizationTypeAddDto organizationTypeAddDto)
        {
            try
            {
                return DResult.Succ(businessOrganization.AddOrganizationType(organizationTypeAddDto));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<string>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }
        
        
        /// <summary>
        /// 删除机构
        /// </summary>
        /// <param name="OrganizationId">机构ID</param>
        /// <returns></returns>
        [HttpDelete("DeleteOrganization")]
        public async Task<DResult<int>> DeleteOrganization(string OrganizationId)
        {
            try
            {
                return DResult.Succ(businessOrganization.DeleteOrganization(OrganizationId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }


        /// <summary>
        /// 添加岗位
        /// </summary>
        /// <param name="positionAddDto">岗位对象信息</param>
        /// <returns></returns>
        [HttpPost("AddPosition")]
        public async Task<DResult<OrganizationAbstractDto>> AddPosition([FromBody]PositionAddDto positionAddDto)
        {
            try
            {
                return DResult.Succ(businessOrganization.AddPosition(positionAddDto));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<OrganizationAbstractDto>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="departmentAddDto"></param>
        /// <returns></returns>
        [HttpPost("AddDepartment")]
        public async Task<DResult<OrganizationAbstractDto>> AddDepartment([FromBody]DepartmentAddDto departmentAddDto) {
            try
            {
                return DResult.Succ(businessOrganization.AddDepartment(departmentAddDto));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<OrganizationAbstractDto>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 根据机构类型获取对应的机构列表
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        [HttpGet("GetOrganizationOfType")]
        public async Task<DResult<IList<OrganizationDto>>> GetOrganizationOfType(string TypeId)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetOrganizationOfType(TypeId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<OrganizationDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 根据机构类型获取对应的机构列表
        /// </summary>
        /// <param name="TypeId"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="IsLoadAll"></param>
        /// <returns></returns>
        [HttpGet("GetOrganizationOfTypeAsPage")]
        public async Task<DResult<PagedList<OrganizationDto>>> GetOrganizationOfTypeAsPage(string TypeId, int Page, int Size, bool IsLoadAll=false)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetOrganizationOfType(TypeId, Page, Size, IsLoadAll));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<OrganizationDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 获取机构类型下所有的机构列表
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        [HttpGet("GetOrganizationAllOfType")]
        public async Task<DResult<IList<TOrganization>>> GetOrganizationAllOfType(string TypeId)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetOrganizationAllOfType(TypeId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<TOrganization>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }


        /// <summary>
        /// 根据机构类型ID删除机构类型
        /// </summary>
        /// <param name="OrganizationTypeId">机构类型ID</param>
        /// <returns></returns>
        [HttpDelete("DeleteOrganizationType")]
        public async Task<DResult<int>> DeleteOrganizationType(string OrganizationTypeId)
        {
            try
            {
                return DResult.Succ(businessOrganization.DeleteOrganizationType(OrganizationTypeId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 获取机构对应的父级机构列表
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        [HttpGet("GetParentList")]
        public async Task<DResult<IList<OrganizationDto>>> GetParentList(string OrganizationId)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetParentList(OrganizationId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<OrganizationDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 获取机构对应的父级机构列表以及机构类型
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        [HttpGet("GetParentListAndType")]
        public async Task<DResult<IList<OrganizationDto>>> GetParentListAndType(string OrganizationId)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetParentListAndType(OrganizationId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<OrganizationDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }



        /// <summary>
        /// 根据机构ID获取机构信息
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        [HttpGet("GetOrganizationInfoOfId")]
        public async Task<DResult<OrganizationInfoMoreDto>> GetOrganizationInfoOfId(string OrganizationId)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetOrganizationInfo(OrganizationId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<OrganizationInfoMoreDto>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        

        /// <summary>
        /// 更新机构类型
        /// </summary>
        /// <param name="organizationTypeUpdateDto"></param>
        /// <returns></returns>
        [HttpPost("UpdateOrganizationType")]
        public async Task<DResult<int>> UpdateOrganizationType([FromBody]OrganizationTypeUpdateDto organizationTypeUpdateDto)
        {
            try
            {
                return DResult.Succ(businessOrganization.UpdateOrganizationType(organizationTypeUpdateDto));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        
        /// <summary>
        /// 获取机构ID对应的父级机构树
        /// </summary>
        /// <param name="OrganizationId">机构ID</param>
        /// <returns></returns>
        [HttpGet("GetParentListAll")]
        public async Task<DResult<IList<IList<OrganizationDto>>>> GetParentListAll(string OrganizationId)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetParentListAll(OrganizationId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<IList<OrganizationDto>>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        

        /// <summary>
        /// 获取机构类型对应的查询属性类型
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        [HttpGet("GetOrganizationTypeQueryAttributionType")]
        public async Task<DResult<IList<AttributionTypeQueryInfoDto>>> GetOrganizationTypeQueryAttributionType(string TypeId)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetOrganizationTypeQueryAttributionType(TypeId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<AttributionTypeQueryInfoDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 获取机构列表
        /// </summary>
        /// <param name="TypeId"></param>
        /// <param name="Name"></param>
        /// <param name="SystemId">系统Id</param>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        [HttpGet("GetOrganizationsByName")]
        public async Task<DResult<IList<OrganizationDto>>> GetOrganizationsByName(string TypeId, string Name, string SystemId, string OrganizationId)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetOrganizationsByName(TypeId, Name, SystemId, OrganizationId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<OrganizationDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 获取机构列表
        /// </summary>
        /// <param name="TypeId"></param>
        /// <param name="Name"></param>
        /// <param name="SystemId">系统Id</param>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        [HttpGet("GetOrganizationsByDName")]
        public async Task<DResult<IList<OrganizationDto>>> GetOrganizationsByDName(string TypeId, string Name, string SystemId, string OrganizationId)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetOrganizationsByDName(TypeId, Name, SystemId, OrganizationId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<OrganizationDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 获取机构列表
        /// </summary>
        /// <param name="TypeId"></param>
        /// <param name="Name"></param>
        /// <param name="SystemId">系统Id</param>
        /// <param name="OrganizationId"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="SystemCode"></param>
        /// <param name="TypeName"></param>
        /// <returns></returns>
        [HttpGet("GetOrganizationsByNameAsPage")]
        public async Task<DResult<PagedList<OrganizationDto>>> GetOrganizationsByNameAsPage(string TypeId, string Name, string SystemId, string OrganizationId, string TypeName=null, string SystemCode=null, int Page=0, int Size=0)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetOrganizationsByNameAsPage(TypeId, Name, SystemId, OrganizationId, TypeName, SystemCode, Page, Size));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<OrganizationDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        [HttpGet("GetOrganizationsByDNameAsPage")]
        public async Task<DResult<PagedList<OrganizationDto>>> GetOrganizationsByDNameAsPage(string TypeId, string Name, string SystemId, string OrganizationId, string TypeName = null, string SystemCode = null, int Page = 0, int Size = 0)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetOrganizationsByDNameAsPage(TypeId, Name, SystemId, OrganizationId, TypeName, SystemCode, Page, Size));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<OrganizationDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        [HttpGet("GetSubOrganizationsNoChildren")]
        public async Task<DResult<PagedList<OrganizationDto>>> GetSubOrganizationsNoChildren(string organizationId, int page = 0, int size = 0)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetSubOrganizationsNoChildren(organizationId,page, size));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<OrganizationDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }
        /// <summary>
        /// 查询机构列表
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        [HttpPost("QueryOrganization")]
        public async Task<DResult<IList<OrganizationDto>>> QueryOrganization([FromBody] OrganizationQueryDto queryDto)
        {
            try
            {
                return DResult.Succ(businessOrganization.QueryOrganizations(queryDto.keyValues, queryDto.TypeId, queryDto.SystemId, queryDto.SystemCode));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<OrganizationDto>>(ExceptionParse.ParseString(ex.Message), 500);

            }
        }

        /// <summary>
        /// 分页查询机构列表
        /// </summary>
        /// <param name="queryPageDto"></param>
        /// <returns></returns>
        [HttpPost("QueryOrganizationAsPage")]
        public async Task<DResult<PagedList<OrganizationDto>>> QueryOrganizationAsPage([FromBody] OrganizationQueryPageDto queryPageDto)
        {
            try
            {
                return DResult.Succ(businessOrganization.QueryOrganizations(queryPageDto.keyValues, queryPageDto.Page, queryPageDto.Size, queryPageDto.TypeId, queryPageDto.SystemId, queryPageDto.SystemCode));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<OrganizationDto>>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

       

        /// <summary>
        /// 更新机构
        /// </summary>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        [HttpPost("UpdateOrganization")]
        public async Task<DResult<int>> UpdateOrganization([FromBody]OrganizationUpdateDto updateDto)
        {
            try
            {
                return DResult.Succ(businessOrganization.UpdateOrganization(updateDto));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }


        /// <summary>
        /// 获取父级机构对应的所有机构信息
        /// </summary>
        /// <param name="Code">机构Code</param>
        /// <param name="Page">页码</param>
        /// <param name="Size">行数</param>
        /// <param name="OrganizationId">机构ID</param>
        /// <returns></returns>
        [HttpGet("GetAllOrganizationOfCode")]
        public async Task<DResult<PagedList<OrganizationAbstractDto>>> GetAllOrganizationOfCode(string Code,int Page=0, int Size=0, string OrganizationId=null) {
            try
            {
                return DResult.Succ(businessOrganization.GetOrganizationAll(Code, Page, Size, OrganizationId));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<OrganizationAbstractDto>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 获取父级机构对应的所有机构详细信息
        /// </summary>
        /// <param name="Code">机构Code</param>
        /// <param name="Page">页码</param>
        /// <param name="Size">行数</param>
        /// <returns></returns>
        [HttpGet("GetAllOrganizationInfoOfCode")]
        public async Task<DResult<PagedList<OrganizationDto>>> GetAllOrganizationInfoOfCode(string Code, int Page, int Size)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetOrganizationAllInfo(Code, Page, Size));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<OrganizationDto>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 获取部门列表下拉框
        /// </summary>
        /// <param name="ParentCode">父级机构编码</param>
        /// <param name="Name">筛选名</param>
        /// <returns></returns>
        [HttpGet("GetDepartments")]
        public async Task<DResult<IList<OrganizationAbstractDto>>> GetDepartments(string ParentCode, string Name) {
            try
            {
                return DResult.Succ(businessOrganization.GetDepartments(ParentCode, Name));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<OrganizationAbstractDto>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 查询部门列表
        /// </summary>
        /// <param name="pageQueryDto"></param>
        /// <returns></returns>
        [HttpPost("QueryDepartments")]
        public async Task<DResult<PagedList<DepartmentInfoDto>>> QueryDepartments([FromBody]DepartmentPageQueryDto pageQueryDto) {
            try
            {
                return DResult.Succ(businessOrganization.QueryDepartments(pageQueryDto));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<DepartmentInfoDto>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 查询岗位列表
        /// </summary>
        /// <param name="pageQueryDto"></param>
        /// <returns></returns>
        [HttpPost("QueryPositions")]
        public async Task<DResult<PagedList<PositionInfoDto>>> QueryPositions([FromBody]PositionPageQueryDto pageQueryDto)
        {
            try
            {
                return DResult.Succ(businessOrganization.QueryPositions(pageQueryDto));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<PositionInfoDto>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="departmentUpdateDto"></param>
        /// <returns></returns>
        [HttpPost("UpdateDepartment")]
        public async Task<DResult<int>> UpdateDepartment([FromBody]DepartmentUpdateDto departmentUpdateDto) {
            try
            {
                return DResult.Succ(businessOrganization.UpdateDepartment(departmentUpdateDto));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 更新岗位
        /// </summary>
        /// <param name="positionUpdateDto"></param>
        /// <returns></returns>
        [HttpPost("UpdatePosition")]
        public async Task<DResult<int>> UpdatePosition([FromBody]PositionUpdateDto positionUpdateDto)
        {
            try
            {
                return DResult.Succ(businessOrganization.UpdatePosition(positionUpdateDto));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 通过父级机构和父级部门获取岗位列表
        /// </summary>
        /// <param name="ParentOrganizationCode"></param>
        /// <param name="ParentDepartmentCode"></param>
        /// <returns></returns>
        [HttpGet("GetPositions")]
        public async Task<DResult<IList<OrganizationAbstractDto>>> GetPositions(string ParentOrganizationCode, string ParentDepartmentCode) {
            try
            {
                return DResult.Succ(businessOrganization.GetPositions(ParentOrganizationCode, ParentDepartmentCode));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<OrganizationAbstractDto>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 获取关联机构对应的区域
        /// </summary>
        /// <param name="OrganizationCode"></param>
        /// <returns></returns>
        [HttpGet("GetAreas")]
        public async Task<DResult<IList<OrganizationAbstractDto>>> GetAreas(string OrganizationCode) {
            try
            {
                return DResult.Succ(businessOrganization.GetAreas(OrganizationCode));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<OrganizationAbstractDto>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 添加驻店员或销售员
        /// </summary>
        /// <param name="relationOrganization"></param>
        /// <returns></returns>
        [HttpPost("AddRelationUser")]
        public async Task<DResult<int>> AddRelationUser([FromBody] RelationOrganizationUserAddDto relationOrganization) {
            try
            {
                return DResult.Succ(businessOrganization.AddRelationUser(relationOrganization));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 根据机构ID或者Code获取关联机构
        /// </summary>
        /// <param name="OrganizationCode"></param>
        /// <param name="OrganizationId"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="Name"></param>
        /// <param name="TypeId"></param>
        /// <param name="Conditions"></param>
        /// <param name="RelationTypeId"></param>
        /// <returns></returns>
        [HttpGet("GetRelationOrganizations")]
        public async Task<DResult<PagedList<RelationOrganizationDetailDto>>> GetRelationOrganizations(string OrganizationCode, string Name, int Page, int Size, string OrganizationId=null, string TypeId=null,
            string RelationTypeId=null, IList<KeyValueItem> Conditions=null) {
            try
            {
                return DResult.Succ(businessOrganization.GetRelationOrganizations(OrganizationCode, Name, Page, Size, OrganizationId, TypeId, RelationTypeId, Conditions));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<RelationOrganizationDetailDto>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 删除关联机构
        /// </summary>
        /// <param name="RelationIds"></param>
        /// <returns></returns>
        [HttpPost("DeleteRelationOrganizations")]
        public async Task<DResult<int>> DeleteRelationOrganizations([FromBody]IList<string> RelationIds) {
            try
            {
                return DResult.Succ(businessOrganization.DeleteRelationOrganizations(RelationIds));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 根据TypeID获取关联机构
        /// </summary>
        /// <param name="TypeId">类型ID</param>
        /// <param name="RelationTypeId">关联的机构的类型Id</param>
        /// <returns></returns>
        [HttpGet("GetRelationMasterOrganizations")]
        public async Task<DResult<IList<OrganizationAbstractDto>>> GetRelationMasterOrganizations(string TypeId, string RelationTypeId) {
            try
            {
                return DResult.Succ(businessOrganization.GetRelationMasterOrganizations(TypeId, RelationTypeId));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<OrganizationAbstractDto>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 获取驻店员、销售员列表
        /// </summary>
        /// <param name="OrganizationCode"></param>
        /// <param name="UserType"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        [HttpGet("GetRelationOrganizationUsers")]
        public async Task<DResult<PagedList<RelationUserInfoDto>>> GetRelationOrganizationUsers(string OrganizationCode, int UserType, int Page, int Size) {
            try
            {
                return DResult.Succ(businessOrganization.GetRelationOrganizationUsers(OrganizationCode, UserType, Page, Size));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<RelationUserInfoDto>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 删除驻店员、销售员
        /// </summary>
        /// <param name="RelationId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteRelationOrganizationUser")]
        public async Task<DResult<int>> DeleteRelationOrganizationUser(string RelationId) {
            try
            {
                return DResult.Succ(businessOrganization.DeleteRelationUsers(RelationId));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 关联机构
        /// </summary>
        /// <param name="partnerDto"></param>
        /// <returns></returns>
        [HttpPost("CorrelateOrganization")]
        public async Task<DResult<OrganizationDto>> CorrelateOrganization([FromBody]PartnerDto partnerDto) {
            try
            {
                OrganizationDto organization = businessOrganization.CorrelateOrganization(partnerDto);
                if (organization == null)
                    return DResult.Error<OrganizationDto>("未找到该机构", 501);
                return DResult.Succ(organization);
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<OrganizationDto>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 批量获取机构信息
        /// </summary>
        /// <param name="OrganizationIds"></param>
        /// <returns></returns>
        [HttpGet("GetOrganizations")]
        public async Task<DResult<IList<OrganizationDto>>> GetOrganizations(IList<string> OrganizationIds) {
            try
            {
                return DResult.Succ(businessOrganization.GetOrganizations(OrganizationIds));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<OrganizationDto>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 获取关联机构信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetForeinOrganizations")]
        public async Task<DResult<IList<OrganizationForeignDto>>> GetForeignOrganizations(IList<string> OrganizationIds=null) {
            try
            {
                return DResult.Succ(businessOrganization.GetForeignDtos());
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<OrganizationForeignDto>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 添加关联机构关系
        /// </summary>
        /// <param name="relationOrganizationDtos"></param>
        /// <returns></returns>
        [HttpPost("AddRelationOrganizations")]
        public async Task<DResult<int>> AddRelationOrganizations([FromBody]IList<RelationOrganizationDto> relationOrganizationDtos) {
            try
            {
                return DResult.Succ(businessOrganization.AddRelationOrganizations(relationOrganizationDtos));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ExceptionParse.ParseString(ex.Message), 500);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationCode"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        [HttpGet("GetAll4sAllOrganizationsOfCode")]
        public async Task<DResult<PagedList<OrganizationAbstractDto>>> GetAll4sAllOrganizationsOfCode(string OrganizationCode, int Page=0, int Size=0, string OrganizationId=null) {
            try
            {
                return DResult.Succ(businessOrganization.GetAll4sOrganizationsOfCode(OrganizationCode, Page, Size, OrganizationId));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<OrganizationAbstractDto>>(ex.Message, 500);
            }
        }


        /// <summary>
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

        [HttpGet("GetUnits")]
        public async Task<DResult<PagedList<OrganizationDto>>> GetUnits(string UserId, string Name, bool IsSub, string TypeName, int Page = 0, int Size = 0)
        {
            try
            {
                return DResult.Succ(businessOrganization.GetUnits(UserId, Name, IsSub, TypeName, Page, Size));
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
                return DResult.Error<PagedList<OrganizationDto>>(e.Message, 500);
            }
        }

        /// <summary>
        /// 获取关联机构的类型
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        [HttpGet("GetRelationMasterOrganizationTypes")]
        public async Task<DResult<IList<OrganizationTypeDto>>> GetRelationMasterOrganizationTypes(string TypeId) {
            try {
                return DResult.Succ(businessOrganization.GetRelationMasterOrganizationTypes(TypeId));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<OrganizationTypeDto>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 获取v2区域分组
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("GetV2UnitGroup")]
        public async Task<DResult<PagedList<OrganizationAbstractDto>>> GetV2UnitGroup(string name, int Page, int Size) {
            try
            {
                return DResult.Succ(businessOrganization.GetV2UnitGroup(name, Page, Size));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<OrganizationAbstractDto>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 获取可关联的机构列表
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="SystemCode"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        [HttpGet("GetRelevancyOrganizations")]
        public async Task<DResult<PagedList<OrganizationDto>>> GetRelevancyOrganizations(string Name, string SystemCode, int Page, int Size) {
            try
            {
                return DResult.Succ(businessOrganization.GetRelevancyOrganizations(Name, SystemCode, Page, Size));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<OrganizationDto>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("GetScsjOrganization")]
        public async Task<DResult<IList<ScsjOrganizationDto>>> GetScsjOrganization(string key) {
            try
            {
                return DResult.Succ(businessOrganization.GetScsjOrganization(key));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<IList<ScsjOrganizationDto>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 获取机构信息通过DName
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("GetOrganizationByDName")]
        public async Task<DResult<OrganizationDto>> GetOrganizationByDName(string name) {
            try
            {
                return DResult.Succ(businessOrganization.GetOrganizationByDName(name));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<OrganizationDto>(ex.Message, 500);
            }
        }
    }

}
