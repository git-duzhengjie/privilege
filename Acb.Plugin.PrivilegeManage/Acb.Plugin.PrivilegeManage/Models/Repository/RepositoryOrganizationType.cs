using Acb.Plugin.PrivilegeManage.Models.Entities;
using Acb.MiddleWare.Data.DB;
using Dynamic.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CDynamic.Dapper;
using Dynamic.Core.Extensions;

namespace Acb.Plugin.PrivilegeManage.Models.Repository
{
    /// <summary>
    /// 机构类型数据库操作
    /// </summary>
    public class RepositoryOrganizationType:DBBase<TOrganizationType>
    {
        /// <summary>
        /// 初始数据库配置
        /// </summary>
        /// <param name="dBConfig"></param>
        public RepositoryOrganizationType(DBCfgViewModel dBConfig) : base(dBConfig) { }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<TOrganizationType> GetAll() {
            return this.DapperRepository.Query().ToList();
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TOrganizationType GetById(string Id) {
            return this.DapperRepository.QueryById(Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SystemId"></param>
        /// <param name="TypeName"></param>
        /// <returns></returns>
        public string GetTypeId(string SystemId, string TypeName) {
            var type = typeof(TOrganizationType);
            string sql = $"select [Id] from {type.PropName()} where [SystemId]=@SystemId and [Name]=@TypeName";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { SystemId, TypeName });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Column"></param>
        /// <param name="ColumnValue"></param>
        /// <param name="WhereColumn"></param>
        /// <param name="WhereValue"></param>
        /// <returns></returns>
        public int Update(string Column, object ColumnValue, string WhereColumn, string WhereValue) {
            var type = typeof(TOrganizationType);
            string sql = $"update {type.PropName()} set [{Column}]=@ColumnValue where [{WhereColumn}]=@WhereValue";
            return this.DapperRepository.ExcuteOriCommand(sql, true, new { ColumnValue, WhereValue });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SystemId"></param>
        /// <param name="SystemCode"></param>
        /// <returns></returns>
        public IList<TOrganizationType> GetBySystemId(string SystemId, string SystemCode) {
            var type = typeof(TOrganizationType);
            var typeS = typeof(TSystem);
            string sql;
            if (string.IsNullOrEmpty(SystemCode))
            {
                sql = $"select {type.Columns()} from {type.PropName()} where [SystemId]=@SystemId";
                return this.DapperRepository.Query(sql, true, new { SystemId }).ToList();
            }
            else
                sql = $@"select t1.* from (select * from {type.PropName()}) t1 left join {typeS.PropName()} t2 on 
                        t1.[SystemId]=t2.[Id] where t2.[Code]=@SystemCode";
                return this.DapperRepository.Query(sql, true, new { SystemCode }).ToList();
        }

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteById(string Id) {
            return this.DapperRepository.Delete(Id);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="organizationType"></param>
        /// <returns></returns>
        public int Insert(TOrganizationType organizationType) {
            return this.DapperRepository.Insert(organizationType, excepts: new[] {  nameof(TOrganizationType.CreateTime) });
        }

        /// <summary>
        /// 获取机构类型编码
        /// </summary>
        /// <param name="SystemId"></param>
        /// <returns></returns>
        public string GetNextOrganizationTypeCode(string SystemId) {
            var typeS = typeof(TSystem);
            var typeT = typeof(TOrganizationType);
            string sql = $"select [Code] from {typeS.PropName()} where [Id]=@SystemId";
            string SystemCode = this.DapperRepository.QueryFirstOrDefault<string>(sql, new { SystemId });
            string like = SystemCode + "%";
            sql = $"select COALESCE(max([Code]), '0') from {typeT.PropName()} where [Code] like @like";
            string code = this.DapperRepository.QueryFirstOrDefault<string>(sql, new { like });
            if (code == "0")
                return SystemCode + "000";
            else
                return SystemCode + string.Format("{0:D3}", int.Parse(code.Substring(2, 3)) + 1);
        }

        /// <summary>
        /// 更新机构类型
        /// </summary>
        /// <param name="organizationType"></param>
        /// <returns></returns>
        public int Update(TOrganizationTypeUpdate organizationType) {
            return this.DapperRepository.Update(organizationType);
        }
    }
}
