using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization
{
    /// <summary>
    /// 合伙人信息
    /// </summary>
    public class PartnerDto
    {
        /// <summary>合伙人ID</summary>
        public string PartnerId { get; set; }
        /// <summary>合伙人名称</summary>
        public string PartnerName { get; set; }
        /// <summary>合伙人电话</summary>
        public string PartnerMobile { get; set; }
        /// <summary>邮箱</summary>
        public string PartnerEmail { get; set; }
        /// <summary>合伙人机构id</summary>
        public string PartnerUnitId { get; set; }
        /// <summary>合伙人机构名称</summary>
        public string PartnerUnitName { get; set; }
        /// <summary>统一社会信用代码</summary>
        public string PartnerCode { get; set; }
        /// <summary>身份证号码</summary>
        public string PartnerCard { get; set; }
        /// <summary>营业执照附件地址</summary>
        public string PartnerUrl { get; set; }
        /// <summary>错误信息</summary>
        public string Message { get; set; }
    }
}
