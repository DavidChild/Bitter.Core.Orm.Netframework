using System;
using System.Data;
using System.Linq;

namespace Bitter.Tools.Utils
{
    /********************************************************************************
    ** auth： Jason
    ** date： 2016/12/26 9:13:15
    ** desc：
    ** Ver.:  V1.0.0
    ** Copyright (C) 2016 Bitter 版权所有。
    *********************************************************************************/

    public class TreeUtil
    {
        public static DataTable GetDt(DataTable dt, string FName, string paranetIDName, string childIDName, string fparentID, string x)
        {
            var dx = from t in dt.AsEnumerable().Where(p => p.Field<Int32>(paranetIDName).ToSafeString("-1") == fparentID) select t;
            if (dx.AsEnumerable().Count() != 0)
            {
                return GetDt(dt, FName, paranetIDName, childIDName, fparentID, x);
            }
            else
            {
                dx.ToList().ForEach(t =>
                {
                    t[FName] = "__" + x + t.Field<string>(FName);
                });
            }
            return dt;
        }

        /// <summary>
        ///Tree
        /// </summary>
        public static DataTable GetTreeData(DataTable dt, string FName, string paranetIDName, string childIDName, string rootID, string x)
        {
            var dz =
                (from t in
                     dt.AsEnumerable()
                         .Where(
                             p =>
                                 p.Field<Int32>(paranetIDName).ToSafeString("0") == rootID)
                 select t).ToList();
            foreach (DataRow r in dz.AsEnumerable())
            {
                GetDt(dt, FName, paranetIDName, childIDName, r[childIDName].ToSafeString("0"), x);
            }
            return dt;
        }
    }
}