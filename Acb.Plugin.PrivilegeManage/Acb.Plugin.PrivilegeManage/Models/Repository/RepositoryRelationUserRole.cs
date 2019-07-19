using Acb.Plugin.PrivilegeManage.Models.Entities;
using Acb.MiddleWare.Data.DB;
using Dynamic.Core.ViewModel;
using System.Collections.Generic;
using Dynamic.Core.Extensions;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role;
using System.Linq;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Privilege;
using Acb.Plugin.PrivilegeManage.Common;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Item;

namespace Acb.Plugin.PrivilegeManage.Models.Repository
{
    /// <summary>
    /// 角色管理数据库操作
    /// </summary>
    public class RepositoryRelationUserRole : DBBase<TRelationUserRole>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dBConfig"></param>
        public RepositoryRelationUserRole(DBCfgViewModel dBConfig) : base(dBConfig) { }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public int Insert(IList<TRelationUserRole> roles) {
            int count = 0;
            foreach (TRelationUserRole userRole in roles) {
                count += this.DapperRepository.Insert(userRole, excepts: new[] { nameof(TRelationUserRole.CreateTime) });
            }
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int Delete(string UserId) {
            return this.DapperRepository.Delete(UserId, "UserId");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<RoleDto> GetRoles(string UserId) {
            var type = typeof(TRelationUserRole);
            var typeR = typeof(TRole);
            var typeI = typeof(TItem);
            string sql = $@"select t2.*,t3.[SystemJsonItem] from (select [RoleId] from {type.PropName()} where [UserId]=@UserId) t1 left join 
                            {typeR.PropName()} t2 on t1.[RoleId]=t2.[Id] left join {typeI.PropName()} t3 on t2.[ItemId]=t3.[Id]";
            return this.DapperRepository.QueryOriCommand<RoleDto>(sql, true, new { UserId }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<PrivilegeDto> GetPrivileges(string UserId) {
            var type = typeof(TRelationUserRole);
            var typeR = typeof(TRelationRolePrivilege);
            var typeP = typeof(TPrivilege);
            string sql = $@"select t3.* from (select [RoleId] from {type.PropName()} where [UserId]=@UserId) t1 left join 
                            {typeR.PropName()} t2 on t1.[RoleId]=t2.[RoleId] left join {typeP.PropName()} t3 on t2.[PrivilegeId]=t3.[Id]";
            var data = this.DapperRepository.QueryOriCommand<PrivilegeDto>(sql, true, new { UserId }).ToList();
            return StringManage.RemoveNull(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="PrivilegeId"></param>
        /// <param name="PrivilegeCode"></param>
        /// <returns></returns>
        public int CountUserPrivilege(string UserId, string PrivilegeId, string PrivilegeCode) {
            var type = typeof(TRelationUserRole);
            var typeP = typeof(TRelationRolePrivilege);
            if (!string.IsNullOrEmpty(PrivilegeId))
            {
                string sql = $@"select count(*) from (select [RoleId] from {type.PropName()} where [UserId]=@UserId) t1 left join 
                        {typeP.PropName()} t2 on t1.[RoleId]=t2.[RoleId] where t2.[PrivilegeId]=@PrivilegeId";
                return this.DapperRepository.QueryFirstOrDefault<int>(sql, new { UserId, PrivilegeId });
            }
            else if (!string.IsNullOrEmpty(PrivilegeCode))
            {
                string sql = $@"select count(*) from (select [RoleId] from {type.PropName()} where [UserId]=@UserId) t1 left join 
                        {typeP.PropName()} t2 on t1.[RoleId]=t2.[RoleId] where t2.[PrivilegeCode]=@PrivilegeCode";
                return this.DapperRepository.QueryFirstOrDefault<int>(sql, new { UserId, PrivilegeCode });
            }
            else
                return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<SystemJsonItem> GetUserJsonItems(string UserId) {
            var typeI = typeof(TItem);
            var typeRi = typeof(TRelationRoleItem);
            var typeRu = typeof(TRelationUserOrganization);
            var typeRp = typeof(TRelationPositionRole);
            var typeRr = typeof(TRelationUserRole);
            string sqlU = $@"select t3.* from (select [RoleId] from {typeRr.PropName()} where [UserId]=@UserId) t1 
                            left join {typeRi.PropName()} t2 on t1.[RoleId]=t2.[RoleId] left join {typeI.PropName()} t3 on t2.[ItemId]=t3.[Id]";
            var resultU = this.DapperRepository.QueryOriCommand<ItemDto>(sqlU, true, new { UserId }).ToList();
            string sqlP = $@"select t4.* from (select [PositionId] from {typeRu.PropName()} where [UserId]=@UserId) t1 left join {typeRp.PropName()} t2
                             on t1.[PositionId]=t2.[PositionId] left join {typeRi.PropName()} t3 on t2.[RoleId]=t3.[RoleId] 
                            left join {typeI.PropName()} t4 on t3.[ItemId]=t4.[Id] where t3.[Id] is not null";
            var resultP = this.DapperRepository.QueryOriCommand<ItemDto>(sqlP, true, new { UserId }).ToList();
            resultU.AddRange(resultP);
            IList<ItemDto> dtos = new List<ItemDto>();
            foreach (var d in resultP)
            {
                ItemDto item = d;
                item.Items = GetJsonItems(item.Id);
                dtos.Add(item);
            }
            return dtos.MapTo<SystemJsonItem>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<string> GetUserJsons(string UserId)
        {
            var typeRi = typeof(TRole);
            var typeRu = typeof(TRelationUserOrganization);
            var typeRp = typeof(TRelationPositionRole);
            var typeRr = typeof(TRelationUserRole);
            string sqlU = $@"select t2.[JsonItem] from (select [RoleId] from {typeRr.PropName()} where [UserId]=@UserId) t1 
                            left join {typeRi.PropName()} t2 on t1.[RoleId]=t2.[Id]";
            var resultU = this.DapperRepository.QueryOriCommand<string>(sqlU, true, new { UserId }).ToList();
            string sqlP = $@"select t3.[JsonItem] from (select [PositionId] from {typeRu.PropName()} where [UserId]=@UserId) t1 left join {typeRp.PropName()} t2
                             on t1.[PositionId]=t2.[PositionId] left join {typeRi.PropName()} t3 on t2.[RoleId]=t3.[Id] 
                            where t3.[Id] is not null";
            var resultP = this.DapperRepository.QueryOriCommand<string>(sqlP, true, new { UserId }).ToList();
            resultU.AddRange(resultP);
            return resultU;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<ItemContentDto> GetSubItem(string parentId)
        {
            var type = typeof(TItemContent);
            string sql = $@"select * from {type.PropName()} where [ParentId]=@parentId";
            var data = this.DapperRepository.QueryOriCommand<TItemContent>(sql, true, new { parentId });
            List<ItemContentDto> jsonItems = new List<ItemContentDto>();
            foreach (var d in data)
            {
                ItemContentDto jsonItem = d.MapTo<ItemContentDto>();
                if (d.IsHasChildren)
                    jsonItem.Children = GetSubItem(d.ParentId);
                jsonItems.Add(jsonItem);
            }

            return jsonItems;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<ItemContentDto> GetJsonItems(string id)
        {
            var type = typeof(TItemContent);
            string sql = $@"select * from {type.PropName()} where [ItemId]=@id";
            var data = this.DapperRepository.QueryOriCommand<TItemContent>(sql, true, new { id });
            IList<ItemContentDto> jsonItems = new List<ItemContentDto>();
            foreach (var d in data)
            {
                ItemContentDto jsonItem = d.MapTo<ItemContentDto>();
                if (d.IsHasChildren)
                    jsonItem.Children = GetSubItem(d.ParentId);
                jsonItems.Add(jsonItem);
            }

            return jsonItems;
        }

    }
}
