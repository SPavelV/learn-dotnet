using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace DotNetFilesAndDirectories
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var storesDirectory = Path.Combine(currentDirectory, "stores");

            var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
            Directory.CreateDirectory(salesTotalDir);

            var salesFiles = FindFiles(storesDirectory);

            var salesTotal = CalculateSalesTotal(salesFiles);

            File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");
        }
         static IEnumerable<string> FindFiles(string folderName){
            List<string> salesFiles = new List<string>();

            var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

            foreach(var file in foundFiles){
               var extension = Path.GetExtension(file);
                if(extension == ".json")
                {
                    salesFiles.Add(file);
                }
            }
            return salesFiles;
        }

        class SalesData
        {
            public double Total { get; set; }
        }

        static double CalculateSalesTotal(IEnumerable<string> salesFiles)
        {
            double salesTotal = 0;

            // Loop over each file path in salesFiles
            foreach(var file in salesFiles)
            {
                // Read the contents of the file
                string salesJson = File.ReadAllText(file);

                // Parse the contents as JSON
                SalesData data = JsonConvert.DeserializeObject<SalesData>(salesJson);

                // Add the amount found in the Total field to the salesTotal variable
                salesTotal += data.Total;
            }

            return salesTotal;
        }
        static void PathDemo(){
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

    }
}
