using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace QLike.CodeGen
{
    /// <summary>
    /// Class of code template
    /// </summary>
    public class Template
    {
        /// <summary>
        /// Generate Code
        /// </summary>
        /// <param name="table"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        internal static string GenerateCode(TableInfo table, string template)
        {
            StringBuilder templateBuilder = new StringBuilder(template);
            templateBuilder.Replace("[=NameSpace]", table.NameSpace);
            templateBuilder.Replace("[=ProductName]", table.ProductName);
            templateBuilder.Replace("[=DbName]", table.DbName);

            templateBuilder.Replace("[=Object]", Template.FormatName(table.Name, true));
            templateBuilder.Replace("[=Class]", Template.FormatName(table.Name, false));

            System.DateTime timeNow = System.DateTime.Now;
            string strDate = timeNow.Year + "-" + timeNow.Month + "-" + timeNow.Day;
            string strTime = timeNow.Hour + ":" + timeNow.Minute + ":" + timeNow.Second;
            templateBuilder.Replace("[=DATE]", strDate);
            templateBuilder.Replace("[=TIME]", strTime);

            //strReturn = strReturn.Replace("[=BR]", "\n");
            //strReturn = strReturn.Replace("[=TAB]", "\t");

            if (table.Columns != null)
            {
                Template.processLoop(templateBuilder, table.Columns);
            }
            return templateBuilder.ToString();
        }

        /// <summary>
        /// Process the loop
        /// </summary>
        /// <param name="template">Template string</param>
        /// <param name="columns">Collection of ColumnInfo</param>
        private static void processLoop(StringBuilder templateBuilder, IList<ColumnInfo> columns)
        {
            Regex r;
            Match m;

            r = new Regex(@"(\[#Loop])([\S\s]*?)(\[#Loop\/\])", RegexOptions.IgnoreCase);
            for (m = r.Match(templateBuilder.ToString()); m.Success; m = m.NextMatch())
            {
                string typeString = string.Empty;
                string separator = string.Empty;
                string content = m.Groups[2].ToString();
                templateBuilder.Replace(m.Groups[0].ToString(),
                    Template.processMatch(typeString, separator, content, columns)
                    );
            }

            r = new Regex(@"(\[#Loop Type=(Primary|Normal|All)])([\S\s]*?)(\[#Loop\/\])", RegexOptions.IgnoreCase);
            for (m = r.Match(templateBuilder.ToString()); m.Success; m = m.NextMatch())
            {
                string typeString = m.Groups[2].ToString();
                string separator = string.Empty;
                string content = m.Groups[3].ToString();
                templateBuilder.Replace(m.Groups[0].ToString(),
                    Template.processMatch(typeString, separator, content, columns)
                    );
            }

            r = new Regex(@"(\[#Loop Separator=([\S\s]*?)\])([\S\s]*?)(\[#Loop\/\])", RegexOptions.IgnoreCase);
            for (m = r.Match(templateBuilder.ToString()); m.Success; m = m.NextMatch())
            {
                string typeString = string.Empty;
                string separator = m.Groups[2].ToString();
                string content = m.Groups[3].ToString();
                templateBuilder.Replace(m.Groups[0].ToString(),
                    Template.processMatch(typeString, separator, content, columns)
                    );
            }


            r = new Regex(@"(\[#Loop Type=(Primary|Normal|All) Separator=([\S\s]*?)\])([\S\s]*?)(\[#Loop\/\])", RegexOptions.IgnoreCase);
            for (m = r.Match(templateBuilder.ToString()); m.Success; m = m.NextMatch())
            {
                string typeString = m.Groups[2].ToString();
                string separator = m.Groups[3].ToString();
                string content = m.Groups[4].ToString();
                templateBuilder.Replace(m.Groups[0].ToString(), 
                    Template.processMatch(typeString, separator, content, columns)
                    );
            }
        }

        /// <summary>
        /// Process each matched sub string
        /// </summary>
        /// <param name="loopType"></param>
        /// <param name="separator"></param>
        /// <param name="content"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        private static string processMatch(string typeString, string separator, string content, IList<ColumnInfo> columns)
        {
            //Get the loop type
            LoopType type;
            if (typeString.Trim().Equals("Primary"))
            {
                type = LoopType.Primary;
            }
            else if (typeString.Trim().Equals("Normal"))
            {
                type = LoopType.Normal;
            }
            else
            {
                type = LoopType.All;
            }

            StringBuilder loopBuilder = new StringBuilder();
            foreach (ColumnInfo column in columns)
            {
                StringBuilder rowBuilder = new StringBuilder(content);
                if (type == LoopType.Primary && column.IsPrimary ||
                    type == LoopType.Normal && !column.IsPrimary ||
                    type == LoopType.All)
                {
                    string typeCast = string.Empty;
                    if (column.TypeName.EndsWith("Enum", StringComparison.CurrentCultureIgnoreCase))
                    {
                        typeCast = string.Format("({0})Convert.ToInt32(", column.TypeName);
                    }
                    else if (column.DataType == DataType.String)
                    {
                        typeCast = "DbProvider.GetString(decode, ";
                    }
                    else
                    {
                        typeCast = string.Concat("Convert.To", column.TypeName, "(");
                    }
                    rowBuilder.Replace("[=TypeCast]", typeCast);


                    rowBuilder.Replace("[=Type]", column.TypeName);
                    rowBuilder.Replace("[=Size]", column.FieldSize.ToString());
                    rowBuilder.Replace("[=Value]", Template.getDefaultValue(column.DataType, column.DefaultValue));
                    rowBuilder.Replace("[=Field]", Template.FormatName(column.Name, true));
                    rowBuilder.Replace("[=Property]", Template.FormatName(column.Name, false));

                    //Append the separator
                    if (loopBuilder.Length > 0 && separator.Length > 0)
                    {
                        loopBuilder.Append(separator);
                    }
                    loopBuilder.Append(rowBuilder);
                }
            }

            return loopBuilder.ToString();
        }

        /// <summary>
        /// Get the formatted name
        /// </summary>
        /// <param name="name">Original Name</param>
        /// <param name="camelCasing">If returns camel cased name</param>
        /// <returns></returns>
        internal static string FormatName(string name, bool camelCasing)
        {
            StringBuilder sbName = new StringBuilder();

            for(int i =0;i<name.Length ;i++)
			{
                if (name[i] != '_')
                {
                    //First char without camel casing mode; Char after splitter; Char after a lower cased char
                    if (sbName.Length == 0)
                    {
                        sbName.Append(camelCasing ? char.ToLower(name[i]) : char.ToUpper(name[i]));
                    }
                    else if ((name[i - 1].Equals('_') && i > 0) ||
                        (char.IsLower(name, i - 1) && char.IsUpper(name, i) && i > 0))
                    {
                        sbName.Append(char.ToUpper(name[i]));
                    }
                    else
                    {
                        sbName.Append(char.ToLower(name[i]));
                    }
                }
			}

            return sbName.ToString();
        }

        /// <summary>
        /// Get the default value according to the date type
        /// </summary>
        /// <param name="type">Data type</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns></returns>
        private static string getDefaultValue(DataType type, string defaultValue)
        {
            string returnValue = string.Empty;
            bool hasDefaultValue = !string.IsNullOrEmpty(defaultValue);

            switch(type)
            {
                case DataType.Boolean:
                    returnValue = hasDefaultValue ? defaultValue : "false";
                    break;
                case DataType.Byte:
                    returnValue = hasDefaultValue ? defaultValue : "0";
                    break;
                case DataType.Char:
                    returnValue = hasDefaultValue ? defaultValue : "0";
                    break;
                case DataType.DateTime:
                    returnValue = hasDefaultValue ? defaultValue : "DateTime.MinValue";
                    break;
                case DataType.Decimal:
                    returnValue = hasDefaultValue ? defaultValue : "0";
                    break;
                case DataType.Double:
                    returnValue = hasDefaultValue ? defaultValue : "0";
                    break;
                case DataType.Int16:
                    returnValue = hasDefaultValue ? defaultValue : "0";
                    break;
                case DataType.Int32:
                    returnValue = hasDefaultValue ? defaultValue : "0";
                    break;
                case DataType.Int64:
                    returnValue = hasDefaultValue ? defaultValue : "0";
                    break;
                case DataType.Single:
                    returnValue = hasDefaultValue ? defaultValue : "0";
                    break;
                case DataType.String:
                    returnValue = hasDefaultValue ? defaultValue : "string.Empty";
                    break;
                default:
                    returnValue = hasDefaultValue ? defaultValue : string.Empty;
                    break;
            }

            return returnValue;
        }
    }//end of class
}
