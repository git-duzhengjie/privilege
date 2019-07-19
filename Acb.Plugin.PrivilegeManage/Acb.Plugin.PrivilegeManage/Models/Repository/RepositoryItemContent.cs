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
    public class RepositoryItemContent:BaseData<TItemContent>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dBCfg"></param>
        public RepositoryItemContent(DBCfgViewModel dBCfg) : base(dBCfg) { }

        
    }
}
