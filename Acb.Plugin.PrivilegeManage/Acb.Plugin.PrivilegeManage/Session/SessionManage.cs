using Acb.MiddleWare.Data.Cache;
using Dynamic.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acb.Plugin.PrivilegeManage.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionManage
    {
        /// <summary>
        /// 
        /// </summary>
        public static ICache _cache { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static PolicyPrivilegeManageConfig _config { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="config"></param>
        public static void Init(ICache cache, PolicyPrivilegeManageConfig config) {
            _cache = cache;
            _config = config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="token"></param>
        public static void SetSession(string token, object t) {
            _cache.Set(token, t, _config.SessionTimeOutMillisecond);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="token"></param>
        /// <returns></returns>
        public static T GetSession<T>(string token) {
            if (_cache == null)
                _cache = IocUnity.Get<ICache>();
            return _cache.Get<T>(token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        public static void RemoveSession(string token)
        {
            if (_cache == null)
                _cache = IocUnity.Get<ICache>();
             _cache.Remove(token);
        }

    }
}
