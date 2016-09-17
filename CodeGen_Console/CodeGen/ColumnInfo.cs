using System;

namespace QLike.CodeGen
{
    /// <summary>
    /// Database column
    /// </summary>
    public class ColumnInfo
    {
        private string name;

        /// <summary>
        /// Field name
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private DataType dataType;

        /// <summary>
        /// Data type
        /// </summary>
        public DataType DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        private string typeName;

        /// <summary>
        /// User defined type name
        /// </summary>
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        private int fieldSize;

        /// <summary>
        /// Field size
        /// </summary>
        public int FieldSize
        {
            get { return fieldSize; }
            set { fieldSize = value; }
        }

        private string defaultValue;

        /// <summary>
        /// Default value
        /// </summary>
        public string DefaultValue
        {
          get { return defaultValue; }
          set { defaultValue = value; }
        }

        private bool isPrimary;

        /// <summary>
        /// If the column is primary key
        /// </summary>
        public bool IsPrimary
        {
            get { return isPrimary; }
            set { isPrimary = value; }
        }

        public ColumnInfo()
        {
            this.dataType = DataType.Other;
            this.defaultValue = string.Empty;
            this.fieldSize = 0;
            this.isPrimary = false;
            this.name = string.Empty;
        }
    }//end of class
}
