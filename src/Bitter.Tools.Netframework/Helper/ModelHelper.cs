using Bitter.Tools.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Bitter.Tools.Helper
{
    public sealed class ModelHelper<T> where T : class, new()
    {
        /// <summary>
        /// 返回实体列表,自动关闭datareader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<T> ConvertManyModel(IDataReader reader)
        {
            return ConvertManyModel(reader, true);
        }

        /// <summary>
        /// 返回实体列表
        /// </summary>
        /// <param name="reader">IDataReader接口</param>
        /// <param name="autoClose">手动控制是否关闭dataReader</param>
        /// <returns></returns>
        public static List<T> ConvertManyModel(IDataReader reader, bool autoClose)
        {
            List<T> list = null;
            if (!reader.IsClosed)
            {
                list = new List<T>();
                try
                {
                    while (reader.Read())
                    {
                        T obj = new T();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string fieldName = reader.GetName(i);
                            PropertyInfo p = obj.GetType().GetProperty(fieldName);
                            if (p == null || !p.CanWrite) continue;
                            p.SetValue(obj, Base.GetDefaultValue(reader[i], p.PropertyType), null);
                        }
                        list.Add(obj);
                    }
                }
                catch
                {
                    reader.Dispose();
                    reader.Close();
                }
                finally
                {
                    if (autoClose)
                    {
                        reader.Dispose();
                        reader.Close();
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 返回单个实体
        /// </summary>
        /// <param name="reader">IDataReader接口</param>
        /// <returns></returns>
        public static T ConvertSingleModel(IDataReader reader)
        {
            return ConvertSingleModel(reader, true);
        }

        /// <summary>
        /// 返回单个实体
        /// </summary>
        /// <param name="reader">IDataReader接口</param>
        /// <returns></returns>
        public static T ConvertSingleModel(IDataReader reader, bool autoClose)
        {
            T t = null;
            if (reader.Read())
            {
                try
                {
                    t = new T();
                    Type modelType = t.GetType();
                    int len = reader.FieldCount;
                    for (int i = 0; i < len; i++)
                    {
                        string filedName = reader.GetName(i);
                        PropertyInfo p = modelType.GetProperty(filedName);
                        if (p == null || !p.CanWrite) continue;
                        p.SetValue(t, Base.GetDefaultValue(reader[p.Name], p.PropertyType), null);
                    }
                }
                catch
                {
                    reader.Dispose();
                    reader.Close();
                }
                finally
                {
                    if (autoClose)
                    {
                        reader.Dispose();
                        reader.Close();
                    }
                }
            }
            return t;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T CreateModelFromRow<T>(DataRow row) where T : new()
        {
            T item = new T();
            SetItemFromRow(item, row);
            return item;
        }


        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static  object CreateModelFromRow(Type type,  DataRow row) 
        {
            //查询条件表达式转换成SQL的条件语句
            //获取类的初始化参数信息
            ConstructorInfo ct1 = type.GetConstructor(System.Type.EmptyTypes);
            //调用不带参数的构造器
            var data = ct1.Invoke(null);
            SetItemFromRow(data, row);
            return data;
        }

        /// <summary>
        /// 将datarow赋值到泛型类上
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T CreateModelFromRow<T>(T item, DataRow row) where T : new()
        {
            SetItemFromRow(item, row);
            return item;
        }

        public static void SetItemFromRow<T>(T item, DataRow row) where T : new()
        {
            foreach (DataColumn c in row.Table.Columns)
            {
                PropertyInfo p = item.GetType().GetProperty(c.ColumnName);
                if (p != null && row[c] != DBNull.Value)
                {
                    try
                    {
                        p.SetValue(item, row[c], null);
                    }
                    catch(Exception ex)
                    {
                        LogService.Default.Fatal("Mpping字段类型映射赋值出错：Model:【"+item.GetType().FullName+"】，字段【" + c.ColumnName+"】");
                        throw ex;
                    }
                }
            }
        }

        #region "控件赋值"

        /// <summary>
        /// 设置页面控件的值
        /// </summary>
        /// <param name="page"></param>
        /// <param name="model"></param>
        public static void SetWebControls(Control page, T model)
        {
            SetWebControls(page, HashTableHelper.GetModelToHashtable(model));
        }

        /// <summary>
        /// 设置页面控件的值
        /// </summary>
        /// <param name="page"></param>
        /// <param name="ht"></param>
        public static void SetWebControls(Control page, Hashtable ht)
        {
            if (ht.Count != 0)
            {
                int size = ht.Keys.Count;
                foreach (string key in ht.Keys)
                {
                    object val = ht[key];
                    if (val != null)
                    {
                        Control control = page.FindControl("txt" + key);
                        Control hiddenCtr = page.FindControl("h" + key);
                        //var s = control.NamingContainer;

                        #region hidden input

                        if (hiddenCtr != null)
                        {
                            HtmlInputHidden hInput = (HtmlInputHidden)hiddenCtr;
                            if (hInput != null)
                            {
                                hInput.Value = val.ToString().Trim();
                            }
                        }

                        #endregion hidden input

                        #region normal control

                        if (control == null) continue;
                        if (control is HtmlGenericControl)
                        {
                            HtmlGenericControl txt = (HtmlGenericControl)control;
                            txt.InnerHtml = val.ToString().Trim();
                        }
                        if (control is HtmlInputText)
                        {
                            HtmlInputText txt = (HtmlInputText)control;
                            txt.Value = val.ToString().Trim();
                        }
                        if (control is TextBox)
                        {
                            TextBox txt = (TextBox)control;
                            txt.Text = val.ToString().Trim();
                        }
                        if (control is HtmlSelect)
                        {
                            HtmlSelect txt = (HtmlSelect)control;

                            if (val.ToString().Trim().ToUpper() == "TRUE"
                                || val.ToString().Trim().ToUpper() == "FALSE")
                            {
                                txt.Value = ((val.ToString().Trim().ToUpper() == "TRUE") ? "1" : "0");
                            }
                            else
                            {
                                txt.Value = val.ToString().Trim();
                            }
                        }
                        if (control is HtmlInputHidden)
                        {
                            HtmlInputHidden txt = (HtmlInputHidden)control;
                            txt.Value = val.ToString().Trim();
                        }
                        if (control is HtmlInputPassword)
                        {
                            HtmlInputPassword txt = (HtmlInputPassword)control;
                            txt.Value = val.ToString().Trim();
                        }
                        if (control is Label)
                        {
                            Label txt = (Label)control;
                            txt.Text = val.ToString().Trim();
                        }
                        if (control is HtmlInputCheckBox)
                        {
                            HtmlInputCheckBox chk = (HtmlInputCheckBox)control;
                            chk.Checked = val.ToSafeInt32(0) == 1 ? true : false;
                        }
                        if (control is HtmlTextArea)
                        {
                            HtmlTextArea area = (HtmlTextArea)control;
                            area.Value = val.ToString().Trim();
                        }
                        if (control is DropDownList)
                        {
                            DropDownList drp = (DropDownList)control;
                            drp.SelectedValue = val.ToString().Trim();
                        }

                        #endregion normal control
                    }
                }
            }
        }

        #endregion "控件赋值"

        #region "控件获值"

        /// <summary>
        /// 获取服务器控件值
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static Hashtable GetWebControls(Control page)
        {
            Hashtable ht = new Hashtable();
            int size = HttpContext.Current.Request.Params.Count;
            for (int i = 0; i < size; i++)
            {
                string id = HttpContext.Current.Request.Params.GetKey(i);
                Control control = page.FindControl(id);
                if (control == null) continue;
                control = page.FindControl(id);
                if (control is HtmlInputText)
                {
                    HtmlInputText txt = (HtmlInputText)control;
                    ht[txt.ID] = txt.Value.Trim();
                }
                if (control is HtmlSelect)
                {
                    HtmlSelect txt = (HtmlSelect)control;
                    ht[txt.ID] = txt.Value.Trim();
                }
                if (control is HtmlInputHidden)
                {
                    HtmlInputHidden txt = (HtmlInputHidden)control;
                    ht[txt.ID] = txt.Value.Trim();
                }
                if (control is HtmlInputPassword)
                {
                    HtmlInputPassword txt = (HtmlInputPassword)control;
                    ht[txt.ID] = txt.Value.Trim();
                }
                if (control is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox chk = (HtmlInputCheckBox)control;
                    ht[chk.ID] = chk.Checked ? 1 : 0;
                }
                if (control is HtmlTextArea)
                {
                    HtmlTextArea area = (HtmlTextArea)control;
                    ht[area.ID] = area.Value.Trim();
                }
            }
            return ht;
        }

        #endregion "控件获值"
    }
}