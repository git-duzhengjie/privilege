using CDynamic.Dapper;
using CDynamic.Dapper.Attr;
using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;
using System;

namespace Acb.Plugin.PrivilegeManage.Models.Entities
{
    /// <summary> 机构表 </summary>
    [Naming("pm_organization")]
    public class TDepartmentUpdate:IEntity
    {
        /// <summary> 机构ID </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
        
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
        /// 是否为区域
        /// </summary>
        public bool IsArea { get; set; }



    }
}
