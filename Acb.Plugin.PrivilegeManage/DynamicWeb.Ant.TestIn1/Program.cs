using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Dynamic.Core.Log;
using Acb.MiddleWare.Web;
using Acb.MiddleWare;
using System.Configuration;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Acb.MiddleWare.Core.Common;
using Acb.Core.MiddleWare;
using Acb.Plugin.PrivilegeManage;
using Acb.Plugin.PrivilegeManage.Plugin;
using Template.Plugin;
using DynamicWeb.MiddleWare.Aop;

namespace DynamicWeb.Ant.TestIn1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AopStartup.InitAopPluginAction = () =>
            {

                InitTestPlugin();
            };
            ProgramInit.MainInit<AopStartup>();
        }
        public static void InitTestPlugin()
        {
            ///这里加测试插件（new 测试插件）
            AopStartup.InjectionPlugins(new PolicyPrivilegeManagePlugin());
        }
    }
}
