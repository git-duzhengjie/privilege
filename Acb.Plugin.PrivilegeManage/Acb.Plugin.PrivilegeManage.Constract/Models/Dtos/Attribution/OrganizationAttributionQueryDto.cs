namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Attribution
{
    /// <summary>
    /// 机构属性对象
    /// </summary>
    public class OrganizationAttributionQueryDto
    {
        /// <summary>
        /// 属性英文名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public object Value { get; set; }
    }
}
