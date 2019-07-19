using CDynamic.Dapper;
using CDynamic.Dapper.Attr;
using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;
using System;

namespace Acb.Plugin.PrivilegeManage.Models.Entities
{
    /// <summary> 机构表 </summary>
    [Naming("pm_organization")]
    public class TOrganization:IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public TOrganization() {
            this.ExtendAttribution = "{}";
        }
        /// <summary> 机构ID </summary>
        [Key]
        public string Id { get; set; }

        
        /// <summary>
        /// 机构码
        /// </summary>
        public string Code { get; set; }

        /// <summary> 父级机构ID </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 父级机构码
        /// </summary>
        public string ParentCode { get; set; }

        /// <summary> 机构名 </summary>
        public string Name { get; set; }

        /// <summary> 机构类型ID </summary>
        public string TypeId { get; set; }

        /// <summary> 机构层级类型 </summary>
        public int HierarchyType { get; set; }

        /// <summary>
        /// 是否有子级
        /// </summary>
        public bool IsHasChildren { get; set; }

        /// <summary> 创建时间 </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 扩展属性
        /// </summary>
        [Json]
        public string ExtendAttribution { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// 是否含有子级机构
        /// </summary>
        public bool IsHasChildOrganization { get; set; }

        /// <summary>
        /// 是否为区域
        /// </summary>
        public bool IsArea { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CloneId { get; set; }
    }
}
