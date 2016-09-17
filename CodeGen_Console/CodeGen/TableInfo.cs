using System;
using System.Collections.Generic;

namespace QLike.CodeGen
{
    /// <summary>
    /// Table
    /// </summary>
    public class TableInfo
    {
        private string name;

        /// <summary>
        /// Name of table
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string nameSpace;

        /// <summary>
        /// Name space
        /// </summary>
        public string NameSpace
        {
            get { return nameSpace; }
            set { nameSpace = value; }
        }

        private string productName;

        /// <summary>
        /// Product name
        /// </summary>
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        private string dbName;

        /// <summary>
        /// Database table name
        /// </summary>
        public string DbName
        {
            get { return dbName; }
            set { dbName = value; }
        }

        private IList<ColumnInfo> columns;

        /// <summary>
        /// Columns in the table
        /// </summary>
        public IList<ColumnInfo> Columns
        {
            get { return columns; }
            set { columns = value; }
        }

        public TableInfo()
        {
            this.name = string.Empty;
            this.columns = null;
        }
    }//end of class
}
