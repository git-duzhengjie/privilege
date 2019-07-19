using Acb.Plugin.PrivilegeManage.Models.Entities;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Attribution;
using Dynamic.Core.Comm;
using Dynamic.Core.Service;
using System.Collections.Generic;
using Acb.Plugin.PrivilegeManage.Models.Repository;
using Dynamic.Core;
using System;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.OrganizationType;
using Acb.Plugin.PrivilegeManage.Common;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User;
using Dynamic.Core.Runtime;
using Dynamic.Core.Models;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Relation;
using Dynamic.Core.Extensions;

namespace Acb.Plugin.PrivilegeManage.Models.Business
{
    /// <summary>
    /// 机构相关业务处理
    /// </summary>
    public class BusinessOrganization
    {
        /// <summary>
        /// 
        /// </summary>
        public BusinessOrganization() { }

        /// <summary>
        /// 新增机构
        /// </summary>
        /// <param name="vOrganization"></param>
        /// <returns></returns>
        public OrganizationAbstractDto AddOrganization(OrganizationAddAbstractDto vOrganization) {

            TOrganization tOrganization = AutoMapperExtensions.MapTo<TOrganization>(vOrganization);
            if (tOrganization.TypeId.IsNullOrEmpty()) {
                tOrganization.TypeId=IocUnity.Get<RepositoryOrganization>().GetTypeId(vOrganization.TypeName, vOrganization.SystemCode);
            }
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            TRelationOrganization relationOrganization=null;
            if (vOrganization.Attributions != null)
            {
                foreach (KeyValueItem attribution in vOrganization.Attributions)
                {
                    if (attribution.Key == "parentUnit")
                        tOrganization.ParentCode = attribution.Value != null ? attribution.Value.Split(',')[0] : null;
                    else if (attribution.Key == "unitName")
                        tOrganization.Name = attribution.Value;
                    else if (attribution.Key == "relationUnit" && !string.IsNullOrEmpty(attribution.Value))
                    {
                        if (relationOrganization == null)
                            relationOrganization = new TRelationOrganization
                            {
                                Id = IdentityHelper.NewSequentialGuid().ToString("N")
                            };
                        relationOrganization.RelationOrganizationCode = attribution.Value.Split(',')[0];
                        relationOrganization.RelationOrganizationId = IocUnity.Get<RepositoryOrganization>().GetId(relationOrganization.RelationOrganizationCode);
                    }
                    else if (attribution.Key == "relationRegion" && !string.IsNullOrEmpty(attribution.Value))
                    {
                        if (relationOrganization == null)
                            relationOrganization = new TRelationOrganization
                            {
                                Id = IdentityHelper.NewSequentialGuid().ToString("N")
                            };
                        relationOrganization.RelationAreaCode = attribution.Value.Split(',')[0];
                        relationOrganization.RelationAreaId = IocUnity.Get<RepositoryOrganization>().GetId(relationOrganization.RelationAreaCode);
                    }
                    keyValuePairs.Add(attribution.Key, attribution.Value);
                }
            }

            if (!string.IsNullOrEmpty(tOrganization.ParentCode))
                tOrganization.ParentId = IocUnity.Get<RepositoryOrganization>().GetId(tOrganization.ParentCode);
            else {
                if (!IsUnique(tOrganization.Name, tOrganization.TypeId))
                    return null;
            }
            tOrganization.HierarchyType = 0;
            if(string.IsNullOrEmpty(tOrganization.Id))
                tOrganization.Id = IdentityHelper.NewSequentialGuid().ToString("N");
            tOrganization.Code = IocUnity.Get<RepositoryOrganization>().GetNextOrganizationCode(tOrganization.ParentCode, tOrganization.TypeId);
            if (relationOrganization != null) {
                relationOrganization.OrganizationId = tOrganization.Id;
                relationOrganization.OrganizationCode = tOrganization.Code;
            }
            tOrganization.ExtendAttribution = SerializationUtility.ObjectToJson(keyValuePairs);
            int count = 0;
            IocUnity.Get<RepositoryOrganization>().DapperRepository.ExcuteTransaction(tranc => {
                var r = IocUnity.Get<RepositoryOrganization>();
                count += IocUnity.Get<RepositoryOrganization>().Insert(tOrganization);
                if (!string.IsNullOrEmpty(tOrganization.ParentId))
                {
                    IocUnity.Get<RepositoryOrganization>().Update("IsHasChildren", true, "Id", tOrganization.ParentId);
                    IocUnity.Get<RepositoryOrganization>().Update("IsHasChildOrganization", true, "Id", tOrganization.ParentId);
                }
                else
                {
                    IocUnity.Get<RepositoryOrganizationType>().Update("IsHasChildren", true, "Id", tOrganization.TypeId);
                }
                if (relationOrganization != null) {
                    IocUnity.Get<RepositoryRelationOrganization>().Insert(relationOrganization);
                }
            });
            return AutoMapperExtensions.MapTo<OrganizationAbstractDto>(IocUnity.Get<RepositoryOrganization>().GetById(tOrganization.Id));
        }

        private bool IsUnique(string Name, string TypeId) {
            if (IocUnity.Get<RepositoryOrganization>().CountName(Name, TypeId) != 0)
                return false;
            return true;
        }

        /// <summary>
        /// 查找机构类型
        /// </summary>
        /// <returns></returns>
        public IList<OrganizationTypeDto> GetOrganizationType(string SystemId, string SystemCode) {
            IList<TOrganizationType> organizationTypes = IocUnity.Get<RepositoryOrganizationType>().GetBySystemId(SystemId, SystemCode);
            return AutoMapperExtensions.MapTo<OrganizationTypeDto>(organizationTypes);
        }

        /// <summary>
        /// 查找子级机构
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        public IList<OrganizationDto> GetSubOrganization(string ParentId) {
            return AutoMapperExtensions.MapTo<OrganizationDto>(IocUnity.Get<RepositoryOrganization>().GetSubOrganizationOfId(ParentId));
        }

