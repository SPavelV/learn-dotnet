using System;
using System.Collections.Generic;
using System.IO;

namespace DotNetFilesAndDirectories
{
    class Program
    {
        static void Main(string[] args)
        {
            // Files info
            string fileName = $"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales{Path.DirectorySeparatorChar}sales.json";
            FileInfo info = new FileInfo(fileName);
            System.Console.WriteLine($"Full Name: {info.FullName}{Environment.NewLine}Directory: {info.Directory}{Environment.NewLine}Extensions: {info.Extension}{Environment.NewLine}Create Date: {info.CreationTime}"); // And many more

            // Determining file name extensions
            System.Console.WriteLine(Path.GetExtension("sales.json"));

            // Combining paths
            System.Console.WriteLine(Path.Combine("stores", "201"));

            // Use current symbol seporator for path
            System.Console.WriteLine($"stores{Path.DirectorySeparatorChar}201");

            // Get special folder lie MyDocuments for Windows, macOS or Linux
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            System.Console.WriteLine($"Documents path: {docPath}");

            string currentDirectory = Directory.GetCurrentDirectory();
            // Get current diryctory:
            System.Console.WriteLine($"Current dirctory: {currentDirectory}");
        }

        static void DirectoryDemo(){
            var salesFiles = FindFiles("stores");

            foreach(var file in salesFiles)
            {
                System.Console.WriteLine(file);
            }
        }

        static IEnumerable<string> FindFiles(string folderName){
            List<string> salesFiles = new List<string>();

            var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

            foreach(var file in foundFiles){
                // The file name will contain the full path,
                // so only check the end of it

                if(file.EndsWith("sales.json"))
                {
                    salesFiles.Add(file);
                }
            }
            return salesFiles;
        }
    }
}
