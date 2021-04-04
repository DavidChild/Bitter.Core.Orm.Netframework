using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Bitter.Tools;
using Bitter.Tools.Utils;
namespace Bitter.Core
{
    public class FindQuery: BaseQuery
    {
          DatabaseProperty dcc { get; set; }
        ExcutParBag_Excut bag { get; set; }
        public FindQuery(string commandText, dynamic dynamicParms, string targetdb = null)
        {
            excutParBag = new ExcutParBag_Excut();
            this.SetTargetDb(targetdb.ToSafeString());
            ((ExcutParBag_Excut)excutParBag).excutEnum = ExcutEnum.ExcutQuery;
            ((ExcutParBag_Excut)excutParBag).commandText = commandText;
            ((ExcutParBag_Excut)excutParBag).dynamicParma = new List<dynamic>();
            if (dynamicParms != null)
            {
                ((ExcutParBag_Excut)excutParBag).dynamicParma.Add(dynamicParms);
            }
            
        }
        /// <summary>
        /// 追加 where 1=1 
        /// </summary>
        /// <returns></returns>
        public FindQuery BeginWhere(string beginwhere, dynamic dynamicParms)
        {
            StringBuilder strbuild = new StringBuilder(((ExcutParBag_Excut)excutParBag).commandText);
            strbuild.AppendLine(" where  "+ beginwhere);
            ((ExcutParBag_Excut)excutParBag).commandText = strbuild.ToString();
            if (dynamicParms != null)
            {
                ((ExcutParBag_Excut)excutParBag).dynamicParma.Add(dynamicParms);
            }
            return this;
        }
        /// <summary>
        /// 追加 and  条件
        /// </summary>
        /// <param name="andwhere"></param>
        /// <param name="dynamicParms"></param>
        /// <returns></returns>
        public FindQuery AndWhere(string andwhere, dynamic dynamicParms)
        {
            StringBuilder strbuild =new StringBuilder(((ExcutParBag_Excut)excutParBag).commandText);
            strbuild.AppendLine(" and (" +andwhere+") ");
            ((ExcutParBag_Excut)excutParBag).commandText = strbuild.ToString();
            if (dynamicParms != null)
            {
                ((ExcutParBag_Excut)excutParBag).dynamicParma.Add(dynamicParms);
            }
             return this;
        }


        public string CommandText
        {
            get
            {
                return ((ExcutParBag_Excut)excutParBag).commandText;
            }
            set
            {
                ((ExcutParBag_Excut)excutParBag).commandText = value;
            }
        }
        public FindQuery AddParms(dynamic dynamicParms)
        {
            if (dynamicParms != null)
            {
                ((ExcutParBag_Excut)excutParBag).dynamicParma.Add(dynamicParms);
                
            }
            return this;
        }

        /// <summary>
        ///  获取数据
        /// </summary>
        /// <typeparam name="TOut">转换类型</typeparam>
        /// <param name="targetdb">执行的数据库</param>
        /// <returns>持久层如果没数据，那么就返回默认的default(IEnumerable<TOut>)</returns>
        public IEnumerable<TOut> Find<TOut>(string targetdb = null)
        {
            var dt = Find();
            if (dt != null)
            {
                return dt.MapTo<IEnumerable<TOut>>();
            }
            return default(IEnumerable<TOut>);
        }
        /// <summary>
        /// 执行获取数据
        /// </summary>
        /// <param name="targetdb">执行的数据库</param>
        /// <returns>可能会返回对象为null的情况</returns>
        public DataTable Find() 
        {
            this.Convert(this.Targetdb);
            DataTable dt = this.ReturnDataTable();
            if (dt != null)
            {
                return dt;
            }
            return null;
        }
        /// <summary>
        /// 获取总数量
        /// </summary>
        /// <param name="targetdb">执行的数据库</param>
        /// <returns>可能会返回对象为null的情况</returns>
        public int FindCount()
        {
            this.Convert(this.Targetdb);
            DataTable dt = this.ReturnDataTable();
            if (dt == null||dt.Rows.Count==0)
            {
                return 0;
            }
            return dt.Rows.Count;
        }







    }
}
