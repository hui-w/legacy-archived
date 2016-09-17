using System;
using System.Xml;
using System.Collections.Generic;

namespace QLike.CodeGen
{
    /// <summary>
    /// Class for generating code according to the DataBase schema
    /// </summary>
    public class Generator
    {
        /// <summary>
        /// Load the list of TableInfo from schema file
        /// </summary>
        /// <param name="schemaFile"></param>
        /// <returns></returns>
        public static IList<TableInfo> LoadTablesFromSchema(string schemaFile)
        {
            try
            {
                IList<TableInfo> tables = new List<TableInfo>();
                XmlDocument doc = new XmlDocument();
                doc.Load(schemaFile);
                XmlNodeList tableNodes = doc.GetElementsByTagName("table");
                foreach (XmlNode tableNode in tableNodes)
                {
                    XmlAttribute attName = tableNode.Attributes["name"];
                    XmlAttribute attNameSpace = tableNode.Attributes["namespace"];
                    XmlAttribute attProductName = tableNode.Attributes["productName"];
                    XmlAttribute attDbName = tableNode.Attributes["dbName"];
                    if (attName != null && attNameSpace != null && attProductName != null && attDbName != null)
                    {
                        TableInfo table = new TableInfo();
                        table.Name = attName.Value;
                        table.NameSpace = attNameSpace.Value;
                        table.ProductName = attProductName.Value;
                        table.DbName = attDbName.Value;
                        table.Columns = LoadColumnsFromNode(tableNode);
                        tables.Add(table);
                    }
                }

                return tables;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load the list of ColumnInfo from XmlNode
        /// </summary>
        /// <param name="tableNode"></param>
        /// <returns></returns>
        private static IList<ColumnInfo> LoadColumnsFromNode(XmlNode tableNode)
        {
            XmlNodeList commentNodes = tableNode.ChildNodes;
            IList<ColumnInfo> comments = new List<ColumnInfo>();
            foreach (XmlNode commentNode in commentNodes)
            {
                ColumnInfo column = new ColumnInfo();

                XmlAttribute attName = commentNode.Attributes["name"];
                XmlAttribute attDataType = commentNode.Attributes["dataType"];
                XmlAttribute attFieldSize = commentNode.Attributes["fieldSize"];
                XmlAttribute attDefaultValue = commentNode.Attributes["defaultValue"];
                XmlAttribute attIsPrimary = commentNode.Attributes["isPrimary"];

                if (attName != null && attDataType != null)
                {
                    column.Name = attName.Value;
                    try
                    {
                        column.DataType = (DataType)Enum.Parse(typeof(DataType), attDataType.Value);
                    }
                    catch
                    {
                        column.DataType = DataType.Other;
                    }
                    column.TypeName = attDataType.Value;
                    column.FieldSize = attFieldSize == null ? 0 : Convert.ToInt32(attFieldSize.Value);
                    column.DefaultValue = attDefaultValue == null ? string.Empty : attDefaultValue.Value;
                    column.IsPrimary = attIsPrimary == null ? false : Convert.ToBoolean(attIsPrimary.Value);
                }

                comments.Add(column);
            }
            return comments;
        }

        /// <summary>
        /// Generate C# code
        /// </summary>
        /// <param name="table"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public static string GenerateCode(TableInfo table, string template)
        {
            return Template.GenerateCode(table, template);
        }
    }//end of class
}
