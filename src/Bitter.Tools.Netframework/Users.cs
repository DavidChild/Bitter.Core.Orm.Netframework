using System;
using System.Web;

namespace Bitter.Tools
{
    public class Users
    {
        private static Users instance = null;

        public static Users Instance()
        {
            if (instance == null)
            {
                instance = new Users();
            }
            return instance;
        }

        /// <summary>
        /// 验证登录信息
        /// </summary>
        /// <returns>adminId,adminName加密值若和adminSigan一致则返回adminId（不一致返回0）</returns>
        public int GetUserID()
        {
            string adminId = string.Empty;
            string adminName = string.Empty;
            string signDate = string.Empty;
            string adminSign = string.Empty;

            if (HttpContext.Current.Session != null)
            {
                adminId = (HttpContext.Current.Session["BT_Manage_AdminId"] == null ? string.Empty : HttpContext.Current.Session["BT_Manage_AdminId"].ToString());
                adminName = (HttpContext.Current.Session["BT_Manage_AdminName"] == null ? string.Empty : HttpContext.Current.Session["BT_Manage_AdminName"].ToString());
                signDate = DateTime.Now.ToString("yyyyMMdd").Trim();
                adminSign = (HttpContext.Current.Session["BT_Manage_AdminSign"] == null ? string.Empty : HttpContext.Current.Session["BT_Manage_AdminSign"].ToString());
            }

            string strSign = Bitter.Tools.Utils.EncryptUtils.MD5(signDate + adminId + signDate + adminName).Trim();

            if (strSign == adminSign)
            {
                return int.Parse(adminId);
            }
            else
            {
                return 0;
            }
        }
    }
}