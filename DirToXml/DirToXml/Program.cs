using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace DirToXml
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirPath = string.Empty;

            //get dir from arguments
            if (args.Length > 0)
            {
                dirPath = args[0];
                if (!Directory.Exists(dirPath))
                {
                    dirPath = string.Empty;
                }
            }

            //get current directory
            if (string.IsNullOrEmpty(dirPath))
            {
                dirPath = AppDomain.CurrentDomain.BaseDirectory;
            }

            DirectoryInfo dir = new DirectoryInfo(dirPath);

            XElement itemRoot = new XElement("Files");
            foreach (FileInfo file in dir.GetFiles())
            {
                itemRoot.Add(
                    new XElement("File", 
                        new XAttribute("Name", file.Name),
                        new XAttribute("Length", file.Length),
                        new XAttribute("Extension", file.Extension),
                        new XAttribute("FullName", file.FullName),
                        new XAttribute("IsReadOnly", file.IsReadOnly),
                        new XAttribute("CreationTime", file.CreationTime),
                        new XAttribute("LastAccessTime", file.LastAccessTime),
                        new XAttribute("LastWriteTime", file.LastWriteTime)
                        )
                    );
            }

            //output to file
            string outPut = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "files.xml");
            XDocument xDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"), 
                new XProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"schema.xslt\""),  
                itemRoot);
            xDoc.Save(outPut);

            Console.WriteLine(string.Format("XML File serialized to {0}", outPut));
            Console.ReadKey();
        }
    }
}
