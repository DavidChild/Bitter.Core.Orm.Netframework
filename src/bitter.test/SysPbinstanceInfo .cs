using Bitter.Core;
using Bitter.Tools.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bitter.test
{
    [TableName("sys_pbinstance")]
    public class SysPbinstanceInfo : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Display(Name = @"")]
        public virtual String id { get; set; }

        /// <summary>
        /// 

        /// </summary>
        [Display(Name = @"")]
        public virtual String pid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual String pname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual String pmethod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual String picon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual Int32? peopletype { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual String modelcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual Int32? ptype { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual Int32? ordernum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual Int32? btntype { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual Int32? oldtype { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual DateTime? createtime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual DateTime? modifytime { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual String createuserid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual String modifyuserid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual Int32? isdeleted { get; set; }

    }

}
