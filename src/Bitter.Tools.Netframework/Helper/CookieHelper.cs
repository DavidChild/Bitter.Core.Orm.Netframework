using System;
using System.Collections.Specialized;
using System.Web;

namespace Bitter.Tools.Helper
{
    /// <summary>
    /// Cookie静态操作类
    /// </summary>
    public static class Cookie
    {
        #region 静态方法

        /// <summary>
        /// 删除Cookie
        /// </summary>
        /// <param name="strCookieName">Cookie Name</param>
        public static void Delete(string strCookieName)
        {
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            objCookie.Expires = DateTime.Now.AddYears(-5);
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        /// <summary>
        /// cookie修改
        /// </summary>
        /// <param name="strCookieName">需修改的cookie name</param>
        /// <param name="strKeyName">cookie子健 key值</param>
        /// <param name="KeyValue">cookie子健 value值</param>
        /// <param name="iExpires">cookie过期时间（1：最大时间）</param>
        /// <returns>是否修改成功</returns>
        public static bool Edit(string strCookieName, string strKeyName, string KeyValue, int iExpires)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return false;
            }
            else
            {
                HttpCookie objCookie = HttpContext.Current.Request.Cookies[strCookieName];

                objCookie[strKeyName] = HttpContext.Current.Server.UrlEncode(KeyValue.Trim());

                if (iExpires > 0)
                {
                    if (iExpires == 1)
                    {
                        objCookie.Expires = DateTime.MaxValue;
                    }
                    else
                    {
                        //objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
                    }
                }

                HttpContext.Current.Response.Cookies.Set(objCookie);

                return true;
            }
        }

        /// <summary>
        /// cookie修改方法
        /// </summary>
        /// <param name="strCookieName">cookie name</param>
        /// <param name="Value">修改目标cookie value</param>
        /// <param name="iExpires">cookie过期时间（1：最大时间）</param>
        /// <returns></returns>
        public static bool Edit(string strCookieName, string Value, int iExpires)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return false;
            }
            else
            {
                HttpCookie objCookie = HttpContext.Current.Request.Cookies[strCookieName];
                objCookie.Value = Value;
                if (iExpires > 0)
                {
                    if (iExpires == 1)
                    {
                        objCookie.Expires = DateTime.MaxValue;
                    }
                    else
                    {
                        // objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
                    }
                }
                HttpContext.Current.Response.Cookies.Set(objCookie);
                return true;
            }
        }

        /// <summary>
        /// 获取cookie值
        /// </summary>
        /// <param name="strCookieName">cookie name</param>
        /// <returns>cookie对应的值</returns>
        public static string GetValue(string strCookieName)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null || HttpContext.Current.Request.Cookies[strCookieName].Value.ToString() == "null")
            {
                return string.Empty;
            }
            else
            {
                return HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies[strCookieName].Value);
            }
        }

        /// <summary>
        /// 获取cookie值
        /// </summary>
        /// <param name="strCookieName">cookie name</param>
        /// <param name="strKeyName">cookie 子键name</param>
        /// <returns></returns>
        public static string GetValue(string strCookieName, string strKeyName)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return null;
            }
            else
            {
                string strObjValue = HttpContext.Current.Request.Cookies[strCookieName].Value;

                string strKeyName2 = strKeyName + "=";

                if (strObjValue.IndexOf(strKeyName2) == -1)
                {
                    return null;
                }
                else
                {
                    return HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies[strCookieName][strKeyName]);
                }
            }
        }
        /// <summary>
        /// 设置cookie，无对应cookie则新建，有则修改（默认过期时间一天）
        /// </summary>
        /// <param name="strCookieName">cookie name</param>
        /// <param name="strValue">cookie value</param>
        public static void SetObject(string strCookieName, string strValue)
        {
            SetObject(strCookieName, 1, strValue);
        }

        public static void SetObject(string strCookieName, int iExpires, string strValue)
        {
            iExpires = 86400;
            //Delete(strCookieName);
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
                objCookie.Value = HttpContext.Current.Server.UrlEncode(strValue.Trim());
                if (iExpires > 0)
                {
                    if (iExpires == 1)
                    {
                        objCookie.Expires = DateTime.MaxValue;
                    }
                    else
                    {
                        objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
                    }
                }

                HttpContext.Current.Response.Cookies.Add(objCookie);
            }
            else
            {
                Edit(strCookieName, strValue, iExpires);
            }
        }

        public static void SetObject(string strCookieName, int iExpires, NameValueCollection KeyValue)
        {
            iExpires = 86400;
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            foreach (string key in KeyValue.AllKeys)
            {
                objCookie[key] = HttpContext.Current.Server.UrlEncode(KeyValue[key].Trim());
            }

            if (iExpires > 0)
            {
                if (iExpires == 1)
                {
                    objCookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
                }
            }

            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        public static void SetObject(string strCookieName, int iExpires, string strValue, string strDomain)
        {
            iExpires = 86400;
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());

            objCookie.Value = HttpContext.Current.Server.UrlEncode(strValue.Trim());

            objCookie.Domain = strDomain.Trim();

            if (iExpires > 0)
            {
                if (iExpires == 1)
                {
                    objCookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
                }
            }

            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        public static void SetObject(string strCookieName, int iExpires, NameValueCollection KeyValue, string strDomain)
        {
            iExpires = 86400;
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());

            foreach (string key in KeyValue.AllKeys)
            {
                objCookie[key] = HttpContext.Current.Server.UrlEncode(KeyValue[key].Trim());
            }

            objCookie.Domain = strDomain.Trim();

            if (iExpires > 0)
            {
                if (iExpires == 1)
                {
                    objCookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
                }
            }

            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        #endregion 静态方法
    }
}