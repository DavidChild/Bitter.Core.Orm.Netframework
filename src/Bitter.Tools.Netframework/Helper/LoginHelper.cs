using System;
using System.Web;

namespace Bitter.Tools
{
    /*----------------------------------------------------------------
    // Copyright (C) 2016 Bitter 版权所有。
    //
    // 文件名：LoginHelper.cs
    // 文件功能描述：【登录帮助类】
    //
    // 创建标识：cq 2016年1月17日13:57:16
    //----------------------------------------------------------------*/

    public class LoginHelper
    {
        /// <summary>
        /// 验证是否登录
        /// </summary>
        /// <param name="context"></param>
        public static void LoginValidate(HttpContext context)
        {
            // 如果尚未登录则直接返回json
            if (!HasLogin())
            {
                try
                {
                    context.Response.ContentType = "application/json";
                    string resultStr = "{\"State\": \"error\", \"Message\":\"尚未登录，处理失败!\"}";
                    context.Response.Write(resultStr);
                    context.Response.End();

                    return;
                }
                catch (Exception ex)
                {
                    Bitter.Tools.Helper.LogHelper.ExceptionLog(ex, "LoginHelper.LoginValidate");
                }
            }
        }

        private static bool HasLogin()
        {
            int adminId = Users.Instance().GetUserID();
            return (adminId == 0 ? false : true);
        }
    }
}