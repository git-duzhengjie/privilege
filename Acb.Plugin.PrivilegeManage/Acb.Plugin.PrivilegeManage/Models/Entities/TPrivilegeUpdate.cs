using System;
using CDynamic.Dapper;
using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;

namespace Acb.Plugin.PrivilegeManage.Models.Entities
{
    /// <summary> 权限表 </summary>
    [Naming("pm_privilege")]
    public class TPrivilegeUpdate:IEntity
    {
        /// <summary> 权限ID </summary>
        [Key]
        public string Id { get; set; }

        /// <summary> 权限名 </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OriginalCode { get; set; }

        /// <summary>
        /// 权限描述
        /// </summary>
        public string Instruction { get; set; }

    }
}
