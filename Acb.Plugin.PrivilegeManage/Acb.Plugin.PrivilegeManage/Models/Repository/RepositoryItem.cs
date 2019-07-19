using Acb.MiddleWare.Data.DB;
using Acb.Plugin.PrivilegeManage.Models.Entities;
using Dynamic.Core;
using Dynamic.Core.Extensions;
using Dynamic.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acb.Plugin.PrivilegeManage.Common;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Item;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role;

namespace Acb.Plugin.PrivilegeManage.Models.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class RepositoryItem:BaseData<TItem>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dBCfg"></param>
        public RepositoryItem(DBCfgViewModel dBCfg) : base(dBCfg) { }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public PagedList<ItemDto> Query(string systemId, string name, int page, int size) {
            var type = typeof(TItem);
            var typeC = typeof(TItemContent);
            if (name.IsNullOrEmpty())
                name = string.Empty;
            string sql = $@"select * from {type.PropName()} where [SystemId]=@systemId and [Name] like @name";
            if (page == 0 && size == 0)
            {
                var data = this.DapperRepository.Query(sql, true, new { systemId, name = $"%{name}%" }).ToList();
                IList<ItemDto> dtos = new List<ItemDto>();
                foreach (var d in data)
                {
                    ItemDto item = d.MapTo<ItemDto>();
                    item.Items = GetJsonItems(item.Id);
                    dtos.Add(item);
                }

                return new PagedList<ItemDto>{DataList = dtos};
            }
            else {
                var data = this.DapperRepository.PagedList<ItemDto>(sql, page, size, new { systemId, name = $"%{name}%" }) as PagedList<ItemDto>;
                IList<ItemDto> dtos = new List<ItemDto>();
                if (data != null)
                {
                    foreach (var d in data.DataList)
                    {
                        ItemDto item = d.MapTo<ItemDto>();
                        item.Items = GetJsonItems(item.Id);
                        dtos.Add(item);
                    }

                    data.DataList = dtos;
                }

                return data;
            }
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
            var data = this.DapperRepository.QueryOriCommand<TItemContent>(sql, true, new {id});
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="path"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public int EditJsonItem(string id, string path, string item) {
            var type = typeof(TItem);
            string sql = $@"update {type.PropName()} set [SystemJsonItem]=jsonb_set([SystemJsonItem], '{path}', '{item}'),[UpdateTime]=@now 
                            where [Id]=@id";
            return this.DapperRepository.ExcuteOriCommand(sql, true, new { now=DateTime.Now, id});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="path"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public int AddJsonItem(string id, string path, string item)
        {
            var type = typeof(TItem);
            string sql = $@"update {type.PropName()} set [SystemJsonItem]=jsonb_insert([SystemJsonItem], '{path}', '{item}'),[UpdateTime]=@now 
                            where [Id]=@id";
            return this.DapperRepository.ExcuteOriCommand(sql, true, new { now = DateTime.Now ,id});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public int DeleteJsonItem(string id, string path) {
            var type = typeof(TItem);
            string sql = $@"update {type.PropName()} set [SystemJsonItem]=[SystemJsonItem]#-{path},[UpdateTime]=@now  where [Id]=@id";
            return this.DapperRepository.ExcuteOriCommand(sql, true, new { now=DateTime.Now, id});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public bool IsItem(string parentId)
        {
            var type = typeof(TItemContent);
            var typeI = typeof(TItem);
            string sql = $@"select count(*) from {type.PropName()} where [Id]=@parentId";
            string sqlI = $@"select count(*) from {typeI.PropName()} where [Id]=@parentId";
            int count = this.DapperRepository.QueryFirstOrDefault<int>(sql, new {parentId});
            int countI = this.DapperRepository.QueryFirstOrDefault<int>(sqlI, new {parentId});
            if(countI >0 && count >0)
                throw new Exception("系统异常");
            else if (countI > 0 && count == 0)
                return true;
            else if(countI==0 && count>0)
            {
                return false;
            }
            else
            {
                throw new Exception("parentId错误！");
            }
        }
    }
}