        /// <summary>
        /// 查找子级
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        public IList<OrganizationDto> GetSub(string ParentId)
        {
            return AutoMapperExtensions.MapTo<OrganizationDto>(IocUnity.Get<RepositoryOrganization>().GetSub(ParentId));
        }

        /// <summary>
        /// 查找子级机构
        /// </summary>
        /// <param name="ParentId"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public PagedList<OrganizationDto> GetSubOrganization(string ParentId, int Page, int Size)
        {
            return IocUnity.Get<RepositoryOrganization>().GetSubOrganizationOfId(ParentId, Page, Size);
        }

        /// <summary>
        /// 查找子级机构
        /// </summary>
        /// <param name="ParentId"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public PagedList<OrganizationDto> GetSubOrganizationAndParent(string ParentId, int Page, int Size)
        {
            return IocUnity.Get<RepositoryOrganization>().GetSubOrganizationOfIdAndParent(ParentId, Page, Size);
        }

        /// <summary>
        /// 获取当前系统下的所有机构类型
        /// </summary>
        /// <param name="SystemId"></param>
        /// <param name="SystemCode"></param>
        /// <returns></returns>
        public IList<OrganizationTypeDto> GetOrganizationTypeOfSystem(string SystemId, string SystemCode) {
            return AutoMapperExtensions.MapTo<OrganizationTypeDto>(IocUnity.Get<RepositoryOrganizationType>().GetBySystemId(SystemId, SystemCode));
        }

        /// <summary>
        /// 查找机构类型对应的属性类型
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        public IList<AttributionTypeAddDto> GetOrganizationAttributionTypeOfTypeId(string TypeId) {
            return AutoMapperExtensions.MapTo<AttributionTypeAddDto>(IocUnity.Get<RepositoryAttributionType>().GetAttributionTypes(TypeId));
        }

        /// <summary>
        /// 获取机构类型详情
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        public OrganizationTypeDto GetOrganizationTypeInfo(string TypeId)
        {
            OrganizationTypeDto organizationTypeDto = AutoMapperExtensions.MapTo<OrganizationTypeDto>(IocUnity.Get<RepositoryOrganizationType>().GetById(TypeId));
            organizationTypeDto.attributionTypes = GetOrganizationAttributionTypeOfTypeId(TypeId);
            return organizationTypeDto;
        }

