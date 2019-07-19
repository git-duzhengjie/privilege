namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.System
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemAddDto { 

        /// <summary> 系统名 </summary>
        public string Name { get; set; }

        /// <summary>
        /// 系统Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// LogoUrl
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Instruction { get; set; }
    }
}
