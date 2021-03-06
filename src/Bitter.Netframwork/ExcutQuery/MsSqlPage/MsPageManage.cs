using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Bitter.Core
{
    internal class MsPageManage
    {

        internal  static void SelectPageData(BaseQuery baseQuery)
        {
            var bag = ((ExcutParBag_Page)(baseQuery).excutParBag);
            baseQuery.CommandText = string.Empty;
            baseQuery.Parameters.Clear();
            if (bag._pageMode == PageMode.SelectCount)
            {

                GetPageDataBySelect(baseQuery);
            }
            else
            {
                GetPageDataByCount(baseQuery);
            }
        }

        private static void GetPageDataByCount(BaseQuery baseQuery)

        {
             
            var bag = (ExcutParBag_Page)(baseQuery.excutParBag);
            var sql = string.Empty;
            var selectTable = bag.pageTableName;
            var selectColumns = bag.pageColumns;
            var len = bag.commandText.ToLower().IndexOf("select") + 6;
            baseQuery.SetDynamicParameters((bag.dynamics));
            BaseQuery sqlTemp = new BaseQuery();
            sqlTemp.CommandText = bag.commandText;
            sqlTemp.CommandType = CommandType.Text;   
           
            for (int i = 0; i < baseQuery.Parameters.Count; i++)
            {
                SqlParameter sqlpa = baseQuery.Parameters[i];
                SqlParameter par = new SqlParameter();
                par.DbType = sqlpa.DbType;
                par.Direction = sqlpa.Direction;
                par.ParameterName = sqlpa.ParameterName;
                par.Value = sqlpa.Value;
                sqlTemp.Parameters.Add(par);
            }
            if (!bag.isPage)
            {
                bag.pageIndex = 1;
                bag.pageSize = 1;
            }
            if (bag.pageIndex == 1)
            {

                #region  //最新改动

                sqlTemp.CommandText = sqlTemp.CommandText.Insert(len, " TOP(@num) COUNT(0) over() CHEOKTOTALCOUNT,");
                if (!string.IsNullOrEmpty(bag.whereBuiler.ToString()))
                {
                    sqlTemp.CommandText = sqlTemp.CommandText + (string.IsNullOrWhiteSpace(bag.whereBuiler.ToString()) ? string.Empty : string.Format(" WHERE {0} ", bag.whereBuiler.ToString()));
                }
                if (!string.IsNullOrEmpty(bag.orderBy.ToString()))
                {
                    sqlTemp.CommandText = sqlTemp.CommandText + (string.IsNullOrWhiteSpace(bag.orderBy.ToString()) ? string.Empty : string.Format(" ORDER BY {0} ", bag.orderBy.ToString()));
                }
                #endregion
                sqlTemp.Parameters.Add("@num", bag.pageSize, SqlDbType.Int, 4);
            }
            else
            {
                sqlTemp.CommandText = sqlTemp.CommandText.Insert(len, string.Format("  ROW_NUMBER() OVER(ORDER BY {0}) as [num] ,COUNT(0) over() CHEOKTOTALCOUNT, ", string.IsNullOrEmpty(bag.orderBy.ToString()) ? "(SELECT 0)" : bag.orderBy.ToString()));
                if (!string.IsNullOrEmpty(bag.whereBuiler.ToString()))
                {
                    sqlTemp.CommandText = sqlTemp.CommandText +
                        (string.IsNullOrWhiteSpace(bag.whereBuiler.ToString()) ? string.Empty : string.Format(" WHERE {0} ", bag.whereBuiler.ToString()));
                }
                sqlTemp.CommandText = "SELECT * FROM (" + sqlTemp.CommandText + ") as [tab] where [num] between @Start and @End";
                sqlTemp.Parameters.Add("@Start", ((bag.pageIndex - 1) * bag.pageSize + 1), SqlDbType.Int, 4);
                sqlTemp.Parameters.Add("@End", (bag.pageIndex * bag.pageSize), SqlDbType.Int, 4);
            }


            try
            {
                if (!string.IsNullOrEmpty(bag._preWith))
                {
                    sqlTemp.CommandText =
                                        bag._preWith + @"
                                        --With 子句已经完毕
                                        " + sqlTemp.CommandText;

                }
                baseQuery.CommandText = sqlTemp.CommandText;
                baseQuery.Parameters = sqlTemp.Parameters;
            }
            finally
            {

            }
        }

        private static void GetPageDataBySelect (BaseQuery baseQuery)
        {
            var bag = (ExcutParBag_Page)(baseQuery.excutParBag);
            var sql = string.Empty;
            var selectTable = bag.pageTableName;
            var selectColumns = bag.pageColumns;
            var len = bag.commandText.ToLower().IndexOf("select") + 6;
            baseQuery.SetDynamicParameters((bag.dynamics));
            BaseQuery sqlTemp = new BaseQuery();
            sqlTemp.CommandText = bag.commandText;
            sqlTemp.CommandType = CommandType.Text;

            for (int i = 0; i < baseQuery.Parameters.Count; i++)
            {
                SqlParameter sqlpa = baseQuery.Parameters[i];
                SqlParameter par = new SqlParameter();
                par.DbType = sqlpa.DbType;
                par.Direction = sqlpa.Direction;
                par.ParameterName = sqlpa.ParameterName;
                par.Value = sqlpa.Value;
                sqlTemp.Parameters.Add(par);
            }
            //构建取总数的数据
            StringBuilder sqlReal = new StringBuilder();
            sqlReal.AppendLine("------查询TotalCount----");
            sqlReal.AppendLine("DECLARE @totalcountcheok INT");
            sqlReal.AppendLine("select");
            sqlReal.AppendLine("@totalcountcheok=count(0)");
            sqlReal.AppendLine("from");
            sqlReal.AppendLine(selectTable);

            if (!string.IsNullOrEmpty(bag.whereBuiler.ToString()))
            {
                sqlReal = sqlReal.AppendLine(string.IsNullOrWhiteSpace(bag.whereBuiler.ToString()) ? string.Empty : string.Format(" WHERE {0} ", bag.whereBuiler.ToString()));
            }
            sqlReal.AppendLine("-----------------SQL查询数据-----------------");
            sqlReal.AppendLine("");
            if (!bag.isPage)
            {
                bag.pageIndex = 1;
                bag.pageSize = 1;
            }
            if (bag.pageIndex == 1)
            {

                #region  //最新改动

                sqlTemp.CommandText = sqlTemp.CommandText.Insert(len, " TOP(@num) @totalcountcheok CHEOKTOTALCOUNT,");
                if (!string.IsNullOrEmpty(bag.whereBuiler.ToString()))
                {
                    sqlTemp.CommandText = sqlTemp.CommandText + (string.IsNullOrWhiteSpace(bag.whereBuiler.ToString()) ? string.Empty : string.Format(" WHERE {0} ", bag.whereBuiler.ToString()));
                }
                if (!string.IsNullOrEmpty(bag.orderBy.ToString()))
                {
                    sqlTemp.CommandText = sqlTemp.CommandText + (string.IsNullOrWhiteSpace(bag.orderBy.ToString()) ? string.Empty : string.Format(" ORDER BY {0} ", bag.orderBy.ToString()));
                }
                #endregion


                //sql = string.Format(@"select top(@num) {0} FROM {1} {2} order by {3}", 
                //string.IsNullOrWhiteSpace(selectColumns) ? "*" : selectColumns, selectTable, 
                //string.IsNullOrWhiteSpace(whereBuiler.ToString()) ? string.Empty : string.Format(" where {0} ", whereBuiler.ToString()), orderBy.ToString());

                sqlTemp.Parameters.Add("@num", bag.pageSize, SqlDbType.Int, 4);
            }
            else
            {
                sqlTemp.CommandText = sqlTemp.CommandText.Insert(len, string.Format("  ROW_NUMBER() OVER(ORDER BY {0}) as [num] ,@totalcountcheok CHEOKTOTALCOUNT, ", string.IsNullOrEmpty(bag.orderBy.ToString()) ? "(SELECT 0)" : bag.orderBy.ToString()));
                if (!string.IsNullOrEmpty(bag.whereBuiler.ToString()))
                {
                    sqlTemp.CommandText = sqlTemp.CommandText +
                        (string.IsNullOrWhiteSpace(bag.whereBuiler.ToString()) ? string.Empty : string.Format(" WHERE {0} ", bag.whereBuiler.ToString()));
                }
                sqlTemp.CommandText = "SELECT * FROM (" + sqlTemp.CommandText + ") as [tab] where [num] between @Start and @End";
                // sql = string.Format(@"SELECT * FROM ( SELECT {0},row_number() over(order by {3}) as [num] FROM {1} {2} ) as [tab] where [num] between @Start and @End", string.IsNullOrWhiteSpace(selectColumns) ? "*" : selectColumns, selectTable, string.IsNullOrWhiteSpace(whereBuiler.ToString()) ? string.Empty : string.Format(" where {0} ", whereBuiler.ToString()), orderBy.ToString());

                sqlTemp.Parameters.Add("@Start", ((bag.pageIndex - 1) * bag.pageSize + 1), SqlDbType.Int, 4);
                sqlTemp.Parameters.Add("@End", (bag.pageIndex * bag.pageSize), SqlDbType.Int, 4);
            }


            try
            {
                sqlTemp.CommandText = sqlReal.ToString() + sqlTemp.CommandText;
                if (!string.IsNullOrEmpty(bag._preWith))
                {
                    sqlTemp.CommandText =
                                        bag._preWith + @"
                                        --With 子句已经完毕

                                        " + sqlTemp.CommandText;

                }
                baseQuery.CommandText = sqlTemp.CommandText;
                baseQuery.Parameters = sqlTemp.Parameters;
              

            }
            finally
            {
            }
        }
    }
}
