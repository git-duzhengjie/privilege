using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Organization;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Privilege;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.User;
using Acb.Plugin.PrivilegeManage.Models.View.Organization;
using Acb.Plugin.PrivilegeManage.Models.View.User;
using Dynamic.Core.Extensions;
using Dynamic.Core.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Acb.Plugin.PrivilegeManage.Common
{
    /// <summary>
    /// 用于某些特殊字符串操作
    /// </summary>
    public static class StringManage
    {
        

        /// <summary>
        /// 通过判断实体中的值是否为空，生成条件判断sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GenerateSqlFromEntity<T>(T t) {
            string s = "";
            foreach (System.Reflection.PropertyInfo p in t.GetType().GetProperties()) {
                if (p.GetValue(t) != null && !string.IsNullOrWhiteSpace(p.GetValue(t).ToString()) && p.GetValue(t).ToString().CompareTo("-1") != 0)
                {
                    if (p.Name == "Name")
                        s += " and " + $"[{p.Name}] like '%{p.GetValue(t).ToString()}%'";
                    else if (p.Name.Contains("Code"))
                        s += $" and [{p.Name}] like '{p.GetValue(t).ToString()}%'";
                    else
                        s += " and " + $"[{p.Name}] = @{p.Name}";
                }
            }
            s = RemovePrefix(s, " and ");
            if (!string.IsNullOrWhiteSpace(s))
                s = "where " + s;
            return s;
        }

        public static string GenerateSqlFromEntity<T>(T t, IList<string> JsonColumns, string  JsonTableName)
        {
            string s = "";
            foreach (System.Reflection.PropertyInfo p in t.GetType().GetProperties())
            {
                if (p.GetValue(t) != null && !string.IsNullOrWhiteSpace(p.GetValue(t).ToString()) && p.GetValue(t).ToString().CompareTo("-1") != 0)
                {
                    if (JsonColumns.Contains(p.Name))
                        s += " and " + $"{JsonTableName}->>'{p.Name}' = @{p.Name}";
                    else
                        s += " and " + $"[{p.Name}] = @{p.Name}";
                }
            }
            s = RemovePrefix(s, " and ");
            if (!string.IsNullOrWhiteSpace(s))
                s = "where " + s;
            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T EmptyStringSetNull<T>(T t) {
            foreach (System.Reflection.PropertyInfo p in t.GetType().GetProperties()) {
                if (p.GetValue(t) != null && string.IsNullOrEmpty(p.GetValue(t).ToString()))
                    p.SetValue(t, null);
            }
            return t;
        }

        /// <summary>
        /// 通过判断实体中的值是否为空，生成条件判断sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="LikeName"></param>
        /// <returns></returns>
        public static string GenerateSqlFromEntity<T>(T t, string LikeName)
        {
            string s = "";
            foreach (System.Reflection.PropertyInfo p in t.GetType().GetProperties())
            {
                if (p.GetValue(t) != null && !string.IsNullOrWhiteSpace(p.GetValue(t).ToString()) && p.GetValue(t).ToString().CompareTo("-1") != 0)
                {
                    if (p.Name.CompareTo(LikeName) != 0)
                        s += " and " + $"[{p.Name}] = @{p.Name}";
                    else
                        s += " and " + $"[{p.Name}] like '%{p.GetValue(t).ToString()}%'";
                }
            }
            s = RemovePrefix(s, " and ");
            if (!string.IsNullOrWhiteSpace(s))
                s = "where " + s;
            return s;
        }

        /// <summary>
        /// 通过判断实体中的值是否为空，生成条件判断sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="LikeName"></param>
        /// <param name="JsonColumn"></param>
        /// <param name="JsonColumnName"></param>
        /// <returns></returns>
        public static string GenerateSqlFromEntity<T>(T t, string LikeName, string JsonColumn, string JsonColumnName)
        {
            string s = "";
            foreach (System.Reflection.PropertyInfo p in t.GetType().GetProperties())
            {
                if (p.GetValue(t) != null && !string.IsNullOrWhiteSpace(p.GetValue(t).ToString()) && p.GetValue(t).ToString().CompareTo("-1") !=0 )
                {
                    if (p.Name.CompareTo(LikeName) == 0)
                        s += " and " + $"[{p.Name}] like '%{p.GetValue(t).ToString()}%'";
                    else if (p.Name.CompareTo(JsonColumnName) == 0) {
                        s += " and " + $"[{JsonColumn}]->>'{JsonColumnName}'= @{ p.Name}";
                    }
                    else
                        s += " and " + $"[{p.Name}] = @{p.Name}";
                }
            }
            s = RemovePrefix(s, " and ");
            if (!string.IsNullOrWhiteSpace(s))
                s = "where " + s;
            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string RemovePrefix(string val, string prefix) {
            string strPre = $"^({prefix})";
            return Regex.Replace(val, strPre, "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string DictionaryToJson(Dictionary<string, string> dict)
        {
            var entries = dict.Select(d =>
                string.Format("\"{0}\": \"{1}\"", d.Key, d.Value));
            return "{" + string.Join(",", entries) + "}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userViews"></param>
        /// <returns></returns>
        public static IList<UserInfoDto> UserViewToUserDto(IList<UserInfoView> userViews) {
            IList<UserInfoDto> userInfoDtos = new List<UserInfoDto>();
            int i = 0;
            for(int k = 0; k < userViews.Count; k=i)
            {
                IList<UserPositionDto> userPositionDtos = new List<UserPositionDto>();
                for (int j = i; j < userViews.Count; j++) {
                    if (userViews[j].OrganizationId != null || userViews[j].DepartmentId != null || userViews[j].PositionId != null) {
                        UserPositionDto userPositionDto = new UserPositionDto
                        {
                            OrganizationId = userViews[j].OrganizationId,
                            DepartmentId = userViews[j].DepartmentId,
                            PositionId = userViews[j].PositionId,
                            OrganizationCode = userViews[j].OrganizationCode,
                            DepartmentCode = userViews[j].DepartmentCode,
                            PositionCode = userViews[j].PositionCode,
                            OrganizationName = userViews[j].OrganizationName,
                            DepartmentName = userViews[j].DepartmentName,
                            PositionName = userViews[j].PositionName,
                            UserType=userViews[j].UserType,
                        };
                        userPositionDtos.Add(userPositionDto);
                    }
                    if (j + 1 == userViews.Count || userViews[j + 1].Id != userViews[j].Id) {
                        i = j + 1;
                        break;
                    }
                }
                UserInfoDto userInfoDto = AutoMapperExtensions.MapTo<UserInfoDto>(userViews[k]);
                userInfoDto.OrganizationIds = userPositionDtos;
                userInfoDtos.Add(userInfoDto);
            }
            return userInfoDtos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userViews"></param>
        /// <returns></returns>
        public static IList<RelationUserInfoDto> UserViewToUserDto(IList<RelationUserInfoView> userViews)
        {
            IList<RelationUserInfoDto> userInfoDtos = new List<RelationUserInfoDto>();
            int i = 0;
            for (int k = 0; k < userViews.Count; k = i)
            {
                IList<UserPositionDto> userPositionDtos = new List<UserPositionDto>();
                for (int j = i; j < userViews.Count; j++)
                {
                    if (userViews[j].OrganizationId != null || userViews[j].DepartmentId != null || userViews[j].PositionId != null)
                    {
                        UserPositionDto userPositionDto = new UserPositionDto
                        {
                            OrganizationId = userViews[j].OrganizationId,
                            DepartmentId = userViews[j].DepartmentId,
                            PositionId = userViews[j].PositionId,
                            OrganizationCode = userViews[j].OrganizationCode,
                            DepartmentCode = userViews[j].DepartmentCode,
                            PositionCode = userViews[j].PositionCode,
                            OrganizationName = userViews[j].OrganizationName,
                            DepartmentName = userViews[j].DepartmentName,
                            PositionName = userViews[j].PositionName,
                            UserType = userViews[j].UserType
                        };
                        userPositionDtos.Add(userPositionDto);
                    }
                    if (j + 1 == userViews.Count || userViews[j + 1].UserId != userViews[j].UserId)
                    {
                        i = j + 1;
                        break;
                    }
                }
                RelationUserInfoDto userInfoDto = AutoMapperExtensions.MapTo<RelationUserInfoDto>(userViews[k]);
                userInfoDto.OrganizationIds = userPositionDtos;
                userInfoDtos.Add(userInfoDto);
            }
            return userInfoDtos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionInfoViews"></param>
        /// <returns></returns>
        public static IList<PositionInfoDto> PositionViewToPositionInfo(IList<PositionInfoView> positionInfoViews) {
            IList<PositionInfoDto> positionInfoDtos = new List<PositionInfoDto>();
            foreach (PositionInfoView positionInfoView in positionInfoViews) {
                PositionInfoDto positionInfoDto = AutoMapperExtensions.MapTo<PositionInfoDto>(positionInfoView);
                positionInfoDto.Roles = SerializationUtility.JsonToObject<IList<AbstractDto>>(positionInfoView.Roles);
                positionInfoDtos.Add(positionInfoDto);
            }
            return positionInfoDtos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IList<RoleDto> RemoveNull(IList<RoleDto> t) {
            IList<RoleDto> ts = new List<RoleDto>();
            foreach (RoleDto _t in t) {
                if (_t.Id != null)
                    ts.Add(_t);
            }
            return ts;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IList<PrivilegeDto> RemoveNull(IList<PrivilegeDto> t)
        {
            IList<PrivilegeDto> ts = new List<PrivilegeDto>();
            foreach (PrivilegeDto _t in t)
            {
                if (_t.Id != null)
                    ts.Add(_t);
            }
            return ts;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IList<OrganizationAbstractDto> RemoveNull(IList<OrganizationAbstractDto> t) {
            IList<OrganizationAbstractDto> ts = new List<OrganizationAbstractDto>();
            foreach (OrganizationAbstractDto _t in t) {
                if (_t.Id != null)
                    ts.Add(_t);
            }
            return ts;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        static public string EncryptPassword(string password) {
            return (password + "o9MuI0-fi_eL8aKSlyTc1Bgjpprs").Md5();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        static public string EncryptPasswordMd5(string password)
        {
            return password.Md5();
        }

        /// <summary>
        /// 通过实体生成查询条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GenerateQuerySqlFromEntity<T>(this T t)
        {
            string s = "";
            foreach (System.Reflection.PropertyInfo p in t.GetType().GetProperties())
            {
                if (p.GetValue(t) != null && !string.IsNullOrWhiteSpace(p.GetValue(t).ToString()) && p.Name != "Page" && p.Name != "Size")
                {
                    if (p.Name.Contains("Code"))
                        s += " and " + $"[{p.Name}] like '{p.GetValue(t)}%'";
                    else
                        s += " and " + $"[{p.Name}] = @{p.Name}";
                }
            }
            s = RemovePrefix(s, " and ");
            if (!string.IsNullOrWhiteSpace(s))
                s = "where " + s;
            return s;
        }

        /// <summary>
        /// 通过实体生成更新sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GenerateUpdateSqlFromEntity<T>(this T t)
        {

            string s = "";
            foreach (System.Reflection.PropertyInfo p in t.GetType().GetProperties())
            {
                if (p.GetValue(t) != null && !string.IsNullOrWhiteSpace(p.GetValue(t).ToString()))
                {
                    s += "," + $"[{p.Name}] = @{p.Name}";
                }
            }
            s = RemovePrefix(s, ",");
            if (!string.IsNullOrWhiteSpace(s))
                s = s + " where [Id]=@Id";
            return s;
        }

    }
}
