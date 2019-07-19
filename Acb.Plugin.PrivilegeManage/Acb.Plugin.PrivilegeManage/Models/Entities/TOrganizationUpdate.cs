using CDynamic.Dapper;
using CDynamic.Dapper.Attr;
using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;
using System;

namespace Acb.Plugin.PrivilegeManage.Models.Entities
{
    /// <summary> 机构表 </summary>
    [Naming("pm_organization")]
    public class TOrganizationUpdate:IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public TOrganizationUpdate() {
            this.ExtendAttribution = "{}";
        }
        /// <summary> 机构ID </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
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

        /// <summary>
        /// 扩展属性
        /// </summary>
        [Json]
        public string ExtendAttribution { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
