using Acb.MiddleWare.Data.DB;
using Dynamic.Core;
using Dynamic.Core.Entities;
using Dynamic.Core.Extensions;
using Dynamic.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acb.Plugin.PrivilegeManage.Common;

namespace Acb.Plugin.PrivilegeManage.Models.Repository
{
    public class BaseData<T> : DBBase<T> where T : IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dBConfig"></param>
        public BaseData(DBCfgViewModel dBConfig) : base(dBConfig)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public int Insert(T Entity)
        {
            return this.DapperRepository.Insert(Entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public int Insert(IEnumerable<T> Entity)
        {
            return this.DapperRepository.Insert(Entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TEntity"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public PagedList<T> Query(T TEntity, int Page, int Size)
        {
            var type = typeof(T);
            string where = TEntity.GenerateQuerySqlFromEntity();
            string sql = $@"select * from {type.PropName()} {where}";
            if (Page == 0 && Size == 0)
            {
                var data = this.DapperRepository.QueryOriCommand<T>(sql, true, TEntity).ToList();
                return new PagedList<T> {DataList = data};
            }
            else
            {
                return this.DapperRepository.PagedList<T>(sql, Page, Size, TEntity) as PagedList<T>;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TEntity"></param>
        /// <param name="Page"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public PagedList<TV> Query<TV, TQ>(TQ TEntity, int Page, int Size)
        {
            var type = typeof(T);
            string where = TEntity.GenerateQuerySqlFromEntity();
            string sql = $@"select * from {type.PropName()} {where} order by [CreateTime] desc";
            if (Page == 0 && Size == 0)
            {
                var data = this.DapperRepository.QueryOriCommand<TV>(sql, true, TEntity).ToList();
                return new PagedList<TV> {DataList = data};
            }
            else
            {
                return this.DapperRepository.PagedList<TV>(sql, Page, Size, TEntity) as PagedList<TV>;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TEntity"></param>
        /// <returns></returns>
        public int Update(T TEntity)
        {
            var type = typeof(T);

            string update = TEntity.GenerateUpdateSqlFromEntity();
            string sql = $"update {type.PropName()} set {update}";
            return this.DapperRepository.ExcuteOriCommand(sql, true, TEntity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IList<T> GetBy(string column, object value)
        {
            var type = typeof(T);
            string sql = $"select * from {type.PropName()} where [{column}]=@value";
            return this.DapperRepository.QueryOriCommand<T>(sql, true, new {value}).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <param name="keyColumn"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public int Update(string column, object value, string keyColumn, object keyValue)
        {
            var type = typeof(T);
            string sql = $"update {type.PropName()} set [{column}]=@value where [{keyColumn}]=@keyValue";
            return this.DapperRepository.ExcuteOriCommand(sql, true, new {value, keyValue});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int Delete(string Id)
        {
            return this.DapperRepository.Delete(Id);
        }
    }
}
