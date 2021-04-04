﻿using Bitter.Tools;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Xml;
using System.Data.SqlTypes;
using System.Data.Odbc;

namespace Bitter.Core
{
  internal class DbProvider
    {
      
        public const string MSSQLPROVIDER = "System.Data.SqlClient";
        public const string OLEDBPROVIDER = "System.Data.OleDb";
        public const string MYSQLPROVIDER = "MySql.Data.MySqlClient";
        public const string ORACLEPROVIDER = "System.Data.OracleClient";
        public const string SQLLITE = "System.Data.SqlLite";
        public static DatabaseType GetType(string typeInfo)
        {
            DatabaseType dbtype=DatabaseType.MSSQLServer;
            switch (typeInfo)
            {
                case DbProvider.MSSQLPROVIDER:
                    dbtype = DatabaseType.MSSQLServer;
                    break;
                case DbProvider.OLEDBPROVIDER:
                    dbtype = DatabaseType.OleDB;
                    break;
                case DbProvider.MYSQLPROVIDER:
                    dbtype = DatabaseType.MySql;
                    break;
                case DbProvider.ORACLEPROVIDER:
                    dbtype = DatabaseType.Oracle;
                    break;
                case DbProvider.SQLLITE:
                    dbtype = DatabaseType.SqlLite;
                    break;
                default:
                    dbtype = DatabaseType.MSSQLServer;
                    break;
            }
            
            return dbtype;
        }

        //底层操作包括的内容  IDbCommand ,IDbConnection,IDbDataAdapter

        public static IDbCommand GetDbCommand(DatabaseType dbtype)
        {
           
            switch (dbtype)
            {
                case DatabaseType.MSSQLServer:
                    return new SqlCommand();
                case DatabaseType.MySql:
                    return new MySql.Data.MySqlClient.MySqlCommand();
                case DatabaseType.SqlLite:
                    return null;
                 //   return new System.Data.SQLite.SQLiteCommand();
                default:
                    return new SqlCommand();
                    
            }
        }

        public static IDbConnection GetDbConnection(string connectionString, DatabaseType dbtype)
        {

            switch (dbtype)
            {
                case DatabaseType.MSSQLServer:
                    return new SqlConnection(connectionString);
                   
             
                case DatabaseType.MySql:
                    return new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                 
                default:
                    return new SqlConnection(connectionString);
                  
            }
        }
        public static IDbDataAdapter GetDbDataAdapter(DatabaseType dbtype,IDbCommand command)
        {

            switch (dbtype)
            {
                case DatabaseType.MSSQLServer:
                    return new SqlDataAdapter((SqlCommand)command);
                  
                //case DatabaseType.OleDB:
                //    return new System.Data.OleDb.OleDbDataAdapter((OleDbCommand)command);
                //    break;
                case DatabaseType.MySql:
                    return new MySql.Data.MySqlClient.MySqlDataAdapter((MySql.Data.MySqlClient.MySqlCommand)command);
                   
                default:
                    return new SqlDataAdapter((SqlCommand)command);
                  
            }
        }
        internal static IVdb GetVdb(DatabaseType dbtype)
        {
            IVdb vdb = new MSSQLVdb();
            switch (dbtype)
            {

                case DatabaseType.MSSQLServer:
                    vdb = new MSSQLVdb();
                    break;
                case DatabaseType.MySql:
                    vdb = new MySqlVdb();
                    break;
                default:
                    vdb = new MSSQLVdb();
                    break;

            }
            return vdb;

        }
       
    }

}
