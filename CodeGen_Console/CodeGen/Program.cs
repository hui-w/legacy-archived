using System;
using System.Collections.Generic;

namespace QLike.CodeGen
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Error arguments");
                Console.Read();
                return;
            }

            try
            {
                //Load schema
                string schemaFile = args[0];
                IList<TableInfo> tables = Generator.LoadTablesFromSchema(schemaFile);
                Console.WriteLine("{0} tables loaded:", tables.Count);
                foreach (TableInfo table in tables)
                {
                    Console.WriteLine(string.Format(" - {0}", table.Name));
                    foreach (ColumnInfo column in table.Columns)
                    {
                        Console.WriteLine(string.Format("       {0} ({1})", column.Name, column.TypeName));
                    }
                }
                Console.WriteLine();

                //Genarate
                string templateName = args[1];
                Console.WriteLine(string.Format(" - Template: {0}", templateName));

                string filePrefix = templateName.Substring(0, templateName.IndexOf("."));
                string templateContent = TextFile.ReadFromFile(templateName);

                foreach (TableInfo table in tables)
                {
                    Console.WriteLine(string.Format("       Table: {0}", table.Name));
                    string targetFile = string.Format(args[2], Template.FormatName(table.Name, false));

                    string source = Generator.GenerateCode(table, templateContent);
                    TextFile.WriteToFile(targetFile, source);
                    Console.WriteLine("           Generated {0}", targetFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
