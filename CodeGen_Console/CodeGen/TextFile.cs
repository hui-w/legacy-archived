using System;
using System.IO;
using System.Text;

namespace QLike.CodeGen
{
    /// <summary>
    /// Operation to text file
    /// </summary>
    public class TextFile
    {
        /// <summary>
        /// ReadTextFile 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ReadFromFile(string fileName)
        {
            try
            {
                StringBuilder sbFile = new StringBuilder();
                using (StreamReader reader = File.OpenText(fileName))
                {
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        sbFile.Append(line);
                        sbFile.Append("\r\n");
                        line = reader.ReadLine();
                    }
                    reader.Close();
                }
                return sbFile.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Write text into file
        /// </summary>
        /// <param name="filename">Name of file</param>
        /// <param name="content">Content to write</param>
        /// <returns>The wrote file</returns>
        public static string WriteToFile(string fileName, string content)
        {
            return TextFile.WriteToFile(fileName, content, true);
        }

        /// <summary>
        /// Write text into file
        /// </summary>
        /// <param name="filename">Name of file</param>
        /// <param name="content">Content to write</param>
        /// <param name="overWrite">Over write existing file</param>
        /// <returns>The wrote file</returns>
        public static string WriteToFile(string fileName, string content, bool overWrite)
        {
            //Check if the file exists
            if (File.Exists(fileName))
            {
                if (!overWrite)
                {
                    int dotPosition = fileName.LastIndexOf(".");
                    string name, ext;
                    if (dotPosition > 0)
                    {
                        name = fileName.Substring(0, dotPosition);
                        ext = fileName.Substring(dotPosition);
                    }
                    else
                    {
                        name = fileName;
                        ext = string.Empty;
                    }

                    string newFileName;
                    int index = 0;
                    do
                    {
                        index++;
                        newFileName = string.Concat(name, "_", index.ToString(), ext);

                    } while (File.Exists(newFileName));

                    fileName = newFileName;
                }
                else
                {
                    TextFile.ReadOnlyToNormal(fileName);
                }

                //Start writting
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.Write(content);
                    writer.Close();
                }
            }
            else
            {
                //Create file and write
                using (StreamWriter writer = File.CreateText(fileName))
                {
                    writer.Write(content);
                    writer.Close();
                }
            }

            return fileName;
        }

        /// <summary>
        /// Change the file's attribute from readonly to normal
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        public static void ReadOnlyToNormal(string fileName)
        {
            if ((File.GetAttributes(fileName) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                File.SetAttributes(fileName, FileAttributes.Normal);
            }
        }
    }//end of class
}
