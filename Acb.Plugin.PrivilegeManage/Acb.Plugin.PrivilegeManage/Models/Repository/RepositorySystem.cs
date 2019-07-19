using Acb.Plugin.PrivilegeManage.Models.Entities;
using Acb.MiddleWare.Data.DB;
using Dynamic.Core.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Dynamic.Core.Extensions;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.System;

namespace Acb.Plugin.PrivilegeManage.Models.Repository
{
    /// <summary>
    /// 系统数据库操作
    /// </summary>
    public class RepositorySystem : DBBase<TSystem>
    {
        /// <summary>
        /// 初始数据库配置
        /// </summary>
        /// <param name="dBConfig"></param>
        public RepositorySystem(DBCfgViewModel dBConfig) : base(dBConfig)
        {
        }

        /// <summary>
        /// 查询系统信息
        /// </summary>
        /// <returns></returns>
        public IList<TSystem> GetAll()
        {
            return this.DapperRepository.Query().ToList();
        }

        /// <summary>
        /// 根据ID查询系统信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TSystem GetById(string Id)
        {
            return this.DapperRepository.QueryById(Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public string GetId(string Code) {
            var type = typeof(TSystem);
            var sql = $"select [Id] from {type.PropName()} where [Code]=@Code";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { Code });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GetCode(string Id) {
            var type = typeof(TSystem);
            var sql = $"select [Code] from {type.PropName()} where [Id]=@Id";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { Id });
        }

        /// <summary>
        /// 根据ID删除系统信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteById(string Id)
        {
            return this.DapperRepository.Delete(Id);
        }

        /// <summary>
        /// 插入系统信息
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public int Insert(TSystem system)
        {
            return this.DapperRepository.Insert(system, excepts: new[] { nameof(TSystem.CreateTime) });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public int Update(TSystemUpdate system) {
            return this.DapperRepository.Update(system);
        }

        /// <summary>
        /// 获取系统编码
        /// </summary>
        /// <returns></returns>
        public string GetNextSystemCode()
        {
            var type = typeof(TSystem);
            string sql = $"select COALESCE(max([Code]),'0') from {type.PropName()}";
            string Code = this.DapperRepository.QueryFirstOrDefault<string>(sql);
            if (Code == "0")
                return "00";
            return string.Format("{0:D2}", int.Parse(Code) + 1);
        }

    }
}
