
namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Attribution
{
   /// <summary>
   /// 
   /// </summary>
    public class AttributionInfoDto
    {
        /// <summary>
        /// 属性ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 属性名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 属性类型ID
        /// </summary>
        public string AttributionTypeId { get; set; }
        /// <summary>
        /// 属性类型中文名
        /// </summary>
        public string ChineseName { get; set; }

        /// <summary>
        /// 属性类型英文名
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 属性控件类型
        /// </summary>
        public int WidgetType { get; set; }

        /// <summary>
        /// 属性控件提示
        /// </summary>
        public string WidgetTip { get; set; }

        /// <summary>
        /// 属性是否为必填
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 属性校验类型
        /// 0.手机号校验；1.email校验；2.数字校验；3.账号校验
        /// </summary>
        public int ValidVerifyType { get; set; }

        /// <summary>
        /// 选项列表
        /// </summary>
        public string OptionItems { get; set; }

        /// <summary>
        /// 机构类型ID
        /// </summary>
        public string OrganizationTypeId { get; set; }

        /// <summary>
        /// 是否为基础类型
        /// </summary>
        public bool IsBase { get; set; }

        /// <summary>
        /// 控件对应的API名
        /// </summary>
        public string ApiName { get; set; }
    }
}
