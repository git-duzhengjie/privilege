﻿using CDynamic.Dapper;
using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;
using System;

namespace Acb.Plugin.PrivilegeManage.Models.Entities
{
    /// <summary> 机构类型表 </summary>
    [Naming("pm_organization_type")]
    public class TOrganizationType:IEntity
    {
        /// <summary> 机构类型ID </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 机构类型码
        /// </summary>
        public string Code { get; set; }

        /// <summary> 机构类型名 </summary>
        public string Name { get; set; }

        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemId { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// 是否关联
        /// </summary>
        public bool IsRelevancy { get; set; }


        /// <summary> 创建时间 </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 是否具有子级机构
        /// </summary>
        public bool IsHasChildren { get; set; }
    }
}
