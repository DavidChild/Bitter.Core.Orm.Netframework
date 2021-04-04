using System.Text;
using System.Web;
using System.Web.UI;

namespace Bitter.Tools.Utils
{
    public static class MessageUtils
    {
        /// <summary>
        /// 警告提示,不关闭窗口
        /// </summary>
        /// <param name="message"></param>
        public static void Alert_Wern(string str)
        {
            ExecuteScript(string.Format("alert(\"{0}\");", str));
        }

        public static void ExecuteScript(string scriptBody)
        {
            string scriptKey = "Q";
            Page p = HttpContext.Current.Handler as Page;
            p.ClientScript.RegisterStartupScript(typeof(string), scriptKey, scriptBody, true);
        }

        /// <summary>
        /// 页面重载
        /// </summary>
        public static void Location()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append("window.location.href=window.location.href;");
            sb.Append("</script>");
            System.Web.HttpContext.Current.Response.Write(sb.ToString());
            System.Web.HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 关闭当前页面
        /// </summary>
        /// <param name="str"></param>
        public static void ModalAlertClose(string str)
        {
            ExecuteScript(string.Format("alert(\"{0}\");\nwindow.parent.tb_remove();\n", str));
        }

        /// <summary>
        /// 刷新当前页面
        /// </summary>
        /// <param name="str"></param>
        public static void ModalAlertReload(string str)
        {
            ExecuteScript(string.Format("alert(\"{0}\");\ntop.windowload();\n", str));

            //StringBuilder sb = new StringBuilder();
            //sb.Append("<script language=\"javascript\"> \n");
            //sb.Append("alert(\"" + str.Trim() + "\");\n");
            //sb.Append("top.windowload();\n");
            //sb.Append("</script>");
            //System.Web.HttpContext.Current.Response.Write(sb.ToString());
            //System.Web.HttpContext.Current.Response.End();
        }

        public static void ModalAlertReloadAndClose(string str)
        {
            ExecuteScript(string.Format("alert(\"{0}\");\nwindow.parent.windowload();\n", str));
        }

        /// <summary>
        /// 刷新上级iframe，关闭本页
        /// </summary>
        /// <param name="str"></param>
        public static void ModalAlertReloadClose(string str)
        {
            ExecuteScript(string.Format("alert(\"{0}\");\ntop.refreshif();window.parent.tb_remove();\n", str));
        }

        /// <summary>
        /// 刷新上级iframe，不关闭本页
        /// </summary>
        /// <param name="str"></param>
        public static void ModalAlertReloadNoClose(string str)
        {
            ExecuteScript(string.Format("alert(\"{0}\");\ntop.refreshif();\n", str));
        }

        /// <summary>
        /// 刷新上级iframe，不关闭本页 Jason
        /// </summary>
        /// <param name="str"></param>
        public static void ModalAlertReloadNoCloseForSencend(string str)
        {
            ExecuteScript(string.Format("alert(\"{0}\");\n top.forceRefesh();\n", str));
        }

        /// <summary>
        /// 关闭当前页面，刷新上级页面
        /// </summary>
        /// <param name="str"></param>
        public static void ModalAlertReloadParentClose(string str, string path)
        {
            ExecuteScript(string.Format("alert(\"{0}\");\nwindow.parent.tb_remove();window.parent.location.href='{1}';\n", str, path));
        }

        /// <summary>
        /// 关闭当前页面，刷新上级页面
        /// </summary>
        /// <param name="str"></param>
        public static void ModalAlertReloadParentClose(string str)
        {
            ExecuteScript(string.Format("alert(\"{0}\");\nwindow.parent.tb_remove();window.parent.location.href=window.parent.location.href;\n", str));
        }

        /// <summary>
        /// 关闭所有子页面,刷新列表页面
        /// </summary>
        /// <param name="str"></param>
        public static void ModalAllCloseAndRefreshifTop(string str)
        {
            var k = "\n window.parent.CloseMine('" + str + "');\n";
            ExecuteScript(k);
        }

        /// <summary>
        /// 关闭所有子页面,刷新列表页面
        /// </summary>
        /// <param name="str"></param>
        public static void ModalAllCloseAndRefreshifTop()
        {
            var k = "refparent(); window.parent.tb_remove();\n";
            ExecuteScript(k);
        }

