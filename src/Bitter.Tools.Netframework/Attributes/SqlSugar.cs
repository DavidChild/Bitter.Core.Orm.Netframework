﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Tools.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class SugarTable : Attribute
    {
        private SugarTable() { }
        public string TableName { get; set; }
        public SugarTable(string tableName)
        {
            this.TableName = tableName;
        }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class SugarColumn : Attribute
    {
        private string _ColumnName;
        public string ColumnName
        {
            get { return _ColumnName; }
            set { _ColumnName = value; }
        }

        private bool _IsIgnore;
        public bool IsIgnore
        {
            get { return _IsIgnore; }
            set { _IsIgnore = value; }
        }

        private bool _IsPrimaryKey;
        public bool IsPrimaryKey
        {
            get { return _IsPrimaryKey; }
            set { _IsPrimaryKey = value; }
        }

        private bool _IsIdentity;
        public bool IsIdentity
        {
            get { return _IsIdentity; }
            set { _IsIdentity = value; }
        }

        private string _MappingKeys;
        public string MappingKeys
        {
            get { return _MappingKeys; }
            set { _MappingKeys = value; }
        }

        private string _ColumnDescription;
        public string ColumnDescription
        {
            get { return _ColumnDescription; }
            set { _ColumnDescription = value; }
        }
    }
}
