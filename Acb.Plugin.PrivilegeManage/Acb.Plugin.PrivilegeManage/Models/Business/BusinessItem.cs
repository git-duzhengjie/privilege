using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Item;
using Acb.Plugin.PrivilegeManage.Models.Entities;
using Acb.Plugin.PrivilegeManage.Models.Repository;
using Dynamic.Core;
using Dynamic.Core.Comm;
using Dynamic.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Acb.Plugin.PrivilegeManage.Common;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role;
using MongoDB.Bson;

namespace Acb.Plugin.PrivilegeManage.Models.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemAdd"></param>
        /// <returns></returns>
        public int AddItem(ItemAddDto itemAdd) {
           
            TItem item = new TItem
            {
                Id = IdentityHelper.NewSequentialGuid().ToString("N"),
                Name = itemAdd.Name,
                SystemId = itemAdd.SystemId,
                CreateTime = DateTime.Now,
                SystemJsonItem = "{}",
                Status=true,
                FrontSystemName = itemAdd.FrontSystemName,
                FrontSystemCode = itemAdd.FrontSystemCode
            };
            return IocUnity.Get<RepositoryItem>().Insert(item);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>

        public int DeleteItem(string itemId) {
            return IocUnity.Get<RepositoryItem>().Delete(itemId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public PagedList<ItemDto> QueryItem(string systemId, string name, int page, int size) {
            return IocUnity.Get<RepositoryItem>().Query(systemId, name, page, size);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonItemEdit"></param>
        /// <returns></returns>
        public int EditJsonItem(JsonItemEditDto jsonItemEdit)
        {
            TItemContent itemContent = jsonItemEdit.MapTo<TItemContent>();
            itemContent.UpdateTime=DateTime.Now;
            return IocUnity.Get<RepositoryItemContent>().Update(jsonItemEdit.MapTo<TItemContent>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonItemEdit"></param>
        /// <returns></returns>
        public int AddJsonItem(JsonItemEditDto jsonItemEdit)
        {
            var r = IocUnity.Get<RepositoryItem>().IsItem(jsonItemEdit.ParentId);
            if (r)
            {
                var id = IdentityHelper.NewSequentialGuid().ToString("N");
                TItemContent itemContent = jsonItemEdit.MapTo<TItemContent>();
                itemContent.Id = id;
                itemContent.IsHasChildren = false;
                itemContent.CreateTime=DateTime.Now;
                itemContent.ParentId = null;
                itemContent.UpdateTime = null;
                itemContent.ItemId = jsonItemEdit.ParentId;
                return IocUnity.Get<RepositoryItemContent>().Insert(itemContent);
            }
            else
            {
                var id = IdentityHelper.NewSequentialGuid().ToString("N");
                TItemContent itemContent = jsonItemEdit.MapTo<TItemContent>();
                itemContent.Id = id;
                itemContent.CreateTime = DateTime.Now;
                itemContent.UpdateTime = null;
                itemContent.ParentId = jsonItemEdit.ParentId;
                itemContent.ItemId = null;
                int count = 0;
                IocUnity.Get<RepositoryItemContent>().DapperRepository.ExcuteTransaction(c =>
                    {
                        count = IocUnity.Get<RepositoryItemContent>().Insert(itemContent);
                        IocUnity.Get<RepositoryItemContent>()
                            .Update("IsHasChildren", true, "Id", jsonItemEdit.ParentId);
                    });
                return count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public int DeleteJsonItem(string itemId) {
            return IocUnity.Get<RepositoryItemContent>().Delete(itemId);
        }
   
    }
}
