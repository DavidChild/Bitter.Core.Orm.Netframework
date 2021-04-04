using Bitter.Tools;
using Bitter.Tools.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
namespace Bitter.Base.Config
{
    public static class OperateWebConfigManage
    {
        static string basedirectory = System.AppDomain.CurrentDomain.BaseDirectory;
        static string dbconfig = basedirectory + "configsetting/db.config";
        static string kvconfig = basedirectory + "configsetting/kv.config";
        public  delegate void AfterUpdatedkvSettigFile(bool isupdated);
       /// <summary>
       /// 配置文件更新之后,触发的事件
       /// </summary>
        public static event AfterUpdatedkvSettigFile EnvAfterUpdatedkvSettigFile;

        private static bool UpdateDb(ConfigValue item)
        {
            if (string.IsNullOrEmpty(item.Value) || string.IsNullOrWhiteSpace(item.Value))
            {
                return false;
            }
            var xml = "<?xml version=" + "\"1.0\"" + " encoding=\"utf-8\" ?><connectionStrings>{0}</connectionStrings>";

            if (!File.Exists(dbconfig))
            {
                return false;
            }
            try
            {
                var dbq = Bitter.Base.Xmlp.GetXmlData(dbconfig, "connectionStrings");
                XmlDocument localXmlDocument = new XmlDocument();

                xml = string.Format(xml, item.Value);
                localXmlDocument.LoadXml(xml);
                DataSet dss = new DataSet();
                StringReader read = new StringReader(localXmlDocument.SelectSingleNode("connectionStrings").OuterXml);
                dss.ReadXml(read);
                var orgsiz = JsonConvert.SerializeObject(dss);
                var localsiz = JsonConvert.SerializeObject(dbq);
                var orgsizMd5 = EncryptUtils.MD5(orgsiz);
                var localsizMd5 = EncryptUtils.MD5(localsiz);
                if (orgsizMd5 != localsizMd5)
                {
                    localXmlDocument.Save(dbconfig);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogService.Default.Fatal("db从配置中心更新数据失败：" + ex.Message + "参数:" + item.Value + "----xml:" + xml);
                return false;
            }


        }

        private static bool UpdateKv(Dictionary<string, string> dic)
        {

            try
            {




                if (!File.Exists(kvconfig))
                {
                    return false;
                }

                var kvds = Bitter.Base.Xmlp.GetXmlData(kvconfig, "appSettings");
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(kvconfig);

                //hjb//解决初始化没有配置的
                if (kvds.Tables.Count <= 0)
                {
                    var tb = new DataTable("kv");
                    tb.Columns.Add("key", typeof(string));
                    tb.Columns.Add("value", typeof(string));
                    kvds.Tables.Add(tb);


                }

                var kvdt = kvds.Tables[0];
                if (dic == null || dic.Count <= 0)
                {
                    return false;
                }

                var dtcopy = kvdt.Copy();
                foreach (KeyValuePair<string, string> item in dic)
                {
                    bool flag = true;

                    foreach (DataRow row in kvdt.Rows)
                    {
                        if (row["key"].ToSafeString() == item.Key && row["value"].ToSafeString() != item.Value)
                        {

                            var dtx = from p in dtcopy.AsEnumerable() where p.Field<string>("key") == item.Key select p;
                            if (dtx.Count() > 0)
                            {
                                dtx.FirstOrDefault()["value"] = item.Value;
                            }

                            flag = false;
                            break;
                        }
                        if (row["key"].ToSafeString() == item.Key)
                        {

                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        DataRow newr = dtcopy.NewRow();
                        newr["key"] = item.Key;
                        newr["value"] = item.Value;
                        dtcopy.Rows.Add(newr);
                    }
                }
                //比较两个键值集合 的MD5值
                kvdt.DefaultView.Sort = "key desc";
                dtcopy.DefaultView.Sort = "key desc";

                var orgsiz = JsonConvert.SerializeObject(dtcopy);
                var localsiz = JsonConvert.SerializeObject(kvdt);
                var orgsizMd5 = EncryptUtils.MD5(orgsiz);
                var localsizMd5 = EncryptUtils.MD5(localsiz);

                if (orgsizMd5 != localsizMd5)
                {

                    string xml = GetKvXml(dtcopy);
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(xml);
                    xmlDocument.Save(kvconfig);
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception ex)
            {
                LogService.Default.Fatal("kv从配置中心更新数据失败：" + ex.Message);
                return false;
            }


        }

        private static string GetKvXml(DataTable dt)
        {
            string xmlhead = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
            string xmlbody = "<appSettings>{0}</appSettings>";
            StringBuilder kvxml = new StringBuilder();

            foreach (DataRow row in dt.Rows)
            {
                string rep = "<add key=\"{0}\" value =\"{1}\"/>";
                rep = string.Format(rep, row["key"].ToSafeString(""), row["value"].ToSafeString(""));
                kvxml.Append(rep);
            }
            string xml = xmlhead + string.Format(xmlbody, kvxml.ToString().ToSafeString(""));
            return xml;
        }


        public static void WiteConfig(List<ConfigValue> configList, string item = "")
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            bool kvbl = false;
            bool dbbl = false;
            foreach (var conitem in configList)
            {
                if (conitem.ConfigType == 1)
                {
                    dbbl = UpdateDb(conitem);
                    //处理数据
                }
                else
                {
                    try
                    {
                        if (string.IsNullOrEmpty(conitem.Value) || string.IsNullOrWhiteSpace(conitem.Value))
                        {
                            continue;
                        }
                        dic.Add(conitem.Key, conitem.Value);

                    }
                    catch (Exception ex)
                    {

                        LogService.Default.Fatal("错误:kv配置中心存有相同的k键.");
                    }

                }
            }
            kvbl = UpdateKv(dic);

            if (kvbl)
            {
                try
                {

                   
                   


                    XmlDocument xml = new XmlDocument();
                    xml.Load(kvconfig);

                    //刷新ConfigurationManager 在内存中的缓存键值
                    foreach (XmlNode node in xml.SelectNodes("/appSettings/add"))
                    {

                        string key = node.Attributes["key"].Value;
                        string value = node.Attributes["value"].Value;
                        AddUpdateAppSettings(key, value);
                    }
                    FieldInfo fieldInfo = typeof(ConfigurationManager).GetField("s_initState", BindingFlags.NonPublic | BindingFlags.Static);
                    if(fieldInfo!=null) fieldInfo.SetValue(null,0);
                    ConfigurationManager.RefreshSection("appSettings");
                    if (EnvAfterUpdatedkvSettigFile != null)
                    {
                        EnvAfterUpdatedkvSettigFile(kvbl);
                    }

                }
                catch (Exception ex)
                {
                    LogService.Default.Debug("flush  memormenmoryy the kv appSettings fail:" + ex.Message);
                }




            }
            if (dbbl)
            {
                try
                {
                    ConfigurationManager.RefreshSection("connectionStrings");
                    XmlDocument xml = new XmlDocument();
                    xml.Load(dbconfig);

                    ConfigurationManager.ConnectionStrings.Clear();
                    //刷新ConfigurationManager 在内存中的缓存键值
                    foreach (XmlNode node in xml.SelectNodes("/connectionStrings/add"))
                    {
                        string name = node.Attributes["name"].Value;
                        string connections = node.Attributes["connectionString"].Value;
                        ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings() { Name = name, ConnectionString = connections });
                    }
                    LogService.Default.Debug("flush  menmory the db connectionString success.");
                }
                catch (Exception ex)
                {
                    LogService.Default.Debug("flush  memormenmoryy the db connectionString fail:" + ex.Message);
                }




            }





        }

        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                  
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

    }
}
