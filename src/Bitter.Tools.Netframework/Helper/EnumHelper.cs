using Bitter.Tools.Utils;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Bitter.Tools.Helper
{
    /*----------------------------------------------------------------
    // Copyright (C) 2016 Bitter 版权所有。
    //
    // 文件名：EnumHelper.cs 文件功能描述：【枚举帮助类】
     * 添加SetEnumHtmlSelect 用于System.Web.UI.HtmlControls.HtmlSelect 控件绑定枚举类列表 2016年1月16日08:59:13 yjq
     * 添加SetDataTableHtmlSelect 用于System.Web.UI.HtmlControls.HtmlSelect 控件绑定Table 2016年1月16日08:59:13 yjq
     * 添加SetListDropDownList<T> 用于DropDownList 控件绑定IList<T> 2016年1月17日11:59:35 cq
     * 添加SetListHtmlSelect<T> 用于System.Web.UI.HtmlControls.HtmlSelect 控件绑定IList<T> 2016年1月17日11:59:35 cq
     * 添加GetEnumDesctipt 用于将指定枚举类的描述和值放到ListItem中，并返回ListItem 2016年1月18日16:57:55 yjq
     * 添加GetEnumDesctiptList 用于获取枚举类的描述和值的ListItem 2016年1月18日16:57:55 yjq
     * 添加SetEnumDescriptDropDownList 用于将指定枚举类的描述与值绑定到DropDownList下拉选择框 2016年1月18日16:57:55 yjq
     * 添加SetEnumDescriptHtmlSelect 将指定枚举类的描述与值绑定到HtmlSelect下拉选择框 2016年1月18日16:57:55 yjq

    // 创建标识：2016年1月16日08:57:16
    //----------------------------------------------------------------*/
    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举类的描述和值的ListItem
        /// </summary>
        /// <param name="type"></param>
        /// <param name="defaulText"></param>
        /// <returns></returns>
        public static List<ListItem> GetEnumDesctiptList(Type type, string defaulText = "")
        {
            List<ListItem> itemList = new List<ListItem>();
            if (!string.IsNullOrEmpty(defaulText))
            {
                itemList.Add(new ListItem() { Text = defaulText, Value = "" });
            }
            return GetEnumDesctipt(itemList, type);
        }

        /// <summary> 根据枚举类型 返回List<ListItem> </summary> <param
        /// name="type">枚举类型(例如：typeof(Aika.BeiTai.Config.EnumCollection.InsureOrderStateEnum))</param>
        /// <param name="defaulText">需要添加的默认选中项（如果为空则不添加）</param> <returns>List<ListItem></returns>
        public static List<ListItem> GetEnumList(Type type, string defaulText = "")
        {
            List<ListItem> itemList = new List<ListItem>();
            if (!string.IsNullOrEmpty(defaulText))
            {
                itemList.Add(new ListItem() { Text = defaulText, Value = "" });
            }{}
            return GetItemByType(itemList, type, null);
        }

        /// <summary>
        /// 获取枚举索引为只o的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string GetEnumName<T>(this object o)
        {
            return Enum.GetName(typeof(T), o.ToSafeInt32(0));
        }

        /// <summary>
        /// 根据设置DropDownList下拉选项值和文本
        /// </summary>
        /// <param name="o">下拉列表控件</param>
        /// <param name="d">数据源datatable</param>
        /// <param name="key">datatable里对应listitem控件key的列</param>
        /// <param name="val">datatable里对应listitem控件value的列</param>
        /// <param name="defaulText">添加默认下拉选项</param>
        public static void SetDataTableDropDownList(this DropDownList o, System.Data.DataTable d, string key, string val, string defaulText)
        {
            if (!string.IsNullOrEmpty(defaulText))
            {
                o.Items.Add(new ListItem
                {
                    Value = "",
                    Text = defaulText
                });
            }

            if (d != null && d.Rows.Count > 0)
            {
                foreach (System.Data.DataRow item in d.Rows)
                {
                    o.Items.Add(new System.Web.UI.WebControls.ListItem()
                    {
                        Text = item[val].ToString(),
                        Value = item[key].ToString()
                    });
                }
            }
            d.Dispose();
        }

        /// <summary>
        /// 设置html <select>元素的下拉选项
        /// </summary>
        /// <param name="o"></param>
        /// <param name="d">数据源datatable</param>
        /// <param name="key">datatable里对应listitem控件key的列</param>
        /// <param name="val">datatable里对应listitem控件value的列</param>
        /// <param name="defaulText">添加默认下拉选项</param>
        public static void SetDataTableHtmlSelect(this System.Web.UI.HtmlControls.HtmlSelect o, System.Data.DataTable d, string key, string val, string defaulText)
        {
            if (!string.IsNullOrEmpty(defaulText))
            {
                o.Items.Add(new ListItem
                {
                    Value = "",
                    Text = defaulText
                });
            }

            if (d != null && d.Rows.Count > 0)
            {
                foreach (System.Data.DataRow item in d.Rows)
                {
                    o.Items.Add(new System.Web.UI.WebControls.ListItem()
                    {
                        Text = item[val].ToString(),
                        Value = item[key].ToString()
                    });
                }
            }
            d.Dispose();
        }

        /// <summary>
        /// 将指定枚举类的描述与值绑定到DropDownList下拉选择框
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="defaulText"></param>
        public static void SetEnumDescriptDropDownList<T>(this DropDownList o, string defaulText)
        {
            GetEnumDesctiptList(typeof(T), defaulText).ForEach(m =>
            {
                o.Items.Add(new ListItem
                {
                    Value = m.Value,
                    Text = m.Text,
                    Selected = m.Selected
                });
            });
        }

        /// <summary>
        /// 将指定枚举类的描述与值绑定到HtmlSelect下拉选择框
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="defaulText"></param>
        public static void SetEnumDescriptHtmlSelect<T>(this System.Web.UI.HtmlControls.HtmlSelect o, string defaulText)
        {
            GetEnumDesctiptList(typeof(T), defaulText).ForEach(m =>
            {
                o.Items.Add(new ListItem
                {
                    Value = m.Value,
                    Text = m.Text,
                    Selected = m.Selected
                });
            });
        }

        /// <summary>
        /// 将指定枚举类的描述与值绑定到DropDownList下拉选择框
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="defaulText"></param>
        public static void SetEnumDropDownList<T>(this DropDownList o, string defaulText)
        {
            GetEnumList(typeof(T), defaulText).ForEach(m =>
            {
                o.Items.Add(new ListItem
                {
                    Value = m.Value,
                    Text = m.Text,
                    Selected = m.Selected
                });
            });
        }

        /// <summary>
        /// 将指定枚举类的描述与值绑定到HtmlSelect下拉选择框
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="defaulText"></param>
        public static void SetEnumHtmlSelect<T>(this System.Web.UI.HtmlControls.HtmlSelect o, string defaulText)
        {
            GetEnumList(typeof(T), defaulText).ForEach(m =>
            {
                o.Items.Add(new ListItem
                {
                    Value = m.Value,
                    Text = m.Text,
                    Selected = m.Selected
                });
            });
        }

        /// <summary>
        /// 将list集合作为数据源绑定到DropDownList，_Momo默认下拉选项（可为空）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="select"></param>
        /// <param name="list"></param>
        /// <param name="_Name"></param>
        /// <param name="_ID"></param>
        /// <param name="_Memo"></param>
        public static void SetListDropDownList<T>(this DropDownList select, IList<T> list, string _Name, string _ID, string _Memo)
        {
            select.DataSource = list;
            select.DataTextField = _Name;
            select.DataValueField = _ID;
            select.DataBind();
            if (!string.IsNullOrEmpty(_Memo.Trim()))
            {
                select.Items.Insert(0, new ListItem(_Memo, ""));
            }
        }

        /// <summary>
        /// 将list集合作为数据源绑定到HtmlSelect，_Momo默认下拉选项（可为空）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="select"></param>
        /// <param name="list"></param>
        /// <param name="_Name"></param>
        /// <param name="_ID"></param>
        /// <param name="_Memo"></param>
        public static void SetListHtmlSelect<T>(this System.Web.UI.HtmlControls.HtmlSelect select, IList<T> list, string _Name, string _ID, string _Memo)
        {
            select.DataSource = list;
            select.DataTextField = _Name;
            select.DataValueField = _ID;
            select.DataBind();
            if (!string.IsNullOrEmpty(_Memo.Trim()))
            {
                select.Items.Insert(0, new ListItem(_Memo, ""));
            }
        }

        /// <summary>
        /// 将指定枚举类的描述和值放到ListItem中，并返回
        /// </summary>
        /// <param name="itemList"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static List<ListItem> GetEnumDesctipt(List<ListItem> itemList, Type type)
        {
            foreach (int item in Enum.GetValues(type))
            {
                System.Reflection.FieldInfo fieldInfo = type.GetField(Enum.GetName(type, item));
                if (fieldInfo != null)
                {
                    System.ComponentModel.DescriptionAttribute[] customAttributes = fieldInfo.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false) as System.ComponentModel.DescriptionAttribute[];
                    if ((customAttributes != null) && (customAttributes.Length == 1))
                    {
                        itemList.Add(new ListItem()
                        {
                            Value = item.ToString(),
                            Text = customAttributes[0].Description
                        });
                    }
                }
            }
            return itemList;
        }

        private static List<ListItem> GetItemByType(List<ListItem> itemList, Type type, int? nowId)
        {
            foreach (int item in Enum.GetValues(type))
            {
                itemList.Add(new ListItem()
                {
                    Selected = item == nowId ? true : false,
                    Value = item.ToString(),
                    Text = Enum.GetName(type, item)
                });
            }
            return itemList;
        }
    }
}