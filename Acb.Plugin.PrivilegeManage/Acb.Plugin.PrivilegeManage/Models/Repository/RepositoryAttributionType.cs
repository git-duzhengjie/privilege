using Acb.Plugin.PrivilegeManage.Models.Entities;
using Acb.MiddleWare.Data.DB;
using Dynamic.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamic.Core.Extensions;

namespace Acb.Plugin.PrivilegeManage.Models.Repository
{
    /// <summary>
    /// 属性类型数据库操作
    /// </summary>
    public class RepositoryAttributionType:DBBase<TAttributionType>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dBConfig"></param>
        /// <returns></returns>
        public RepositoryAttributionType(DBCfgViewModel dBConfig) : base(dBConfig) { }

        /// <summary>
        /// 获取属性类型
        /// </summary>
        /// <param name="TypeId">属性类型ID</param>
        /// <returns></returns>
        public IList<TAttributionType> GetAttributionTypes(string TypeId) {
            var type = typeof(TAttributionType);
            string sql = $"select * from [{type.PropName()}] where [OrganizationTypeId]=@TypeId order by [CreateTime]";
            return this.DapperRepository.Query(sql, true, new { TypeId }).ToList();
        }

        /// <summary>
        /// 插入属性类型
        /// </summary>
        /// <param name="attributionType">属性类型对象</param>
        /// <returns></returns>
        public int Insert(TAttributionType attributionType) {
            return this.DapperRepository.Insert(attributionType, excepts: new[] { nameof(TAttributionType.CreateTime) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributionTypes"></param>
        /// <returns></returns>
        public int Insert(IList<TAttributionType> attributionTypes)
        {
            int count = 0;
            foreach (TAttributionType attributionType in attributionTypes) {
                count += this.DapperRepository.Insert(attributionType, excepts: new[] { nameof(TAttributionType.CreateTime) });
            }
            return count;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="attributionTypes"></param>
        /// <returns></returns>
        public int Update(IList<TAttributionType> attributionTypes) {
            return this.DapperRepository.Update(attributionTypes);
        }

        /// <summary>
        /// 条件删除
        /// </summary>
        /// <param name="Column"></param>
        /// <param name="ColumnValue"></param>
        /// <returns></returns>
        public int DeleteWhere(string Column, string ColumnValue) {
            return this.DapperRepository.Delete(ColumnValue, Column);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        public IList<TAttributionType> GetQueryAttributionTypes(string TypeId) {
            var type = typeof(TAttributionType);
            string sql = $"select * from {type.PropName()} where [OrganizationTypeId]=@TypeId and [IsSearch]=true";
            return this.DapperRepository.Query(sql, true, new { TypeId }).ToList();
        }
    }
}