        public static void ModalReloadClose()
        {
            ExecuteScript(string.Format("top.refreshif();window.parent.tb_remove();\n"));
        }

        /// <summary>
        /// 重定向
        /// </summary>
        /// <param name="url"></param>
        public static void RedirectPage(string url)
        {
            string path = "http://" + System.Web.HttpContext.Current.Request.Url.Host + url;
            //StringBuilder sb = new StringBuilder();
            //sb.Append("<script language=\"javascript\"> \n");
            //sb.Append(string.Format("window.location.href='{0}';", @path));
            //sb.Append("</script>");

            //System.Web.HttpContext.Current.Response.Write(sb.ToString());
            //System.Web.HttpContext.Current.Response.End();
            ExecuteScript(string.Format(string.Format("window.location.href='{0}';", @path)));
        }

        /// <summary>
        /// 刷新上级iframe，不关闭本页
        /// </summary>
        /// <param name="str"></param>
        public static void RefreshAndNoClose(string str, string url)
        {
            ExecuteScript(string.Format("alert(\"{0}\");\ntop.refreshif();window.location.href=\"{1}\"\n", str, url));
        }

        /// <summary>
        /// 刷新当前页面，并且不关闭
        /// </summary>
        /// <param name="str"></param>
        /// <param name="url"></param>
        public static void RefreshNowNoClose(string str, string url)
        {
            ExecuteScript(string.Format("alert(\"{0}\");\nwindow.location.href=\"{1}\"\n", str, url));
        }

        /// <summary>
        /// 刷新当前页面，并且不关闭
        /// </summary>
        /// <param name="str"></param>
        /// <param name="url"></param>
        public static void RefreshNowNoClose(string str)
        {
            ExecuteScript(string.Format("alert(\"{0}\");\nwindow.location.href=window.location.href\n", str));
        }

        /// <summary>
        /// 显示一个弹出窗口
        /// </summary>
        /// <param name="str"></param>
        public static void Show(string str)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append("alert(\"" + str.Trim() + "\"); \n");
            sb.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sb.ToString());
            System.Web.HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 显示一个弹出窗口，并关闭当前页
        /// </summary>
        /// <param name="str"></param>
        public static void ShowClose(string str)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script language=\"javascript\">\n");
            sb.Append("alert(\"" + str.Trim() + "\"); \n");
            sb.Append("window.close();\n");
            sb.Append("</script>\n");
            System.Web.HttpContext.Current.Response.Write(sb.ToString());
            System.Web.HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 显示一段自定义的输出代码
        /// </summary>
        /// <param name="MyPage"></param>
        /// <param name="strCode"></param>
        public static void ShowJS(System.Web.UI.Page MyPage, string strCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append(strCode.Trim());
            sb.Append("</script>");
            MyPage.Response.Write(sb.ToString());
        }

        /// <summary>
        /// 显示一个弹出窗口，并转向当前页(刷新)
        /// </summary>
        /// <param name="str"></param>
        public static void ShowLocation(string str)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append("alert(\"" + str.Trim() + "\"); \n");
            sb.Append("window.location.href=window.location.href;\n");
            sb.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sb.ToString());
            System.Web.HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 显示一个弹出窗口
        /// </summary>
        /// <param name="str"></param>
        public static void ShowPre(string str)
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append("<script language=\"javascript\"> \n");
            //sb.Append("alert(\"" + str.Trim() + "\");\n");
            //sb.Append("history.back();\n");
            //sb.Append("</script>");

            //System.Web.HttpContext.Current.Response.Write(sb.ToString());
            //System.Web.HttpContext.Current.Response.End();
            ExecuteScript(string.Format(string.Format("alert(\"" + str.Trim() + "\");\n")));
        }

        /// <summary>
        /// 显示一个弹出窗口，并转向目标页(导航)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="url"></param>
        public static void ShowRedirect(string str, string url)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            if (!string.IsNullOrEmpty(str))
                sb.Append("alert(\"" + str.Trim() + "\"); \n");
            sb.Append("window.location.href=\"" + url.Trim() + "\";\n");
            sb.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sb.ToString());
            System.Web.HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 一般输出
        /// </summary>
        /// <param name="str"></param>
        public static void Write(string str)
        {
            System.Web.HttpContext.Current.Response.Write(str);
            System.Web.HttpContext.Current.Response.End();
        }
    }
}