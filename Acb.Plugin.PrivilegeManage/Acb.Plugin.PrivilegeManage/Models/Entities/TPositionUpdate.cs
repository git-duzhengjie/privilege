using CDynamic.Dapper;
using CDynamic.Dapper.Attr;
using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;
using System;

namespace Acb.Plugin.PrivilegeManage.Models.Entities
{
    /// <summary> 机构表 </summary>
    [Naming("pm_organization")]
    public class TPositionUpdate:IEntity
    {
        /// <summary> 机构ID </summary>
        [Key]
        public string Id { get; set; }
        
        /// <summary> 机构名 </summary>
        public string Name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// 扩展属性
        /// </summary>
        [Json]
        public string ExtendAttribution { get; set; }
    }
}
