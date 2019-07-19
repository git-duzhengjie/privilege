using CDynamic.Dapper;
using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;
using System;

namespace Acb.Plugin.PrivilegeManage.Models.Entities
{
    /// <summary> 属性类型表 </summary>
    [Naming("pm_attribution_type")]
    public class TAttributionType:IEntity
    {
        /// <summary> 属性类型ID </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizationTypeId { get; set; }

        /// <summary> 属性类型标题 </summary>
        public string ChineseName { get; set; }

        /// <summary>
        /// 属性类型中文名
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary> 属性控件类型 </summary>
        public int WidgetType{ get; set; }

        /// <summary> 属性控件提示 </summary>
        public string WidgetTip { get; set; }

        /// <summary> 属性是否为必填 </summary>
        public bool IsRequired { get; set; }

        /// <summary> 属性校验类型 </summary>
        public int ValidVerifyType { get; set; }

        /// <summary>
        /// 选项列表
        /// </summary>
        public string OptionItems { get; set; }

        /// <summary>
        /// 是否为默认字段
        /// </summary>
        public bool IsBase { get; set; }

        /// <summary>
        /// 是否为专属字段
        /// </summary>
        public bool IsSearch { get; set; }

        /// <summary>
        /// 控件对应的API名
        /// </summary>
        public string ApiName { get; set; }

        /// <summary> 属性类型创建时间 </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
