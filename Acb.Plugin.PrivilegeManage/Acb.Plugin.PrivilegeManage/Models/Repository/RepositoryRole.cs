using Acb.Plugin.PrivilegeManage.Models.Entities;
using Acb.MiddleWare.Data.DB;
using Dynamic.Core.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Dynamic.Core.Extensions;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role;

namespace Acb.Plugin.PrivilegeManage.Models.Repository
{
    /// <summary>
    /// 角色管理数据库操作
    /// </summary>
    public class RepositoryRole : DBBase<TRole>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dBConfig"></param>
        public RepositoryRole(DBCfgViewModel dBConfig) : base(dBConfig) { }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public int Insert(TRole role) {
            return this.DapperRepository.Insert(role, excepts: new[] { nameof(TRole.CreateTime) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public int Delete(string RoleId) {
            return this.DapperRepository.Delete(RoleId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public string GetName(string RoleId) {
            var type = typeof(TRole);
            string sql = $"select [Name] from {type.PropName()} where [Id]=@RoleId";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { RoleId });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public AbstractDto GetAbstract(string RoleId) {
            var type = typeof(TRole);
            string sql = $"select [Id],[Name] from {type.PropName()} where [Id]=@RoleId";
            return this.DapperRepository.QueryFirstOrDefault<AbstractDto>(sql, new { RoleId });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SystemId"></param>
        /// <returns></returns>
        public IList<RoleDto> GetRoles(string SystemId) {
            var type = typeof(TRole);
            string sql = $"select * from {type.PropName()} where [SystemId]=@SystemId";
            return this.DapperRepository.QueryOriCommand<RoleDto>(sql, true, new { SystemId }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleUpdate"></param>
        /// <returns></returns>
        public int Update(TRoleUpdate roleUpdate) {
            return this.DapperRepository.Update(roleUpdate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GetSystemCode(string Id) {
            var type = typeof(TRole);
            var typeS = typeof(TSystem);
            string sql = $@"select t2.[Code] from (select [SystemId] from {type.PropName()} where [Id]=@Id) t1 left join {typeS.PropName()} t2 on t1.[SystemId]=t2.[Id]";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { Id });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GetCode(string Id) {
            var type = typeof(TRole);
            string sql = $@"select [Code] from {type.PropName()} where [Id]=@Id";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { Id });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public string GetId(string Code)
        {
            var type = typeof(TRole);
            string sql = $@"select [Id] from {type.PropName()} where [Code]=@Code";
            return this.DapperRepository.QueryFirstOrDefault<string>(sql, new { Code });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="RoleCode"></param>
        public void UpdateCode(string RoleId, string RoleCode)
        {
            var typePr = typeof(TRelationPositionRole);
            var typeR = typeof(TRelationRolePrivilege);
            var typeUr = typeof(TRelationUserRole);
            string sqlPr = $"update {typePr.PropName()} set [RoleCode]=@RoleCode where [RoleId]=@RoleId";
            string sqlR = $"update {typeR.PropName()} set [RoleCode]=@RoleCode where [RoleId]=@RoleId";
            string sqlUr = $"update {typeUr.PropName()} set [RoleCode]=@RoleCode where [RoleId]=@RoleId";
            this.DapperRepository.ExcuteOriCommand(sqlPr, true, new { RoleId, RoleCode });
            this.DapperRepository.ExcuteOriCommand(sqlR, true, new { RoleId, RoleCode });
            this.DapperRepository.ExcuteOriCommand(sqlUr, true, new { RoleId, RoleCode });
        }
    }
}
