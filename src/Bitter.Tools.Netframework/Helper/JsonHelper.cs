using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
namespace Bitter.Tools.Helper
{
    public class JsonHelper
    {
        /// <summary>
        /// string数组转json
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static string ArrayToJson(string[] strs)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strs.Length; i++)
            {
                sb.AppendFormat("'{0}':'{1}',", i + 1, strs[i]);
            }
            if (sb.Length > 0)
            {
                return ("{" + sb.ToString().TrimEnd(new char[] { ',' }) + "}");
            }
            return "";
        }

        /// <summary>
        /// list集合转json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="propertyname">Json字符串的key值</param>
        /// <returns></returns>
        public static string ArrayToJson<T>(List<T> list, string propertyname)
        {
            StringBuilder sb = new StringBuilder();
            if (list.Count > 0)
            {
                sb.Append("[{\"");
                sb.Append(propertyname);
                sb.Append("\":[");
                foreach (T t in list)
                {
                    sb.Append("\"");
                    sb.Append(t.ToString());
                    sb.Append("\",");
                }
                return (sb.ToString().TrimEnd(new char[] { ',' }) + "]}]");
            }
            return "";
        }

        /// <summary>
        /// datarow转json
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static string DataRowToJson(DataRow dr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            foreach (DataColumn dc in dr.Table.Columns)
            {
                sb.Append("\"");
                sb.Append(dc.ColumnName);
                sb.Append("\":\"");
                if (((dr[dc] != null) && (dr[dc] != DBNull.Value)) && (dr[dc].ToString() != ""))
                {
                    sb.Append(dr[dc]);
                }
                else
                {
                    sb.Append("&nbsp;");
                }
                sb.Append("\",");
            }
            sb = sb.Remove(0, sb.Length - 1);
            sb.Append("},");
            return sb.ToString();
        }

        /// <summary>
        /// datatable转json
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dtName">最外的key值</param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dt, string dtName = "")
        {
            StringBuilder sb = new StringBuilder();
            if (dtName != "")
            {
                sb.Append("{\"");
                sb.Append(dtName);
                sb.Append("\":");
            }
            sb.Append("[");

            if (IsExistRows(dt))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("{");
                    foreach (DataColumn dc in dr.Table.Columns)
                    {
                        sb.Append("\"");
                        sb.Append(dc.ColumnName);
                        sb.Append("\":\"");
                        if (((dr[dc] != null) && (dr[dc] != DBNull.Value)) && (dr[dc].ToString() != ""))
                        {
                            sb.Append(dr[dc]).Replace(@"\", "/");
                        }
                        else
                        {
                            sb.Append("&nbsp;");
                        }
                        sb.Append("\",");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
            if (dtName != "")
            {
                sb.Append("}");
            }
            return JsonCharFilter(sb.ToString());
        }

        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            return t;
        }

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }

       
        /// <summary>
        /// hashtable转json
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dtName">最外的key值</param>
        /// <returns></returns>
        public static string HashtableToJson(Hashtable data, string dtName = "")
        {
            StringBuilder sb = new StringBuilder();
            if (dtName != "")
            {
                sb.Append("{\"");
                sb.Append(dtName);
                sb.Append("\":");
                sb.Append("[");
            }
            else
            {
                sb.Append("{");
            }
            foreach (object key in data.Keys)
            {
                object value = data[key];
                sb.Append("\"");
                sb.Append(key);
                sb.Append("\":\"");
                if (!(string.IsNullOrEmpty(value.ToString()) || (value == DBNull.Value)))
                {
                    sb.Append(value).Replace(@"\", "/");
                }
                else
                {
                    sb.Append(" ");
                }
                sb.Append("\",");
            }
            sb = sb.Remove(sb.Length - 1, 1);

            if (dtName != "")
            {
                sb.Append("]");
                sb.Append("}");
            }
            else
            {
                sb.Append("}");
            }

            return JsonCharFilter(sb.ToString());
        }

        /// <summary>
        /// 泛型集合转json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string IListToJson<T>(IList<T> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (T t in list)
            {
                sb.Append(ObjectToJson<T>(t) + ",");
            }
            return (sb.ToString().TrimEnd(new char[] { ',' }) + "]");
        }

        /// <summary>
        /// 泛型集合转json，ClassName为最外层的key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public static string IListToJson<T>(IList<T> list, string ClassName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"" + ClassName + "\":[");
            foreach (T t in list)
            {
                sb.Append(ObjectToJson<T>(t) + ",");
            }
            return (sb.ToString().TrimEnd(new char[] { ',' }) + "]}");
        }

        /// <summary>
        /// 判断datatable是否为空
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsExistRows(DataTable dt)
        {
            return ((dt != null) && (dt.Rows.Count > 0));
        }

        /// <summary>
        /// 泛型集合转json，jsonName为空默认将泛型的实例名作为最外面的key，若不为空则jsonName为最外面的key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objlist"></param>
        /// <param name="jsonName"></param>
        /// <returns></returns>
        public static string ListToJson<T>(List<T> objlist, string jsonName)
        {
            string result = "{";
            if (jsonName.Equals(string.Empty))
            {
                object o = objlist[0];
                jsonName = o.GetType().ToString();
            }
            result = result + "\"" + jsonName + "\":[";
            bool firstline = true;
            foreach (object oo in objlist)
            {
                if (!firstline)
                {
                    result = result + "," + ObjectToJson(oo);
                }
                else
                {
                    result = result + ObjectToJson(oo);
                    firstline = false;
                }
            }
            return (result + "]}");
        }

        /// <summary>
        /// 泛型类转json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ObjectToJson<T>(T t)
        {
            StringBuilder sb = new StringBuilder();
            string json = "";
            if (t == null)
            {
                return json;
            }
            sb.Append("{");
            PropertyInfo[] properties = t.GetType().GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                sb.Append("\"" + pi.Name.ToString() + "\"");
                sb.Append(":");
                sb.Append("\"" + pi.GetValue(t, null) + "\"");
                sb.Append(",");
            }
            return (sb.ToString().TrimEnd(new char[] { ',' }) + "}");
        }

        /// <summary>
        /// 泛型类转json，className：json字符串最外层key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public static string ObjectToJson<T>(T t, string ClassName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"" + ClassName + "\":[");
            string json = "";
            if (t == null)
            {
                return json;
            }
            sb.Append("{");
            PropertyInfo[] properties = t.GetType().GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                sb.Append("\"" + pi.Name.ToString() + "\"");
                sb.Append(":");
                sb.Append("\"" + pi.GetValue(t, null) + "\"");
                sb.Append(",");
            }
            return (sb.ToString().TrimEnd(new char[] { ',' }) + "}]}");
        }

        /// <summary>
        /// 泛型集合转json，jsonName：json最外层key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="IL"></param>
        /// <param name="jsonName"></param>
        /// <returns></returns>
        public static string ObjectToJson<T>(IList<T> IL, string jsonName)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("{\"" + jsonName + "\":[");
            if (IL.Count > 0)
            {
                for (int i = 0; i < IL.Count; i++)
                {
                    PropertyInfo[] pis = Activator.CreateInstance<T>().GetType().GetProperties();
                    Json.Append("{");
                    for (int j = 0; j < pis.Length; j++)
                    {
                        Json.Append(string.Concat(new object[] { "\"", pis[j].Name.ToString(), "\":\"", pis[j].GetValue(IL[i], null), "\"" }));
                        if (j < (pis.Length - 1))
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < (IL.Count - 1))
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }

       
        /// <summary>
        /// 获取Json的Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="szJson"></param>
        /// <returns></returns>
        public static T ParseFromJson<T>(string szJson)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }

        /// <summary>
        /// 将对象序列化为JSON格式(标准，建议用此方法)
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o)
        {
            string json = JsonConvert.SerializeObject(o);

            return json;
        }

        private static List<string> GetObjectProperty(object o)
        {
            List<string> propertyslist = new List<string>();
            PropertyInfo[] propertys = o.GetType().GetProperties();
            foreach (PropertyInfo p in propertys)
            {
                propertyslist.Add(string.Concat(new object[] { "\"", p.Name.ToString(), "\":\"", p.GetValue(o, null), "\"" }));
            }
            return propertyslist;
        }

        private static string JsonCharFilter(string sourceStr)
        {
            return sourceStr;
        }

        private static string ObjectToJson(object o)
        {
            string result = "{";
            List<string> ls_propertys = new List<string>();
            ls_propertys = GetObjectProperty(o);
            foreach (string str_property in ls_propertys)
            {
                if (result.Equals("{"))
                {
                    result = result + str_property;
                }
                else
                {
                    result = result + "," + str_property;
                }
            }
            return (result + "}");
        }
    }
}