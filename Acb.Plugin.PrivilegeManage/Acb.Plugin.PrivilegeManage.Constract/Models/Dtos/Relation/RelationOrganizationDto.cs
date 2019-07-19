namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Relation
{
    /// <summary>
    /// 用户机构关系对象
    /// </summary>
    public class RelationOrganizationDto
    {
        /// <summary>
        /// 机构ID
        /// </summary>
        public string OrganizationId { get; set; }

        /// <summary>
        /// 关联机构ID
        /// </summary>
        public string RelationOrganizationId { get; set; }


        /// <summary>
        /// 机构ID
        /// </summary>
        public string OrganizationCode { get; set; }

        /// <summary>
        /// 关联机构ID
        /// </summary>
        public string RelationOrganizationCode { get; set; }

        /// <summary>
        /// 关联区域ID
        /// </summary>
        public string RelationAreaId { get; set; }


        /// <summary>
        /// 关联区域CODE
        /// </summary>
        public string RelationAreaCode { get; set; }
    }
}
