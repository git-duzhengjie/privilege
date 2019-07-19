using Acb.Plugin.PrivilegeManage.Models.Entities;
using Acb.MiddleWare.Data.DB;
using Dynamic.Core.ViewModel;
using CDynamic.Dapper;
using Dynamic.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Models.Repository
{
    /// <summary>
    /// 权限业务
    /// </summary>
    public class RepositoryPrivilege:DBBase<TPrivilege>
    {
        /// <summary>
        /// 初始数据库配置
        /// </summary>
        /// <param name="dBConfig"></param>
        public RepositoryPrivilege(DBCfgViewModel dBConfig) : base(dBConfig) { }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="privilege">权限对象</param>
        /// <returns></returns>
        public int Insert(TPrivilege privilege) {
            return this.DapperRepository.Insert(privilege, excepts: new[] { nameof(TPrivilege.CreateTime) });
        }

        /// <summary>
        /// 批量添加权限
        /// </summary>
        /// <param name="privileges"></param>
        /// <returns></returns>
        public int Insert(IList<TPrivilege> privileges)
        {
            int count = 0;
            foreach (TPrivilege privilege in privileges) {
                count += this.DapperRepository.Insert(privilege, excepts: new[] { nameof(TPrivilege.CreateTime) });
            }
            return count;
        }

        /// <summary>
        /// 根据权限组ID查询权限信息
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public IList<TPrivilege> GetByGroupId(string GroupId) {
            var type = typeof(TPrivilege);
            string sql = $"select {type.Columns()} from { type.PropName()} where [GroupId]=@GroupId";
            return this.DapperRepository.Query(sql,true,new { GroupId }).ToList();
        }

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public int Update(TPrivilegeUpdate update) {
            return this.DapperRepository.Update(update);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PrivilegeId"></param>
        /// <returns></returns>
        public int Delete(string PrivilegeId) {
            return this.DapperRepository.Delete(PrivilegeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PrivilegeId"></param>
        /// <returns></returns>
        public string GetSystemCode(string PrivilegeId) {
            var type = typeof(TPrivilege);
            var typeG = typeof(TPrivilegeGroup);
            var typeS = typeof(TSystem);
            string sql = $@"select t3.[Code] from (select [GroupId] from {type.PropName()} where [Id]=@PrivilegeId) 
                            t1 left join {typeG.PropName()} t2 on t2.[Id]=t1.[GroupId] 
                            left join {typeS.PropName()} t3 on t3.[Id]=t2.[SystemId]";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { PrivilegeId });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PrivilegeId"></param>
        /// <returns></returns>
        public string GetPrivilegeCode(string PrivilegeId) {
            var type = typeof(TPrivilege);
            string sql = $"select [Code] from {type.PropName()} where [Id]=@PrivilegeId";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { PrivilegeId });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PrivilegeCode"></param>
        /// <returns></returns>
        public string GetPrivilegeId(string PrivilegeCode)
        {
            var type = typeof(TPrivilege);
            string sql = $"select [Id] from {type.PropName()} where [Code]=@PrivilegeCode";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { PrivilegeCode });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PrivilegeId"></param>
        /// <param name="PrivilegeCode"></param>
        public void UpdateCode(string PrivilegeId, string PrivilegeCode) {
            var type = typeof(TRelationUserPrivilege);
            var typeR = typeof(TRelationRolePrivilege);
            string sql = $"update {type.PropName()} set [PrivilegeCode]=@PrivilegeCode where [PrivilegeId]=@PrivilegeId";
            string sqlR = $"update {typeR.PropName()} set [PrivilegeCode]=@PrivilegeCode where [PrivilegeId]=@PrivilegeId";
            this.DapperRepository.ExcuteOriCommand(sql, true, new { PrivilegeId, PrivilegeCode });
            this.DapperRepository.ExcuteOriCommand(sqlR, true, new { PrivilegeId, PrivilegeCode });
        }
    }
}
