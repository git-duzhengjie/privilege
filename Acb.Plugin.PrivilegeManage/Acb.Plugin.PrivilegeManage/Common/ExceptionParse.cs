using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionParse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExMessage"></param>
        /// <returns></returns>
        public static string ParseString(String ExMessage)
        {
            if (ExMessage.Contains("un_cn"))
                return "属性类型中文名重复";
            else if (ExMessage.Contains("un_en"))
                return "属性类型英文名重复";
            else if (ExMessage.Contains("un_or_na"))
                return "名字重复";
            else if (ExMessage.Contains("un_orp_na"))
                return "机构类型名重复";
            else if (ExMessage.Contains("un_pr_na"))
                return "权限名重复";
            else if (ExMessage.Contains("un_pr_gr_na"))
                return "权限模块名重复";
            else if (ExMessage.Contains("un_ro_na"))
                return "角色名重复";
            else if (ExMessage.Contains("un_sys_na"))
                return "系统名重复";
            else if (ExMessage.Contains("un_sys_en"))
                return "系统英文名重复";
            else if (ExMessage.Contains("un_opd"))
                return "已经有了该OpenId";
            else if (ExMessage.Contains("un_und"))
                return "已经有了该UnionId";
            else if (ExMessage.Contains("un_acc"))
                return "已经有了该账号名";
            else if (ExMessage.Contains("un_tel"))
                return "该电话号已经注册";
            else if (ExMessage.Contains("un_em"))
                return "该邮箱已经注册";
            else if (ExMessage.Contains("fk_rl_pv"))
                return "该权限已经被绑定到角色，不能被删除！如需要删除需要先解除该权限绑定";
            else if (ExMessage.Contains("fk_rur_rd"))
                return "该角色已经被绑定到用户，不能被删除！如需要删除需要先解除该绑定";
            else if (ExMessage.Contains("fk_rp_rd"))
                return "该角色已经被绑定到岗位，不能被删除！如需要删除需要先解除该绑定";
            else if (ExMessage.Contains("un_rl_uo"))
                return "该用户已经被绑定到该机构";
            else if (ExMessage.Contains("fk_ps_or"))
                return "该机构已经关联用户，不能被删除！如需要删除需要先删除该用户！";
            else if (ExMessage.Contains("rl_ra"))
                return "该部门已经被其他机构关联，不能删除！如需删除需要先解除该关联";
            else if (ExMessage.Contains("rl_ro"))
                return "该机构已经被其他机构关联，不能删除！如需删除需要先解除该关联";
            else if (ExMessage.Contains("un_pr_co"))
                return "该权限code已经存在！";
            else if (ExMessage.Contains("22P02: invalid input syntax for type json"))
                return "关联菜单输入的json语法格式有误！";
            else if (ExMessage.Contains("un_rsb"))
                return "该机构已经被关联到该区域！";
            else if (ExMessage.Contains("un_ror"))
                return "该机构已经被关联到该机构或者该机构已经被关联到该机构下面某个区域，不能同时关联多个区域！";
            else if (ExMessage.Contains("fk_type_id"))
                return "该机构类型下面还有机构，请先删除机构再进行删除！";
            else if (ExMessage.Contains("un_co_sy"))
                return "应用code重复";
            else
                return ExMessage;
         }
    }
}
