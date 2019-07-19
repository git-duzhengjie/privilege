using CDynamic.Dapper;
using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;
using System;

namespace Acb.Plugin.PrivilegeManage.Models.Entities
{
    /// <summary>
    /// 
    /// </summary>
    [Naming("pm_relation_user_organization")]
    public class TRelationUserOrganization:IEntity
    {
        /// <summary>
        /// 用户机构关系ID
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public string OrganizationId { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public string OrganizationIdO { get; set; }

        /// <summary>
        /// 部门ID 
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 岗位ID
        /// </summary>
        public string PositionId { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public string OrganizationCode { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 所属岗位
        /// </summary>
        public string PositionCode { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 用户类型：0：默认；1：驻店员；2：销售员
        /// </summary>
        public int UserType { get; set; }
    }
}
