namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization
{
    /// <summary>
    /// 机构视图
    /// </summary>
    public class RelationOrganizationDetailDto:OrganizationDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string RelationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        new public bool IsHasChildOrganization;

        /// <summary>
        /// 
        /// </summary>
        new public bool IsHasChildren;

    }
}
