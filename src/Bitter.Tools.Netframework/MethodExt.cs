using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Tools
{
    public static class MethodExt
    {
        /// <summary>
        /// 手机号校验
        /// 130~139
        /// 143 145 146 147 148 149
        /// 150 ~159
        /// 166
        /// 170 171 172 173 175 176 177
        /// 180~189
        /// 192 198 199 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CheckMobile(this string str)
        {
            if (string.IsNullOrEmpty(str)) return false;

            /*130~139
            143 145 147  149
            150 ~159
            166
            170 171 172 173 175 176 177
            180~189
            191 192 193 198 199*/
            string patten = @"^[1](([385][0-9])|([4][356789])|([6][6])|([7][01235678])|([9][12389]))[0-9]{8}$";

            return System.Text.RegularExpressions.Regex.IsMatch(str.Trim(), patten);
        }
    }
}
