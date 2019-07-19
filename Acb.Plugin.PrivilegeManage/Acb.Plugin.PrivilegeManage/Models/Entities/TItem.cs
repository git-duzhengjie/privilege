using CDynamic.Dapper.Attr;
using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Models.Entities
{
    /// <summary>
    /// 
    /// </summary>
    [Naming("pm_item")]
    public class TItem:IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        [Json]
        public string SystemJsonItem { get; set; }

        /// <summary>
        /// 菜单名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemId { get; set; }

        /// <summary>
        /// 菜单状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

       

        /// <summary>
        /// 前端系统code
        /// </summary>
        public string FrontSystemCode { get; set; }

        /// <summary>
        /// 前端系统名
        /// </summary>
        public string FrontSystemName { get; set; }
    }
}
