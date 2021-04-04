using Bitter.Tools.Utils;
using System;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bitter.Flow.FlowDialog
{
    public abstract class CusControlObj : PlaceHolder, INamingContainer
    {
        protected string command;
        protected bool enabled;
        protected string filter;
        protected Int32 height;
        protected string idvalue;
        protected bool isnull;
        protected string name;
        protected string tag;
        protected string textvalue;
        protected string title;
        protected Int32 width;

        /// <summary>
        /// 执行命令
        /// </summary>
        public string Command
        {
            get
            {
                return command;
            }
            set
            {
                command = value;
            }
        }

        public abstract bool Enabled
        {
            get;
            set;
        }

        public string Filter
        {
            get
            {
                return filter;
            }
            set
            {
                filter = value;
            }
        }

        /// 执行命令 </summary>
        public Int32 Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value.ToSafeInt32(0);
            }
        }

        /// <summary>
        /// 值Id
        /// </summary>
        public virtual string IdValue
        { get; set; }

        public virtual bool IsNull
        {
            get
            {
                return isnull;
            }
            set
            {
                isnull = value;
            }
        }

        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// 显示值
        /// </summary>
        public virtual string TextValue
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        /// 执行命令 </summary>
        public Int32 Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value.ToSafeInt32(0);
            }
        }

        /// <summary>
        /// 获取表达式对应属性的名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        protected string GetPropertyName<T>(Expression<Func<T, object>> expr)
        {
            var rtn = "";
            if (expr.Body is UnaryExpression)
            {
                rtn = ((MemberExpression)((UnaryExpression)expr.Body).Operand).Member.Name;
            }
            else if (expr.Body is MemberExpression)
            {
                rtn = ((MemberExpression)expr.Body).Member.Name;
            }
            else if (expr.Body is ParameterExpression)
            {
                rtn = ((ParameterExpression)expr.Body).Type.Name;
            }
            return rtn;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //注册客户端JS脚本
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "SelectDialogScript"))
            {
                Page.ClientScript.RegisterClientScriptInclude("SelectDialogScript", "/Flow/FlowDialog/SelectDialog.js");
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        }
    }
}