        /// <summary>
        /// 添加机构类型
        /// </summary>
        /// <param name="organizationTypeAddDto"></param>
        /// <returns></returns>
        public string AddOrganizationType(OrganizationTypeAddDto organizationTypeAddDto) {
            TOrganizationType organizationType = AutoMapperExtensions.MapTo<TOrganizationType>(organizationTypeAddDto);
            organizationType.Id = IdentityHelper.NewSequentialGuid().ToString("N");
            organizationType.Code = IocUnity.Get<RepositoryOrganizationType>().GetNextOrganizationTypeCode(organizationTypeAddDto.SystemId);
            IList<TAttributionType> attributionTypes = new List<TAttributionType>();
            if (organizationTypeAddDto.AttributionTypes != null)
            {
                foreach (AttributionTypeAddDto attributionTypeAddDto in organizationTypeAddDto.AttributionTypes)
                {
                    TAttributionType attributionType = AutoMapperExtensions.MapTo<TAttributionType>(attributionTypeAddDto);
                    attributionType.Id = IdentityHelper.NewSequentialGuid().ToString("N");
                    attributionType.OrganizationTypeId = organizationType.Id;
                    attributionTypes.Add(attributionType);
                }
            }
            IocUnity.Get<RepositoryOrganization>().DapperRepository.ExcuteTransaction(tranc =>
            {
                IocUnity.Get<RepositoryOrganizationType>().Insert(organizationType);
                IocUnity.Get<RepositoryAttributionType>().Insert(attributionTypes);
            });
            return organizationType.Id;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        public int DeleteOrganization(string OrganizationId) {
            var r = IocUnity.Get<RepositoryOrganization>();
            string ParentId = r.GetParentId(OrganizationId);
            int count;
            if (string.IsNullOrEmpty(ParentId))
            {
                string TypeId = r.GetTypeIdOfId(OrganizationId);
                r.DapperRepository.ExcuteTransaction(tranc => {
                    count = r.DeleteOrganization(OrganizationId);
                    if (!r.IsHasChildren(TypeId, true))
                        IocUnity.Get<RepositoryOrganizationType>().Update("IsHasChildren", false, "Id", TypeId);

                });
            }
            else
            {
                r.DapperRepository.ExcuteTransaction(tranc=> {
                    count = r.DeleteOrganization(OrganizationId);
                    if (!r.IsHasChildren(ParentId, false))
                    {
                        IocUnity.Get<RepositoryOrganization>().Update("IsHasChildren", false, "Id", ParentId);
                        IocUnity.Get<RepositoryOrganization>().Update("IsHasChildOrganization", false, "Id", ParentId);
                    }
                });
            }
            return r.DeleteOrganization(OrganizationId);
        }

        /// <summary>
        /// 添加岗位
        /// </summary>
        /// <param name="positionAddDto"></param>
        /// <returns></returns>
        public OrganizationAbstractDto AddPosition(PositionAddDto positionAddDto)
        {
            TOrganization organization = AutoMapperExtensions.MapTo<TOrganization>(positionAddDto);
            organization.Id = IdentityHelper.NewSequentialGuid().ToString("N");
            organization.HierarchyType = 2;
            if (!string.IsNullOrEmpty(positionAddDto.ParentDepartmentCode))
            {
                organization.ParentCode = positionAddDto.ParentDepartmentCode;
            }
            else
            {
                organization.ParentCode = positionAddDto.ParentOrganizationCode;
            }
            organization.ParentId = IocUnity.Get<RepositoryOrganization>().GetId(organization.ParentCode);
            organization.TypeId = IocUnity.Get<RepositoryOrganization>().GetTypeId(organization.ParentCode);
            organization.Code = IocUnity.Get<RepositoryOrganization>().GetNextOrganizationCode(organization.ParentCode, organization.TypeId);
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("ParentOrganizationCode", positionAddDto.ParentOrganizationCode);
            keyValues.Add("ParentOrganizationId", IocUnity.Get<RepositoryOrganization>().GetId(positionAddDto.ParentOrganizationCode));
            keyValues.Add("ParentOrganizationName", IocUnity.Get<RepositoryOrganization>().GetNameByCode(positionAddDto.ParentOrganizationCode));
            keyValues.Add("ParentDepartmentCode", positionAddDto.ParentDepartmentCode);
            keyValues.Add("ParentDepartmentId", IocUnity.Get<RepositoryOrganization>().GetId(positionAddDto.ParentDepartmentCode));
            keyValues.Add("ParentDepartmentName", IocUnity.Get<RepositoryOrganization>().GetNameByCode(positionAddDto.ParentDepartmentCode));
           
            IList<TRelationPositionRole> relationPositionRoles = new List<TRelationPositionRole>();
            IList<Dictionary<string, object>> roles = new List<Dictionary<string, object>>();
            if (positionAddDto.Roles != null)
            {
                foreach (string role in positionAddDto.Roles)
                {
                    TRelationPositionRole relationPositionRole = new TRelationPositionRole
                    {
                        Id = IdentityHelper.NewSequentialGuid().ToString("N"),
                        PositionId = organization.Id,
                        RoleId = role
                    };
                    Dictionary<string, object> kv = new Dictionary<string, object>();
                    kv.Add("Id", role);
                    kv.Add("Name", IocUnity.Get<RepositoryRole>().GetName(role));
                    roles.Add(kv);
                    relationPositionRoles.Add(relationPositionRole);
                }
            }
            keyValues.Add("Roles", roles);
            organization.ExtendAttribution = SerializationUtility.ObjectToJson(keyValues);
            var count = 0;
            IocUnity.Get<RepositoryOrganization>().DapperRepository.ExcuteTransaction(tranc =>
            {
                count += IocUnity.Get<RepositoryOrganization>().Insert(organization);
                IocUnity.Get<RepositoryRelationPositionRole>().Insert(relationPositionRoles);
                IocUnity.Get<RepositoryOrganization>().Update("IsHasChildren", true, "Id", organization.ParentId);
                
            });
            return AutoMapperExtensions.MapTo<OrganizationAbstractDto>(IocUnity.Get<RepositoryOrganization>().GetById(organization.Id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionUpdateDto"></param>
        /// <returns></returns>
        public int UpdatePosition(PositionUpdateDto positionUpdateDto) {
            TPositionUpdate positionUpdate = AutoMapperExtensions.MapTo<TPositionUpdate>(positionUpdateDto);
            positionUpdate.ExtendAttribution = IocUnity.Get<RepositoryOrganization>().GetExtendAttribution(positionUpdateDto.Id);
            IList<TRelationPositionRole> relationPositionRoles = new List<TRelationPositionRole>();
            IList<Dictionary<string, object>> roles = new List<Dictionary<string, object>>();
            if (positionUpdateDto.Roles != null)
            {
                foreach (string role in positionUpdateDto.Roles)
                {
                    TRelationPositionRole relationPositionRole = new TRelationPositionRole
                    {
                        Id = IdentityHelper.NewSequentialGuid().ToString("N"),
                        PositionId = positionUpdate.Id,
                        RoleId = role
                    };
                    Dictionary<string, object> kv = new Dictionary<string, object>();
                    kv.Add("Id", role);
                    kv.Add("Name", IocUnity.Get<RepositoryRole>().GetName(role));
                    roles.Add(kv);
                    relationPositionRoles.Add(relationPositionRole);
                }
                Dictionary<string, object> keyValuePairs = SerializationUtility.JsonToObject<Dictionary<string, object>>(positionUpdate.ExtendAttribution);
                keyValuePairs["Roles"] = roles;
                positionUpdate.ExtendAttribution = SerializationUtility.ObjectToJson(keyValuePairs);
            }
            int count = 0;
            IocUnity.Get<RepositoryOrganization>().DapperRepository.ExcuteTransaction(tranc => {
                count = IocUnity.Get<RepositoryOrganization>().Update(positionUpdate);
                IocUnity.Get<RepositoryRelationPositionRole>().Delete(positionUpdate.Id, "PositionId");
                IocUnity.Get<RepositoryRelationPositionRole>().Insert(relationPositionRoles);
            });
            return count;
        }

        /// <summary>
        /// 获取机构对应的用户数
        /// </summary>
        /// <param name="OrganizationId">机构ID</param>
        /// <param name="HierarchyType">
        /// 层级类型：0：机构；1：部门；2；岗位
        /// </param>
        /// <returns></returns>
        public long GetOrganizationUserCount(string OrganizationId, int HierarchyType) {
            return IocUnity.Get<RepositoryRelationUserOrganization>().QueryCount(OrganizationId, HierarchyType);

        }

        /// <summary>
        /// 获取机构类型对应的机构列表
        /// </summary>
        /// <param name="TypeId">机构类型ID</param>
        /// <returns></returns>
        public IList<OrganizationDto> GetOrganizationOfType(string TypeId) {
            return IocUnity.Get<RepositoryOrganization>().GetOrganizationOfType(TypeId);
        }

        /// <summary>
        /// 获取机构类型对应的机构列表
        /// </summary>
        /// <param name="TypeId">机构类型ID</param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="IsLoadAll"></param>
        /// <returns></returns>
        public PagedList<OrganizationDto> GetOrganizationOfType(string TypeId, int Page, int Size, bool IsLoadAll=false)
        {
            return IocUnity.Get<RepositoryOrganization>().GetOrganizationOfType(TypeId, Page, Size, IsLoadAll);
        }

        /// <summary>
        /// 获取当前机构类型下面的所有机构
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        public IList<TOrganization> GetOrganizationAllOfType(string TypeId) {
            return IocUnity.Get<RepositoryOrganization>().GetOrganizationAllOfType(TypeId);
        }

        /// <summary>
        /// 获取机构下面的所有用户列表
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <param name="HierarchyType"></param>
        /// <returns></returns>
        public ResultModel GetUsersOfOrganization(string OrganizationId, int HierarchyType) {
            return new ResultModel
            {
                data = IocUnity.Get<RepositoryRelationUserOrganization>().GetUsersOfOrganization(OrganizationId, HierarchyType),
                msg ="ok",
                state = true
            };
        }

        /// <summary>
        /// 根据属性条件获取机构
        /// </summary>
        /// <param name="organizationAttributions"></param>
        /// <returns></returns>
        public ResultModel GetOrganizationInfoOfAttributions(IList<OrganizationAttributionQueryDto> organizationAttributions) {
            return new ResultModel { };
        }

        /// <summary>
        /// 删除机构类型
        /// </summary>
        /// <param name="OrganizationTypeId"></param>
        /// <returns></returns>
        public int DeleteOrganizationType(string OrganizationTypeId) {
            return IocUnity.Get<RepositoryOrganizationType>().DeleteById(OrganizationTypeId);
        }

        /// <summary>
        /// 获取机构对应的父级机构列表
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        public IList<OrganizationDto> GetParentList(string OrganizationId) {
            return IocUnity.Get<RepositoryOrganization>().GetParentList(OrganizationId);
        }

        /// <summary>
        /// 获取机构对应的父级机构列表
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        public IList<OrganizationDto> GetParentListAndType(string OrganizationId)
        {
            IList<OrganizationDto> list = IocUnity.Get<RepositoryOrganization>().GetParentList(OrganizationId);
            string TypeId = IocUnity.Get<RepositoryOrganization>().GetTypeIdOfId(OrganizationId);
            OrganizationDto typeDto = AutoMapperExtensions.MapTo<OrganizationDto>(IocUnity.Get<RepositoryOrganizationType>().GetById(TypeId));
            list.Insert(0, typeDto);
            return list;
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="departmentAddDto"></param>
        /// <returns></returns>
        public OrganizationAbstractDto AddDepartment(DepartmentAddDto departmentAddDto)
        {
            TOrganization organization = AutoMapperExtensions.MapTo<TOrganization>(departmentAddDto);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            if (departmentAddDto.ParentDepartmentCode.IsNotNullOrEmpty())
            {
                keyValues.Add("ParentDepartmentCode", departmentAddDto.ParentDepartmentCode);
                keyValues.Add("ParentDepartmentId", IocUnity.Get<RepositoryOrganization>().GetId(departmentAddDto.ParentDepartmentCode));
            }
            if (departmentAddDto.ParentDepartmentId.IsNotNullOrEmpty()){
                keyValues.Add("ParentDepartmentCode", IocUnity.Get<RepositoryOrganization>().GetCode(departmentAddDto.ParentDepartmentId));
                keyValues.Add("ParentDepartmentId", departmentAddDto.ParentDepartmentId);
            }
            if (departmentAddDto.ParentDepartmentId.IsNullOrEmpty() && departmentAddDto.ParentDepartmentCode.IsNullOrEmpty())
            {
                keyValues.Add("ParentDepartmentCode", null);
                keyValues.Add("ParentDepartmentId", null);
            }   
            if (departmentAddDto.ParentOrganizationCode.IsNotNullOrEmpty())
            {
                keyValues.Add("ParentOrganizationCode", departmentAddDto.ParentOrganizationCode);
                keyValues.Add("ParentOrganizationId", IocUnity.Get<RepositoryOrganization>().GetId(departmentAddDto.ParentOrganizationCode));
            }
            if(departmentAddDto.ParentOrganizationId.IsNotNullOrEmpty()){
                keyValues.Add("ParentOrganizationCode", IocUnity.Get<RepositoryOrganization>().GetCode(departmentAddDto.ParentOrganizationId));
                keyValues.Add("ParentOrganizationId", departmentAddDto.ParentOrganizationId);
            }
            keyValues.Add("ParentDepartmentName", IocUnity.Get<RepositoryOrganization>().GetNameByCode(keyValues["ParentDepartmentCode"]));
            keyValues.Add("ParentOrganizationName", IocUnity.Get<RepositoryOrganization>().GetNameByCode(keyValues["ParentOrganizationCode"]));
            if (keyValues["ParentDepartmentCode"].IsNotNullOrEmpty())
            {
                organization.ParentCode = keyValues["ParentDepartmentCode"];
                organization.ParentId = keyValues["ParentDepartmentId"];
            }
            else
            {
                organization.ParentCode = keyValues["ParentOrganizationCode"];
                organization.ParentId = keyValues["ParentOrganizationId"];
            }
            if(organization.Id.IsNullOrEmpty())
                organization.Id = IdentityHelper.NewSequentialGuid().ToString("N");
            organization.TypeId = IocUnity.Get<RepositoryOrganization>().GetTypeId(organization.ParentCode);
            organization.Code = IocUnity.Get<RepositoryOrganization>().GetNextOrganizationCode(organization.ParentCode, organization.TypeId);
            organization.HierarchyType = 1;
            organization.ExtendAttribution = SerializationUtility.ObjectToJson(keyValues);
            int count = 0;
            IocUnity.Get<RepositoryOrganization>().DapperRepository.ExcuteTransaction(tranc => {
                count = IocUnity.Get<RepositoryOrganization>().Insert(organization);
                IocUnity.Get<RepositoryOrganization>().Update("IsHasChildren", true, "Id", organization.ParentId);
            });
            return AutoMapperExtensions.MapTo<OrganizationAbstractDto>(IocUnity.Get<RepositoryOrganization>().GetById(organization.Id));
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="departmentUpdateDto"></param>
        /// <returns></returns>
        public int UpdateDepartment(DepartmentUpdateDto departmentUpdateDto) {
            TDepartmentUpdate departmentUpdate = AutoMapperExtensions.MapTo<TDepartmentUpdate>(departmentUpdateDto);
            return IocUnity.Get<RepositoryOrganization>().Update(departmentUpdate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationId">机构ID</param>
        /// <returns></returns>
        public IList<IList<OrganizationDto>> GetParentListAll(string OrganizationId) {
            IList<OrganizationDto> organizationDtos = IocUnity.Get<RepositoryOrganization>().GetParentList(OrganizationId);
            IList<IList<OrganizationDto>> organizations = new List<IList<OrganizationDto>>();
            foreach (OrganizationDto organizationDto in organizationDtos) {
                IList<OrganizationDto> organization;
                if (organizationDto.ParentId == null)
                {
                    organization = AutoMapperExtensions.MapTo<OrganizationDto>(IocUnity.Get<RepositoryOrganization>().GetOrganizationOfType(organizationDto.TypeId));
                }else
                    organization = AutoMapperExtensions.MapTo<OrganizationDto>(IocUnity.Get<RepositoryOrganization>().GetSubOrganizationOfId(organizationDto.ParentId));
                organizations.Add(organization);
            }

            return organizations;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        public OrganizationInfoMoreDto GetOrganizationInfo(string OrganizationId) {
            return  IocUnity.Get<RepositoryOrganization>().QueryById(OrganizationId);
        }

        /// <summary>
        /// 更新机构类型
        /// </summary>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        public int UpdateOrganizationType(OrganizationTypeUpdateDto updateDto) {
            int count = 0;
            IList<TAttributionType> attributionTypes = new List<TAttributionType>();

            if (updateDto.AttributionTypes != null)
            {
                foreach (AttributionTypeAddDto attributionTypeAddDto in updateDto.AttributionTypes)
                {
                    TAttributionType attributionType = AutoMapperExtensions.MapTo<TAttributionType>(attributionTypeAddDto);
                    attributionType.Id = IdentityHelper.NewSequentialGuid().ToString("N");
                    attributionType.OrganizationTypeId = updateDto.Id;
                    attributionTypes.Add(attributionType);
                }
            }
            TOrganizationTypeUpdate organizationTypeUpdate = AutoMapperExtensions.MapTo<TOrganizationTypeUpdate>(updateDto);
            organizationTypeUpdate.UpdateTime = DateTime.Now;
            IocUnity.Get<RepositoryOrganization>().DapperRepository.ExcuteTransaction(tranc => {
                count += IocUnity.Get<RepositoryOrganizationType>().Update(organizationTypeUpdate);
                IocUnity.Get<RepositoryAttributionType>().DeleteWhere("OrganizationTypeId", updateDto.Id);
                IocUnity.Get<RepositoryAttributionType>().Insert(attributionTypes);
            });
            return count;
        }

        /// <summary>
        /// 获取机构类型对应的查询类型
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        public IList<AttributionTypeQueryInfoDto> GetOrganizationTypeQueryAttributionType(string TypeId) {
            return AutoMapperExtensions.MapTo<AttributionTypeQueryInfoDto>(IocUnity.Get<RepositoryAttributionType>().GetQueryAttributionTypes(TypeId));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeId"></param>
        /// <param name="Name"></param>
        /// <param name="SystemId"></param>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>

        public IList<OrganizationDto> GetOrganizationsByName(string TypeId, string Name, string SystemId, string OrganizationId) {
            return IocUnity.Get<RepositoryOrganization>().QueryOrganizationsByName(TypeId, Name, SystemId, OrganizationId);
        }

        public IList<OrganizationDto> GetOrganizationsByDName(string TypeId, string Name, string SystemId, string OrganizationId)
        {
            return IocUnity.Get<RepositoryOrganization>().QueryOrganizationsByDName(TypeId, Name, SystemId, OrganizationId);
        }


        /// <summary>
        /// 
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
        public PagedList<OrganizationDto> GetOrganizationsByNameAsPage(string TypeId, string Name, string SystemId, string OrganizationId, string TypeName, string SystemCode, int Page, int Size)
        {
            if (string.IsNullOrEmpty(SystemId) && !string.IsNullOrEmpty(SystemCode))
                SystemId = IocUnity.Get<RepositorySystem>().GetId(SystemCode);
            if (string.IsNullOrEmpty(TypeId) && !string.IsNullOrEmpty(TypeName))
                TypeId = IocUnity.Get<RepositoryOrganizationType>().GetTypeId(SystemId, TypeName);
            return IocUnity.Get<RepositoryOrganization>().QueryOrganizationsByNameAsPage(TypeId, Name, SystemId, OrganizationId, Page, Size);
        }

        public PagedList<OrganizationDto> GetOrganizationsByDNameAsPage(string TypeId, string Name, string SystemId, string OrganizationId, string TypeName, string SystemCode, int Page, int Size)
        {
            if (string.IsNullOrEmpty(SystemId) && !string.IsNullOrEmpty(SystemCode))
                SystemId = IocUnity.Get<RepositorySystem>().GetId(SystemCode);
            if (string.IsNullOrEmpty(TypeId) && !string.IsNullOrEmpty(TypeName))
                TypeId = IocUnity.Get<RepositoryOrganizationType>().GetTypeId(SystemId, TypeName);
            return IocUnity.Get<RepositoryOrganization>().QueryOrganizationsByDNameAsPage(TypeId, Name, SystemId, OrganizationId, Page, Size);
        }

        public PagedList<OrganizationDto> GetSubOrganizationsNoChildren(string organizationId, int page, int size) {
            return IocUnity.Get<RepositoryOrganization>().GetSubOrganizationsNoChildren(organizationId, page, size);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValueItems"></param>
        /// <param name="TypeId"></param>
        /// <param name="SystemCode"></param>
        /// <param name="SystemId"></param>
        /// <returns></returns>
        public IList<OrganizationDto> QueryOrganizations(IList<KeyValueItem> keyValueItems, string TypeId, string SystemId, string SystemCode) {
            if (SystemId.IsNullOrEmpty() && !SystemCode.IsNullOrEmpty())
                SystemId = IocUnity.Get<RepositorySystem>().GetId(SystemCode);
            return AutoMapperExtensions.MapTo<OrganizationDto>(IocUnity.Get<RepositoryOrganization>().QueryByWhere(GerateQuerySql(keyValueItems), TypeId, SystemId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValueItems"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="TypeId"></param>
        /// <param name="SystemCode"></param>
        /// <param name="SystemId"></param>
        /// <returns></returns>
        public PagedList<OrganizationDto> QueryOrganizations(IList<KeyValueItem> keyValueItems, int Page, int Size, string TypeId, string SystemId=null, string SystemCode=null)
        {
            return IocUnity.Get<RepositoryOrganization>().QueryByWhere(GerateQuerySql(keyValueItems), Page, Size, TypeId, SystemId);
        }

        private string GerateQuerySql(IList<KeyValueItem> keyValueItems) {
            string sql = "";
            foreach (KeyValueItem kv in keyValueItems) {
                if (kv.Key == "parentUnit" && !string.IsNullOrEmpty(kv.Value))
                    sql += $" and [ParentCode]='{kv.Value.Split(',')[0]}'";
                else if (kv.Key == "unitName" && !string.IsNullOrEmpty(kv.Value))
                    sql += $" and [Name] like '%{kv.Value}%'";
                else if (!string.IsNullOrEmpty(kv.Value))
                {
                    if (kv.Key == "shorterName")
                        sql += $" and [ExtendAttribution]->>'{kv.Key}' like '%{kv.Value}%'";
                    else
                        sql += $" and [ExtendAttribution]->>'{kv.Key}'='{kv.Value}'";
                }
            }
            return sql;
        }

        private string GerateQuerySql2(string Name, IList<KeyValueItem> keyValueItems)
        {
            if (keyValueItems == null)
                return "";
            string sql = "";
            foreach (KeyValueItem kv in keyValueItems)
            {
                if (kv.Key == "parentUnit" && !string.IsNullOrEmpty(kv.Value))
                    sql += $" and t2.[ParentCode]='{kv.Value.Split(',')[0]}'";
                else if (kv.Key == "unitName" && !string.IsNullOrEmpty(kv.Value) && Name.IsNullOrEmpty())
                    sql += $" and t2.[Name] like '%{kv.Value}%'";
                else if (!string.IsNullOrEmpty(kv.Value) && kv.Key != "unitName")
                {
                    if (kv.Key == "shorterName")
                        sql += $" and t2.[ExtendAttribution]->>'{kv.Key}' like '%{kv.Value}%'";
                    else
                        sql += $" and t2.[ExtendAttribution]->>'{kv.Key}'='{kv.Value}'";
                }
            }
            return sql;
        }

        /// <summary>
        /// 更新机构
        /// </summary>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        public int UpdateOrganization(OrganizationUpdateDto updateDto) {
            TOrganizationUpdate tOrganization = AutoMapperExtensions.MapTo<TOrganizationUpdate>(updateDto);
            TRelationOrganization relationOrganization = null;
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            if (updateDto.Attributions != null)
            {
                foreach (KeyValueItem attribution in updateDto.Attributions)
                {
                    if (attribution.Key == "parentUnit")
                        tOrganization.ParentCode = attribution.Value != null ? attribution.Value.Split(',')[0] : null;
                    else if (attribution.Key == "unitName")
                        tOrganization.Name = attribution.Value;

                    else if (attribution.Key == "relationUnit" && !string.IsNullOrEmpty(attribution.Value))
                    {
                        if (relationOrganization == null)
                            relationOrganization = new TRelationOrganization
                            {
                                Id = IdentityHelper.NewSequentialGuid().ToString("N")
                            };
                        relationOrganization.RelationOrganizationCode = attribution.Value.Split(',')[0];
                        relationOrganization.RelationOrganizationId = IocUnity.Get<RepositoryOrganization>().GetId(relationOrganization.RelationOrganizationCode);
                    }
                    else if (attribution.Key == "relationRegion" && !string.IsNullOrEmpty(attribution.Value))
                    {
                        if (relationOrganization == null)
                            relationOrganization = new TRelationOrganization
                            {
                                Id = IdentityHelper.NewSequentialGuid().ToString("N")
                            };
                        relationOrganization.RelationAreaCode = attribution.Value.Split(',')[0];
                        relationOrganization.RelationAreaId = IocUnity.Get<RepositoryOrganization>().GetId(relationOrganization.RelationAreaCode);
                    }
                    
                    keyValuePairs.Add(attribution.Key, attribution.Value);
                }
            }
            if (relationOrganization != null)
            {
                relationOrganization.OrganizationId = tOrganization.Id;
                relationOrganization.OrganizationCode = IocUnity.Get<RepositoryOrganization>().GetCode(tOrganization.Id);
            }
            if (!string.IsNullOrEmpty(tOrganization.ParentCode))
                tOrganization.ParentId = IocUnity.Get<RepositoryOrganization>().GetId(tOrganization.ParentCode);
            if (tOrganization.ParentId.IsNotNullOrEmpty())
                tOrganization.ParentCode = IocUnity.Get<RepositoryOrganization>().GetCode(tOrganization.ParentId);
            string parentId = IocUnity.Get<RepositoryOrganization>().GetParentId(tOrganization.Id);
            string parentCode = IocUnity.Get<RepositoryOrganization>().GetCode(parentId);
            string code = IocUnity.Get<RepositoryOrganization>().GetCode(tOrganization.Id);
            if (tOrganization.ParentId != parentId)
                tOrganization.Code = IocUnity.Get<RepositoryOrganization>().GetNextOrganizationCode(tOrganization.ParentCode, IocUnity.Get<RepositoryOrganization>().GetTypeIdOfId(tOrganization.Id));
            else
                tOrganization.Code = code;
            tOrganization.UpdateTime = DateTime.Now;
            tOrganization.ExtendAttribution = SerializationUtility.ObjectToJson(keyValuePairs);
            int count = 0;
            IocUnity.Get<RepositoryOrganization>().DapperRepository.ExcuteTransaction(tranc => {
                count += IocUnity.Get<RepositoryOrganization>().Update(tOrganization);
                if (!string.IsNullOrEmpty(tOrganization.ParentId))
                {
                    IocUnity.Get<RepositoryOrganization>().Update("IsHasChildren", true, "Id", tOrganization.ParentId);
                    IocUnity.Get<RepositoryOrganization>().Update("IsHasChildOrganization", true, "Id", tOrganization.ParentId);
                }
                if (relationOrganization != null) {
                    IocUnity.Get<RepositoryRelationOrganization>().Delete(tOrganization.Id);
                    IocUnity.Get<RepositoryRelationOrganization>().Insert(relationOrganization);
                }
                if (tOrganization.ParentId != parentId) {
                    IocUnity.Get<RepositoryOrganization>().UpdateCode(code, tOrganization.Code);
                }
            });
            return count;
        }

        /// <summary>
        /// 获取机构对应的所有机构信息
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        public PagedList<OrganizationAbstractDto> GetOrganizationAll(string Code, int Page=0, int Size=0, string OrganizationId=null) {
            return IocUnity.Get<RepositoryOrganization>().GetOrganizationAll(Code, Page, Size, OrganizationId);
        }

        /// <summary>
        /// 获取机构对应的所有机构信息
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public PagedList<OrganizationDto> GetOrganizationAllInfo(string Code, int Page, int Size)
        {
            return IocUnity.Get<RepositoryOrganization>().GetOrganizationAllInfo(Code, Page, Size);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParentCode"></param>
        /// <param name="Name">筛选名</param>
        /// <returns></returns>
        public IList<OrganizationAbstractDto> GetDepartments(string ParentCode, string Name) {
            return IocUnity.Get<RepositoryOrganization>().GetDepartments(ParentCode, Name);
        }

        /// <summary>
        /// 查询部门列表
        /// </summary>
        /// <param name="pageQueryDto"></param>
        /// <returns></returns>
        public PagedList<DepartmentInfoDto> QueryDepartments(DepartmentPageQueryDto pageQueryDto) {
            return IocUnity.Get<RepositoryOrganization>().QueryDepartments(pageQueryDto);
        }

        /// <summary>
        /// 查询岗位列表
        /// </summary>
        /// <param name="pageQueryDto"></param>
        /// <returns></returns>
        public PagedList<PositionInfoDto> QueryPositions(PositionPageQueryDto pageQueryDto)
        {
            return IocUnity.Get<RepositoryOrganization>().QueryPositions(pageQueryDto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParentOrganizationCode"></param>
        /// <param name="ParentDepartmentCode"></param>
        /// <returns></returns>
        public IList<OrganizationAbstractDto> GetPositions(string ParentOrganizationCode, string ParentDepartmentCode) {
            return IocUnity.Get<RepositoryOrganization>().GetPositions(ParentOrganizationCode, ParentDepartmentCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationCode"></param>
        /// <returns></returns>
        public IList<OrganizationAbstractDto> GetAreas(string OrganizationCode) {
            return IocUnity.Get<RepositoryOrganization>().GetAreas(OrganizationCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relationOrganization"></param>
        /// <returns></returns>
        public int AddRelationUser(RelationOrganizationUserAddDto relationOrganization) {
            IList<TRelationUserOrganization> relationUserOrganizations = new List<TRelationUserOrganization>();
            if (relationOrganization.Users != null)
            {
                foreach (string userDto in relationOrganization.Users)
                {
                    TRelationUserOrganization relationUserOrganization = new TRelationUserOrganization
                    {
                        Id = IdentityHelper.NewSequentialGuid().ToString("N"),
                        OrganizationCode = relationOrganization.OrganizationCode,
                        OrganizationId = IocUnity.Get<RepositoryOrganization>().GetId(relationOrganization.OrganizationCode),
                        OrganizationIdO = IocUnity.Get<RepositoryOrganization>().GetId(relationOrganization.OrganizationCode),
                        UserId= userDto,
                        UserType= relationOrganization.UserType
                    };
                    relationUserOrganizations.Add(relationUserOrganization);
                }
            }
            return IocUnity.Get<RepositoryRelationUserOrganization>().Insert(relationUserOrganizations);
        }

        /// <summary>
        /// 
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
        public PagedList<RelationOrganizationDetailDto> GetRelationOrganizations(string OrganizationCode, string Name, int Page, int Size, 
            string OrganizationId, string TypeId, string RelationTypeId,IList<KeyValueItem> Conditions) {
            if (OrganizationCode.IsNullOrEmpty() && OrganizationId.IsNotNullOrEmpty())
                OrganizationCode = IocUnity.Get<RepositoryOrganization>().GetCode(OrganizationId);
            return IocUnity.Get<RepositoryRelationOrganization>().GetRelationOrganizations(OrganizationCode, Name, Page, Size, TypeId, RelationTypeId, GerateQuerySql2(Name, Conditions));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeId"></param>
        /// <param name="RelationTypeId"></param>
        /// <returns></returns>
        public IList<OrganizationAbstractDto> GetRelationMasterOrganizations(string TypeId, string RelationTypeId) {
            return IocUnity.Get<RepositoryOrganization>().GetRelationMasterOrganizations(TypeId, RelationTypeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationCode"></param>
        /// <param name="UserType"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public PagedList<RelationUserInfoDto> GetRelationOrganizationUsers(string OrganizationCode, int UserType, int Page, int Size) {
            return IocUnity.Get<RepositoryRelationUserOrganization>().GetRelationUsers(OrganizationCode, UserType, Page, Size);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RelationId"></param>
        /// <returns></returns>
        public int DeleteRelationUsers(string RelationId) {
            return IocUnity.Get<RepositoryRelationUserOrganization>().Delete(RelationId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="partnerDto"></param>
        /// <returns></returns>
        public OrganizationDto CorrelateOrganization(PartnerDto partnerDto) {
            OrganizationDto organization = IocUnity.Get<RepositoryOrganization>().GetById(partnerDto.PartnerUnitId).MapTo<OrganizationDto>();
            if (organization == null)
                return null;
            TRelationOrganizationForeign relationOrganizationForeign = new TRelationOrganizationForeign
            {
                Id=IdentityHelper.NewSequentialGuid().ToString("N"),
                OrganizationId=organization.Id,
                UnionId=partnerDto.PartnerId,
                UnionName=partnerDto.PartnerName
            };
            IocUnity.Get<RepositioryRelationOrganizationForeign>().Insert(relationOrganizationForeign);
            return organization;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationIds"></param>
        /// <returns></returns>
        public IList<OrganizationDto> GetOrganizations(IList<string> OrganizationIds) {
            return IocUnity.Get<RepositoryOrganization>().GetOrganizations(OrganizationIds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<OrganizationForeignDto> GetForeignDtos(IList<string> OrganizationIds=null) {
            return IocUnity.Get<RepositioryRelationOrganizationForeign>().GetForeignDtos(OrganizationIds); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relationOrganizations"></param>
        /// <returns></returns>
        public int AddRelationOrganizations(IList<RelationOrganizationDto> relationOrganizations) {
            IList<TRelationOrganization> relations = new List<TRelationOrganization>();
            foreach (RelationOrganizationDto relationDto in relationOrganizations) {
                TRelationOrganization relation = relationDto.MapTo<TRelationOrganization>();
                relation.Id = IdentityHelper.NewSequentialGuid().ToString("N");
                if (string.IsNullOrEmpty(relation.OrganizationId) && !string.IsNullOrEmpty(relation.OrganizationCode))
                    relation.OrganizationId = IocUnity.Get<RepositoryOrganization>().GetId(relation.OrganizationCode);
                if (string.IsNullOrEmpty(relation.OrganizationCode) && !string.IsNullOrEmpty(relation.OrganizationId))
                    relation.OrganizationCode = IocUnity.Get<RepositoryOrganization>().GetCode(relation.OrganizationId);
                if (string.IsNullOrEmpty(relation.RelationOrganizationId) && !string.IsNullOrEmpty(relation.RelationOrganizationCode))
                    relation.RelationOrganizationId = IocUnity.Get<RepositoryOrganization>().GetId(relation.RelationOrganizationCode);
                if (string.IsNullOrEmpty(relation.RelationOrganizationCode) && !string.IsNullOrEmpty(relation.RelationOrganizationId))
                    relation.RelationOrganizationCode = IocUnity.Get<RepositoryOrganization>().GetCode(relation.RelationOrganizationId);
                 if (string.IsNullOrEmpty(relation.RelationAreaId) && !string.IsNullOrEmpty(relation.RelationAreaCode))
                    relation.RelationAreaId = IocUnity.Get<RepositoryOrganization>().GetId(relation.RelationAreaCode);
                if (string.IsNullOrEmpty(relation.RelationAreaCode) && !string.IsNullOrEmpty(relation.RelationAreaId))
                    relation.RelationAreaCode = IocUnity.Get<RepositoryOrganization>().GetCode(relation.RelationAreaId);
                relations.Add(relation);
            }
            return IocUnity.Get<RepositoryRelationOrganization>().Insert(relations);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationCode"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>
        public PagedList<OrganizationAbstractDto> GetAll4sOrganizationsOfCode(string OrganizationCode, int Page, int Size, string OrganizationId) {
            return IocUnity.Get<RepositoryOrganization>().GetAll4sOrganizationsOfCode(OrganizationCode, Page, Size, OrganizationId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Name"></param>
        /// <param name="IsSub"></param>
        /// <param name="TypeName"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public PagedList<OrganizationDto> GetUnits(string UserId, string Name, bool IsSub, string TypeName, int Page, int Size)
        {
            return IocUnity.Get<RepositoryOrganization>().GetUnits(UserId, Name, IsSub, TypeName, Page, Size);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RelationIds"></param>
        /// <returns></returns>
        public int DeleteRelationOrganizations(IList<string> RelationIds) {
            return IocUnity.Get<RepositoryRelationOrganization>().DeleteRelationOrganizations(RelationIds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        public IList<OrganizationTypeDto> GetRelationMasterOrganizationTypes(string TypeId) {
            return IocUnity.Get<RepositoryRelationOrganization>().GetRelationMasterOrganizationTypes(TypeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public PagedList<OrganizationAbstractDto> GetV2UnitGroup(string name, int Page, int Size) {
            return IocUnity.Get<RepositoryOrganization>().GetAreas("1514814a05534c1cb1a1147a13e0cbad", Page, Size, name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <param name="SystemCode"></param>
        /// <returns></returns>
        public PagedList<OrganizationDto> GetRelevancyOrganizations(string Name, string SystemCode, int Page, int Size) {
            return IocUnity.Get<RepositoryOrganization>().GetRelevancyOrganizations(Name, SystemCode, Page, Size);
        }

        public IList<ScsjOrganizationDto> GetScsjOrganization(string key) {
            return IocUnity.Get<RepositoryOrganization>().GetScsjOrganization(key);
        }

        public OrganizationDto GetOrganizationByDName(string name) {
            return IocUnity.Get<RepositoryOrganization>().GetByDName(name);
        }

    }
}
