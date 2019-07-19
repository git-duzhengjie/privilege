using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Item;
using Acb.Plugin.PrivilegeManage.Models.Entities;
using Dynamic.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Acb.Plugin.PrivilegeManage.Constract.Models.Dtos.Role;

namespace Acb.Plugin.PrivilegeManage.Plugin
{
  
    /// <summary>
    /// 
    /// </summary>
    public partial class PolicyPrivilegeManagePlugin
    {
        /// <summary>
        /// 添加根级菜单
        /// </summary>
        [HttpPost("addItem")]
        public async Task<DResult<int>> AddItem([FromBody]ItemAddDto addItem) {
            try
            {
                return DResult.Succ(businessItem.AddItem(addItem));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 删除根级菜单
        /// </summary>
        /// <param name="itemId">菜单id</param>
        /// <returns></returns>
        [HttpDelete("deleteItem")]
        public async Task<DResult<int>> DeleteItem(string itemId) {
            try
            {
                return DResult.Succ(businessItem.DeleteItem(itemId));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 查询菜单列表
        /// </summary>
        /// <param name="systemId">系统ID</param>
        /// <param name="name">模糊匹配菜单名</param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet("queryItem")]
        public async Task<DResult<PagedList<ItemDto>>> QueryItem(string systemId, string name, int page, int size)
        {
            try
            {
                return DResult.Succ(businessItem.QueryItem(systemId, name, page, size));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return DResult.Error<PagedList<ItemDto>>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="jsonItemEdit"></param>
        /// <returns></returns>
        [HttpPost("editJsonItem")]
        public async Task<DResult<int>> EditJsonItem([FromBody]JsonItemEditDto jsonItemEdit) {
            try
            {
                return DResult.Succ(businessItem.EditJsonItem(jsonItemEdit));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 添加子级菜单
        /// </summary>
        /// <param name="jsonItemEdit"></param>
        /// <returns></returns>
        [HttpPost("addJsonItem")]
        public async Task<DResult<int>> AddJsonItem([FromBody] JsonItemEditDto jsonItemEdit) {
            try
            {
                return DResult.Succ(businessItem.AddJsonItem(jsonItemEdit));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ex.Message, 500);
            }
        }

        /// <summary>
        /// 删除菜单内容
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpDelete("deleteJsonItem")]
        public async Task<DResult<int>> DeleteJsonItem(string itemId) {
            try
            {
                return DResult.Succ(businessItem.DeleteJsonItem(itemId));
            }
            catch (Exception ex) {
                Logger.Error(ex.ToString());
                return DResult.Error<int>(ex.Message, 500);
            }
        }
    }
}
