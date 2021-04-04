
using Bitter.Tools;
using System.Collections.Generic;
using System.Configuration;

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

            if (connectionStringSettings == null)
            {
                reader.ConnectionString = string.Empty;
            }
            else
            {
                reader.ConnectionString = connectionStringSettings.ConnectionString;
                reader.DatabaseType = DbProvider.GetType(connectionStringSettings.ProviderName);
                if ((!string.IsNullOrEmpty(connectionStringSettings.ProviderName)) && connectionStringSettings.ProviderName.Contains("2012"))
                {
                    reader.Version = 2012;

                }

            }
            DatabaseConnection writer = default(DatabaseConnection);
            writer.DatabaseType = DatabaseType.MSSQLServer;
            writer.Version = 2008;
            ConnectionStringSettings connectionStringSettings2 = Configsetting.Appsettings.DbConnection(name + ".Writer");
            if (connectionStringSettings2 == null)
            {
                writer.ConnectionString = string.Empty;
            }
            else
            {
                writer.ConnectionString = connectionStringSettings2.ConnectionString;
                writer.DatabaseType = DbProvider.GetType(connectionStringSettings.ProviderName);
                if ((!string.IsNullOrEmpty(connectionStringSettings2.ProviderName)) && connectionStringSettings2.ProviderName.Contains("2012"))
                {
                    writer.Version = 2012;
                }

            }
            return new DatabaseProperty(reader, writer);
        }
    }
}