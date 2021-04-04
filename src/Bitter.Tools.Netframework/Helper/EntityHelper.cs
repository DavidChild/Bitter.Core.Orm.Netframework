using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Reflection;
using Bitter.Tools;
using Bitter.Tools.Utils;
using System.Collections;
using Bitter.Flow.FlowDialog;
using System.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Bitter.Tools.Helper
{
    /// <summary>
    /// 创建人： 陈志航
    /// 创建时间：2016年11月30日15:00:21
    /// </summary>
    public class EntityHelper
    {
        /// <summary>
        /// 为控件赋值
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="page">页面</param>
        /// <param name="entity">实体</param>
        /// <param name="con">控件类型</param>
        public static void SetEntityValueToControl<T>(Page page, T entity)
        {
            foreach (System.Reflection.PropertyInfo pro in typeof(T).GetProperties())
            {
                try
                {
                    var value = pro.GetValue(entity, null);
                    if (value != null)
                    {
                        Control control = page.Form.FindControl("txt" + pro.Name);
                        Control hiddenCtr = page.Form.FindControl("hide" + pro.Name);
                        CusControlObj em = (CusControlObj)page.Form.FindControl(pro.Name);
                       
                        
                        #region 给自定义select赋值
                        if (em != null)
                        {
                            em.IdValue = pro.GetValue(entity, null).ToString();
                        }
                        #endregion
                        #region hidden input
                        if (hiddenCtr != null)
                        {
                            HtmlInputHidden hInput = (HtmlInputHidden)hiddenCtr;
                            if (hInput != null)
                            {
                                hInput.Value = pro.GetValue(entity, null).ToString();
                            }
                        }
                        #endregion
                        #region normal control
                        if (control == null) continue;
                        if (control is HtmlGenericControl)
                        {
                            HtmlGenericControl txt = (HtmlGenericControl)control;
                            txt.InnerHtml = pro.GetValue(entity, null).ToString();
                        }
                        if (control is HtmlInputText)
                        {
                            HtmlInputText txt = (HtmlInputText)control;
                            txt.Value = pro.GetValue(entity, null).ToString();
                        }
                        if (control is TextBox)
                        {
                            TextBox txt = (TextBox)control;
                            txt.Text = pro.GetValue(entity, null).ToString();
                        }
                        if (control is HtmlSelect)
                        {
                            HtmlSelect txt = (HtmlSelect)control;

                            if (pro.GetValue(entity, null).ToString().ToUpper() == "TRUE"
                                || pro.GetValue(entity, null).ToString().ToUpper() == "FALSE")
                            {
                                txt.Value = ((pro.GetValue(entity, null).ToString().ToUpper() == "TRUE") ? "1" : "0");
                            }
                            else
                            {
                                txt.Value = pro.GetValue(entity, null).ToString();
                            }
                        }
                        if (control is HtmlInputHidden)
                        {
                            HtmlInputHidden txt = (HtmlInputHidden)control;
                            txt.Value = pro.GetValue(entity, null).ToString();
                        }
                        if (control is HtmlInputPassword)
                        {
                            HtmlInputPassword txt = (HtmlInputPassword)control;
                            txt.Value = pro.GetValue(entity, null).ToString();
                        }
                        if (control is Label)
                        {
                            Label txt = (Label)control;
                            txt.Text = pro.GetValue(entity, null).ToString();
                        }
                        if (control is HtmlInputCheckBox)
                        {
                            HtmlInputCheckBox chk = (HtmlInputCheckBox)control;
                            chk.Checked = pro.GetValue(entity, null).ToSafeBool().Value;
                        }
                        if (control is HtmlTextArea)
                        {
                            HtmlTextArea area = (HtmlTextArea)control;
                            area.Value = pro.GetValue(entity, null).ToString();
                        }
                        if (control is DropDownList)
                        {
                            DropDownList drp = (DropDownList)control;
                            drp.SelectedValue = pro.GetValue(entity, null).ToString();
                        }
                         #region 给自定义用户控件赋值
                        if (control is UserControl)
                        {
                            UserControl userControl = (UserControl)control;
                            System.Reflection.PropertyInfo proInfo = userControl.GetType().GetProperty("Value");
                            if (proInfo != null)
                            {
                                proInfo.SetValue(userControl, pro.GetValue(entity, null).ToString(), null);
                            }
                        }
                        #endregion
                        #endregion
                    }
                }
                catch(Exception e)
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// 将DataTable的第一行值赋值给页面上与列名同名的控件
        /// 控件名必须有 txt/hide 前缀 
        /// 自定义控件ID与列名同名即可
        /// </summary>
        /// <param name="page">需要赋值的页面对象</param>
        /// <param name="dt">dt</param>
        public static void SetEntityValueToControl(Page page, DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    Control control = page.Form.FindControl("txt" + column.ColumnName);
                    Control hiddenCtr = page.Form.FindControl("hide" + column.ColumnName);
                    CusControlObj em = (CusControlObj)page.Form.FindControl(column.ColumnName);

                    #region 给自定义select赋值

                    if (em != null)
                    {
                        em.IdValue = dt.Rows[0][column.ColumnName].ToString();
                    }
                    #endregion
                    #region hidden input
                    if (hiddenCtr != null)
                    {
                        HtmlInputHidden hInput = (HtmlInputHidden)hiddenCtr;
                        if (hInput != null)
                        {
                            hInput.Value = dt.Rows[0][column.ColumnName].ToString();
                        }
                    }
                    #endregion
                    #region normal control
                    if (control == null) continue;
                    if (control is HtmlGenericControl)
                    {
                        HtmlGenericControl txt = (HtmlGenericControl)control;
                        txt.InnerHtml = dt.Rows[0][column.ColumnName].ToString();
                    }
                    if (control is HtmlInputText)
                    {
                        HtmlInputText txt = (HtmlInputText)control;
                        txt.Value = dt.Rows[0][column.ColumnName].ToString();
                    }
                    if (control is TextBox)
                    {
                        TextBox txt = (TextBox)control;
                        txt.Text = dt.Rows[0][column.ColumnName].ToString();
                    }
                    if (control is HtmlSelect)
                    {
                        HtmlSelect txt = (HtmlSelect)control;

                        if (dt.Rows[0][column.ColumnName].ToString().ToUpper() == "TRUE"
                            || dt.Rows[0][column.ColumnName].ToString().ToUpper() == "FALSE")
                        {
                            txt.Value = ((dt.Rows[0][column.ColumnName].ToString().ToUpper() == "TRUE") ? "1" : "0");
                        }
                        else
                        {
                            txt.Value = dt.Rows[0][column.ColumnName].ToString();
                        }
                    }
                    if (control is HtmlInputHidden)
                    {
                        HtmlInputHidden txt = (HtmlInputHidden)control;
                        txt.Value = dt.Rows[0][column.ColumnName].ToString();
                    }
                    if (control is HtmlInputPassword)
                    {
                        HtmlInputPassword txt = (HtmlInputPassword)control;
                        txt.Value = dt.Rows[0][column.ColumnName].ToString();
                    }
                    if (control is Label)
                    {
                        Label txt = (Label)control;
                        txt.Text = dt.Rows[0][column.ColumnName].ToString();
                    }
                    if (control is HtmlInputCheckBox)
                    {
                        HtmlInputCheckBox chk = (HtmlInputCheckBox)control;
                        chk.Checked = dt.Rows[0][column.ColumnName].ToString().ToUpper() == "TRUE" ? true : false;
                    }
                    if (control is HtmlTextArea)
                    {
                        HtmlTextArea area = (HtmlTextArea)control;
                        area.Value = dt.Rows[0][column.ColumnName].ToString();
                    }
                    if (control is DropDownList)
                    {
                        DropDownList drp = (DropDownList)control;
                        drp.SelectedValue = dt.Rows[0][column.ColumnName].ToString();
                    }
                    #region 给自定义用户控件赋值
                    if (control is UserControl)
                    {
                        UserControl userControl = (UserControl)control;
                        System.Reflection.PropertyInfo proInfo = userControl.GetType().GetProperty("Value");
                        if (proInfo != null)
                        {
                            proInfo.SetValue(userControl, dt.Rows[0][column.ColumnName].ToString(), null);
                        }
                    }
                    #endregion
                    #endregion
                }

            }
        }
        /// <summary>
        /// 把页面控件上的值赋值给实体对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="page">页面</param>
        /// <param name="type">反射类型</param>
        /// <param name="entity">返回实体类</param>
        public static void SetControlValueToEntity<T>(Page page, ref T entity)
        {
            foreach (System.Reflection.PropertyInfo pro in typeof(T).GetProperties())
            {
                try
                {
                    Control control = page.Form.FindControl("txt" + pro.Name);
                    Control hiddenCtr = page.Form.FindControl("hide" + pro.Name);
                    CusControlObj em = (CusControlObj)page.Form.FindControl(pro.Name);
                    #region 取自定义控件select的值
                    if (em != null)
                    {
                        SetEntityValue<T>(pro, ref entity, em.IdValue);
                    }
                    #endregion
                    #region hidden input
                    if (hiddenCtr != null)
                    {
                        HtmlInputHidden hInput = (HtmlInputHidden)hiddenCtr;
                        if (hInput != null)
                        {
                            SetEntityValue<T>(pro, ref entity, hInput.Value);
                        }
                    }
                    #endregion
                    #region normal control
                    if (control == null) continue;

                    if (control is HtmlGenericControl)
                    {
                        HtmlGenericControl txt = (HtmlGenericControl)control;
                        SetEntityValue<T>(pro, ref entity, txt.InnerHtml);

                    }
                    if (control is HtmlInputText)
                    {
                        HtmlInputText txt = (HtmlInputText)control;
                        SetEntityValue<T>(pro, ref entity, txt.Value);
                    }
                    if (control is TextBox)
                    {
                        TextBox txt = (TextBox)control;
                        SetEntityValue<T>(pro, ref entity, txt.Text);
                    }
                    if (control is HtmlSelect)
                    {
                        HtmlSelect txt = (HtmlSelect)control;
                        SetEntityValue<T>(pro, ref entity, txt.Value);
                    }
                    if (control is HtmlInputHidden)
                    {
                        HtmlInputHidden txt = (HtmlInputHidden)control;
                        SetEntityValue<T>(pro, ref entity, txt.Value);
                    }
                    if (control is HtmlInputPassword)
                    {
                        HtmlInputPassword txt = (HtmlInputPassword)control;
                        SetEntityValue<T>(pro, ref entity, txt.Value);
                    }
                    if (control is Label)
                    {
                        Label txt = (Label)control;
                        SetEntityValue<T>(pro, ref entity, txt.Text);
                    }
                    if (control is HtmlInputCheckBox)
                    {
                        HtmlInputCheckBox chk = (HtmlInputCheckBox)control;
                        SetEntityValue<T>(pro, ref entity, chk.Checked.ToString());
                    }
                    if (control is HtmlTextArea)
                    {
                        HtmlTextArea area = (HtmlTextArea)control;
                        SetEntityValue<T>(pro, ref entity, area.Value);
                    }
                    if (control is DropDownList)
                    {
                        DropDownList drp = (DropDownList)control;
                        SetEntityValue<T>(pro, ref entity, drp.SelectedValue);
                    }
                     #region 取自定义用户控件的值
                    if (control is UserControl)
                    {
                        UserControl userControl = (UserControl)control;
                        Type userType = userControl.GetType();
                        System.Reflection.PropertyInfo proInfo = userType.GetProperty("Value");
                        if (proInfo != null)
                        {
                            SetEntityValue<T>(pro, ref entity, proInfo.GetValue(userControl, null).ToString());
                        }
                    }
                    #endregion
                    #endregion
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 对比两个实体类属性值的差异
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="oldMod">原实体类</param>
        /// <param name="newMod">新实体类</param>
        /// <returns>差异记录</returns>
        public static string CompareEntityValue<T>(T oldMod, T newMod)
        {
            Type typeDescription = typeof(DisplayAttribute);
            if (oldMod == null || newMod == null) { return ""; }
            string updateData = "";
            System.Reflection.PropertyInfo[] mPi = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            for (int i = 0; i < mPi.Length; i++)
            {
                System.Reflection.PropertyInfo pi = mPi[i];
                object[] arr = pi.GetCustomAttributes(typeDescription, true);
                string atrr = arr.Length > 0 ? ((DisplayAttribute)arr[0]).Name : pi.Name;

                object oldObj = pi.GetValue(oldMod, null);
                object newObj = pi.GetValue(newMod, null);
                string oldValue = oldObj == null ? "" : oldObj.ToString();
                string newValue = newObj == null ? "" : newObj.ToString();
                if (oldValue != newValue)
                {
                    oldValue = oldValue == "" ? "空" : oldValue;
                    newValue = newValue == "" ? "空" : newValue;
                    updateData += atrr + "：由 " + oldValue + " 改成 " + newValue + "<br/>";
                }
            }
            return updateData;
        }

        /// <summary>
        /// 实体差异对比
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="oldMod">原对象</param>
        /// <param name="newMod">新对象</param>
        /// <returns>返回datatable，结构【属性名，描述，原始值，更改值】</returns>
        public static DataTable CompareEntityValueDT<T>(T oldMod, T newMod)
        {
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Display");
            dt.Columns.Add("OldValue");
            dt.Columns.Add("NewValue");
            Type typeDescription = typeof(DisplayAttribute);
            if (oldMod == null || newMod == null) { return dt; }            
            System.Reflection.PropertyInfo[] mPi = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            for (int i = 0; i < mPi.Length; i++)
            {
                System.Reflection.PropertyInfo pi = mPi[i];
                object[] arr = pi.GetCustomAttributes(typeDescription, true);
                string atrr = arr.Length > 0 ? ((DisplayAttribute)arr[0]).Name : pi.Name;
                object oldObj = pi.GetValue(oldMod, null);
                object newObj = pi.GetValue(newMod, null);
                string oldValue = oldObj == null ? "" : oldObj.ToString();
                string newValue = newObj == null ? "" : newObj.ToString();
                if (oldValue.Replace(" ", "") != newValue.Replace(" ", ""))
                {
                    dt.Rows.Add(pi.Name, atrr, oldValue, newValue);
                }
            }
            return dt;
        }


        private static void SetEntityValue<T>(System.Reflection.PropertyInfo pro, ref T entity, string value)
        {
            if (pro.PropertyType.ToString().Contains("System.DateTime")) pro.SetValue(entity, value.ToSafeDateTime(), null);
            if (pro.PropertyType.ToString().Contains("System.Decimal")) pro.SetValue(entity, value.ToSafeDecimal(), null);
            if (pro.PropertyType.ToString().Contains("System.Int")) pro.SetValue(entity, value.ToSafeInt32(), null);
            if (pro.PropertyType.ToString().Contains("System.Int32")) pro.SetValue(entity, value.ToSafeInt32(), null);
            if (pro.PropertyType.ToString().Contains("System.Int64")) pro.SetValue(entity, value.ToSafeInt32(), null);
            if (pro.PropertyType.ToString().Contains("System.Boolean")) pro.SetValue(entity, value.ToSafeBool(), null);
            if (pro.PropertyType.ToString().Contains("System.String")) { pro.SetValue(entity, value, null); }
        }

        public static void SetControlReadOnly(Control ctr)
        {
            if (ctr is TextBox)
            {
                TextBox txtControl = (TextBox)ctr;
                txtControl.ReadOnly = true;
                txtControl.Enabled = false;

            }
            else if (ctr is CusControlObj)
            {
                CusControlObj em = (CusControlObj)ctr;
                em.Enabled = false;
            }
            else if (ctr is RadioButton)
            {
                RadioButton btn = (RadioButton)ctr;
                btn.Enabled = false;

            }
            else if (ctr is RadioButtonList)
            {
                RadioButtonList btn = (RadioButtonList)ctr;
                btn.Enabled = false;
            }

            else if (ctr is CheckBox)
            {
                CheckBox cb = (CheckBox)ctr;
                cb.Enabled = false;
            }
            else if (ctr is DropDownList)
            {
                DropDownList list = (DropDownList)ctr;
                list.Enabled = false;
            }

            else if (ctr is HtmlTextArea)
            {
                HtmlTextArea cb = (HtmlTextArea)ctr;
                cb.Attributes.Add("readonly", "");
                cb.Disabled = true;
            }
            else if (ctr is HtmlSelect)
            {
                HtmlSelect rb = (HtmlSelect)ctr;
                rb.Disabled = true;
            }

            else if (ctr is HtmlInputCheckBox)
            {
                HtmlInputCheckBox rb = (HtmlInputCheckBox)ctr;
                rb.Disabled = true;
            }
            else if (ctr is HtmlInputRadioButton)
            {
                HtmlInputRadioButton rb = (HtmlInputRadioButton)ctr;
                rb.Disabled = true;
            }
            else if (ctr is HtmlInputText)
            {
                HtmlInputControl input = (HtmlInputControl)ctr;
                input.Attributes.Add("readonly", "");
                input.Disabled = true;
            }
            else
                foreach (Control ctr1 in ctr.Controls)
                {
                    SetControlReadOnly(ctr1);
                }
        }

        public static void SetControlEdit(Control ctr)
        {
            if (ctr is TextBox)
            {
                TextBox txtControl = (TextBox)ctr;
                txtControl.ReadOnly = false;
                txtControl.Enabled = true;

            }
            else if (ctr is CusControlObj)
            {
                CusControlObj em = (CusControlObj)ctr;
                em.Enabled = true;
            }
            else if (ctr is RadioButton)
            {
                RadioButton btn = (RadioButton)ctr;
                btn.Enabled = true;

            }
            else if (ctr is RadioButtonList)
            {
                RadioButtonList btn = (RadioButtonList)ctr;
                btn.Enabled = true;
            }

            else if (ctr is CheckBox)
            {
                CheckBox cb = (CheckBox)ctr;
                cb.Enabled = true;
            }
            else if (ctr is DropDownList)
            {
                DropDownList list = (DropDownList)ctr;
                list.Enabled = true;
            }

            else if (ctr is HtmlTextArea)
            {
                HtmlTextArea cb = (HtmlTextArea)ctr;
                cb.Attributes.Remove("readonly");
                cb.Attributes.Remove("disabled");
                cb.Disabled = false;
            }
            else if (ctr is HtmlSelect)
            {
                HtmlSelect rb = (HtmlSelect)ctr;
                rb.Attributes.Remove("disabled");
                rb.Disabled = false;
            }

            else if (ctr is HtmlInputCheckBox)
            {
                HtmlInputCheckBox rb = (HtmlInputCheckBox)ctr;
                rb.Attributes.Remove("disabled");
                rb.Disabled = false;
            }
            else if (ctr is HtmlInputRadioButton)
            {
                HtmlInputRadioButton rb = (HtmlInputRadioButton)ctr;
                rb.Attributes.Remove("disabled");
                rb.Disabled = false;
            }
            else if (ctr is HtmlInputText)
            {
                HtmlInputControl input = (HtmlInputControl)ctr;
                input.Attributes.Remove("disabled");
                input.Attributes.Remove("readonly");
                input.Disabled = false;
            }
            else
                foreach (Control ctr1 in ctr.Controls)
                {
                    SetControlReadOnly(ctr1);
                }
        }
    }
}