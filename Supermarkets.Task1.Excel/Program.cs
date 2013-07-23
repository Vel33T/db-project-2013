using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;
using Supermarkets.Model;
using System.Data;

namespace Supermarkets.Task1.Excel
{
    public static class ExcelReader
    {
        static void Main(string[] args)
        {
            string zipPath = @"..\..\Sales-Reports.zip";
            string extractPath = @"..\..\extract";

            ToString(GetReportsData(zipPath, extractPath));
        }

        static public IEnumerable<string[]> GetReportsData(string zipPath, string extractPath)
        {
            if (Directory.Exists(extractPath))
            {
                Directory.Delete(extractPath, true);
            }

            using (ZipFile archive = ZipFile.Read(zipPath))
            {
                archive.ExtractAll(extractPath, ExtractExistingFileAction.OverwriteSilently);
            }

            IEnumerable<string> excelFiles = DirSearch(extractPath);

            var connectionString = new OleDbConnectionStringBuilder();
            connectionString.Provider = "Microsoft.ACE.OLEDB.12.0";
            connectionString.Add("Extended Properties", "Excel 12.0");

            var sales = new List<String[]>();
            foreach (var file in excelFiles)
            {
                connectionString.DataSource = file;

                // Get the date from the name of the file.
                string pattern = "-Sales-Report-";
                string salesDate = file.Substring(file.LastIndexOf(pattern) + pattern.Length);
                salesDate = salesDate.Substring(0, salesDate.IndexOf(".xls"));

                sales.AddRange(ReadOneExcelFile(connectionString, salesDate));
            }
            Directory.Delete(extractPath, true);
            return sales;
        }

        /// <summary>
        /// Recursivly get all  .xls files
        /// </summary>
        /// <param name="sDir">base directory</param>
        /// <returns>all .xls files</returns>
        public static IEnumerable<string> DirSearch(string sDir)
        {
            List<string> allFiles = new List<string>();
            try
            {
                foreach (string directory in Directory.GetDirectories(sDir))
                {
                    foreach (string file in Directory.GetFiles(directory))
                    {
                        if (file.EndsWith(".xls"))
                        {
                            allFiles.Add(file);
                        }
                    }
                    DirSearch(directory);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return allFiles;
        }

        /// <summary>
        /// Reads the contents of an excel report file into a list of sales arrays;
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="salesDate"></param>
        /// <returns>rows: Supermarket, Data, ProductID, Quantity, Unit Price</returns>
        public static List<string[]> ReadOneExcelFile(OleDbConnectionStringBuilder connectionString, string salesDate)
        {
            List<String[]> data = new List<String[]>();
            using (OleDbConnection connection = new OleDbConnection(connectionString.ToString()))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from [Sales$]", connection);
                using (OleDbDataReader oneRow = command.ExecuteReader())
                {
                    oneRow.Read(); // get Supermarket name
                    string supermarket = oneRow[0].ToString();
                    oneRow.Read();  // Skip row - ProductID, Quantity, Unit Price

                    while (oneRow.Read())
                    {
                        string productID = oneRow[0].ToString();
                        string quantity = oneRow[1].ToString();
                        string unitPrice = oneRow[2].ToString();
                        int result;  // only to check if the line containd a sale
                        if (int.TryParse(productID, out result))
                        // If not the Last rows
                        {
                            string[] line = { supermarket, salesDate, productID, quantity, unitPrice };
                            data.Add(line);
                        }
                    }
                }
            }
            return data;
        }

        /// <summary>
        ///  Print rows - 
        ///  Supermarket, Data, ProductID, Quantity, Unit Price
        /// </summary>
        /// <param name="sales"></param>
        public static void ToString(IEnumerable<string[]> sales)
        {
            //Console.WriteLine("Supermarket, Data, ProductID, Quantity, Unit Price");
            foreach (var sale in sales)
            {
                Console.WriteLine(string.Join(", ", sale));
            }
        }
    }
}
