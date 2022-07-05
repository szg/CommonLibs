using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLibs.Utilities
{
    /// <summary>
    /// 浏览器公共类
    /// </summary>
    public class BrowserUtility
    {
        /// <summary>
        /// 判断是否在微信内置浏览器中
        /// </summary>
        /// <param name="userAgent">浏览器头</param>
        /// <returns>true：在微信内置浏览器内。false：不在微信内置浏览器内。</returns>
        public static bool SideInWeixinBrowser(string userAgent)
        {
            //判断是否在微信浏览器内部
            var isInWeixinBrowser = userAgent != null &&
                        (userAgent.ToUpper().Contains("MICROMESSENGER")/*MicroMessenger*/ ||
                        userAgent.ToUpper().Contains("WINDOWS PHONE")/*Windows Phone*/);
            return isInWeixinBrowser;
        }

        /// <summary>
        /// 获取客户端IP地址（无视代理）
        /// </summary>
        /// <param name="httpContext">httpContext</param>
        /// <returns>若失败则返回回送地址</returns>
        public static string GetHostAddress(HttpContext httpContext)
        {
            string result = httpContext.Request.Headers["HTTP_X_FORWARDED_FOR"].FirstOrDefault();
            if (null == result || result == String.Empty)
            {
                result = httpContext.Request.Headers["REMOTE_ADDR"].FirstOrDefault();
            }

            if (null == result || result == String.Empty)
            {
                result = httpContext.Connection.RemoteIpAddress.ToString();
            }
            result = result.Split(',')[0].Trim();
            if (result.Equals("::1") || result.Equals("localhost"))
            {
                result = "127.0.0.1";
            }
            return result;
        }

        /// <summary>
        /// 获取跟域名
        /// </summary>
        /// <param name="host">地址</param>
        /// <returns></returns>
        public static string GetBaseDomain(string host)
        {
            IList<string> list = new List<string>(".com|.co|.info|.net|.org|.me|.mobi|.us|.biz|.xxx|.ca|.co.jp|.com.cn|.net.cn|.org.cn|.mx|.tv|.ws|.ag|.com.ag|.net.ag|.org.ag|.am|.asia|.at|.be|.com.br|.net.br|.bz|.com.bz|.net.bz|.cc|.com.co|.net.co|.nom.co|.de|.es|.com.es|.nom.es|.org.es|.eu|.fm|.fr|.gs|.in|.co.in|.firm.in|.gen.in|.ind.in|.net.in|.org.in|.it|.jobs|.jp|.ms|.com.mx|.nl|.nu|.co.nz|.net.nz|.org.nz|.se|.tc|.tk|.tw|.com.tw|.idv.tw|.org.tw|.hk|.co.uk|.me.uk|.org.uk|.vg".Split('|'));
            string[] hs = host.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (hs.Length > 2)
            {
                //传入的host地址至少有三段
                int p2 = host.LastIndexOf('.');                 //最后一次“.”出现的位置
                int p1 = host.Substring(0, p2).LastIndexOf('.');//倒数第二个“.”出现的位置
                string s1 = host.Substring(p1);
                if (!list.Contains(s1))
                    return s1.TrimStart('.');

                //域名后缀为两段（有用“.”分隔）
                if (hs.Length > 3)
                    return host.Substring(host.Substring(0, p1).LastIndexOf('.'));
                else
                    return host.TrimStart('.');
            }
            else if (hs.Length == 2)
            {
                return host.TrimStart('.');
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
