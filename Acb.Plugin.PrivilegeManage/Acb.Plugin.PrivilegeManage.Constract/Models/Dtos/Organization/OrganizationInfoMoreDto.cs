namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization
{
    /// <summary>
    /// 机构视图
    /// </summary>
    public class OrganizationInfoMoreDto
    {
        /// <summary>
        /// 机构ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 机构码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 父级机构ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 父级机构码
        /// </summary>
        public string ParentCode { get; set; }

        /// <summary>
        /// 父级机构名
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 机构名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 机构类型ID
        /// </summary>
        public string TypeId { get; set; }

        /// <summary>
        /// 层级类型
        /// </summary>
        public int HierarchyType { get; set; }

        /// <summary>
        /// 是否含有子级
        /// </summary>
        public bool IsHasChildren { get; set; }

        /// <summary>
        /// 是否含有子级机构
        /// </summary>
        public bool IsHasChildOrganization { get; set; }

        /// <summary>
        /// 扩展属性
        /// </summary>
        public string ExtendAttribution { get; set; }

        /// <summary>
        /// 是否关联
        /// </summary>
        public bool IsRelevancy { get; set; }

    }
}
