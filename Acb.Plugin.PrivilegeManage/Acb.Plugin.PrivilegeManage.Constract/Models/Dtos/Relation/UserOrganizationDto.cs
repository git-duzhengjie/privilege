namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Relation
{
    /// <summary>
    /// 用户机构关系对象
    /// </summary>
    public class UserOrganizationDto
    {
        /// <summary>
        /// 机构ID
        /// </summary>
        public string OrganizationId { get; set; }

        /// <summary>
        /// 层级ID
        /// 0:机构；1：部门；2：岗位
        /// </summary>
        public int HierarchyType { get; set; }

        /// <summary>
        /// 机构类型ID
        /// </summary>
        public string OrganizationType { get; set; }
    }
}
