
using Bitter.Tools;
using System.Collections.Generic;
using System.Configuration;
using Bitter.Tools.Utils;
namespace Bitter.Core
{
    public sealed class DBSettings
    {
       

        public static string GetConnectionInfo(string name)
        {

            /*** 对各版本数据库的支持
             *
             * web.config连接字符串中加入providerName特性
             * Aceess数据库--->providerName="System.Data.OleDb"
             * Oracle 数据库--->providerName="System.Data.OracleClient"或者providerName="Oracle.DataAccess.Client"
             * SQLite数据库--->providerName="System.Data.SQLite"
             * sql     数据库--->providerName="System.Data.SqlClient"
             * MySQL数据库--->providerName="MySql.Data.MySqlClient"
             * * **/


            ConnectionStringSettings connectionStringSettings = Configsetting.Appsettings.DbConnection(name + ".Reader");
            if (connectionStringSettings == null)
            {
                return string.Empty;
            }
            else
            {
                return connectionStringSettings.ConnectionString;

            }
        }


        public static DatabaseProperty GetDatabaseProperty(string name)
        {
            DatabaseConnection reader = default(DatabaseConnection);
            reader.DatabaseType = DatabaseType.MSSQLServer;


            ConnectionStringSettings connectionStringSettings = Configsetting.Appsettings.DbConnection(name + ".Reader");
            var connec = Configsetting.Appsettings["SQLServerDBConnectionStr"].ToSafeString();
            if (connectionStringSettings == null && (string.IsNullOrEmpty(connec)))
            {
                reader.ConnectionString = string.Empty;
            }
            else if (connectionStringSettings!=null)
            {
                reader.ConnectionString = connectionStringSettings.ConnectionString;
                reader.DatabaseType = DbProvider.GetType(connectionStringSettings.ProviderName);
                if ((!string.IsNullOrEmpty(connectionStringSettings.ProviderName)) && connectionStringSettings.ProviderName.Contains("2012"))
                {
                    reader.Version = 2012;

                }

            }
            else
            {
                reader.ConnectionString = connec;

            }
            DatabaseConnection writer = default(DatabaseConnection);
            writer.DatabaseType = DatabaseType.MSSQLServer;
            writer.Version = 2008;
            ConnectionStringSettings connectionStringSettings2 = Configsetting.Appsettings.DbConnection(name + ".Writer");

            if (connectionStringSettings2 == null && string.IsNullOrEmpty(connec))
            {
                writer.ConnectionString = string.Empty;
            }
            else if (connectionStringSettings2 != null)
            {
                writer.ConnectionString = connectionStringSettings2.ConnectionString;
                writer.DatabaseType = DbProvider.GetType(connectionStringSettings.ProviderName);
                if ((!string.IsNullOrEmpty(connectionStringSettings2.ProviderName)) && connectionStringSettings2.ProviderName.Contains("2012"))
                {
                    writer.Version = 2012;
                }

            }
            else
            {
                writer.ConnectionString = connec;

            }
            return new DatabaseProperty(reader, writer);
        }
    }
}