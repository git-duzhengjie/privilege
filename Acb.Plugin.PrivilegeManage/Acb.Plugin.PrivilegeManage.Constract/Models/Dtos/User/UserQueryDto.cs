
using System.Collections.Generic;

namespace Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User
{
    /// <summary>
    /// 用户查询信息
    /// </summary>
    public class UserQueryDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 第三方用户ID
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 第三方UnitID
        /// </summary>
        public string UnionId { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 电话号
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 状态:-1:不限；0：禁用；1：启用
        /// </summary>
        public int State { get; set; }


        /// <summary>
        /// 渠道号
        /// </summary>
        public int Channel { get; set; }

        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SystemCode { get; set; }

        /// <summary>
        /// 所属机构
        /// </summary>
        public string OrganizationCode { get; set; }
       
        /// <summary>
        /// 所属部门
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 所属岗位
        /// </summary>
        public string PositionCode { get; set; }

        /// <summary>
        /// 所属机构
        /// </summary>
        public string OrganizationId { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 所属岗位
        /// </summary>
        public string PositionId { get; set; }

        /// <summary>
        /// 所属机构类型ID
        /// </summary>
        public string TypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SessionKey { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int? UserType { get; set; }
    }
}
