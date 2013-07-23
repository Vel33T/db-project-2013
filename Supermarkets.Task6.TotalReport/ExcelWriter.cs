using System;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using Supermarkets.SQLite.EntityFramework;

namespace Supermarkets.Task6.TotalReport
{
    class ExcelWriter
    {
        public static void GenerateExcel(IQueryable<VendorFinancialResult> items, string pathToExcel)
        {
            if (File.Exists(pathToExcel))
            {
                File.Delete(pathToExcel);
            }

            var connectionString = new OleDbConnectionStringBuilder();
            connectionString.Provider = "Microsoft.ACE.OLEDB.12.0";
            connectionString.Add("Extended Properties", "Excel 12.0 xml");
            connectionString.DataSource = pathToExcel;

            InsertIntoExcelDoc(connectionString, items);
        }

        public static void InsertIntoExcelDoc(OleDbConnectionStringBuilder connectionString, IQueryable<VendorFinancialResult> items)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString.ToString()))
            {
                connection.Open();

                OleDbCommand command = new OleDbCommand(@"CREATE TABLE [Sheet1] 
([Vendor Name] string, [Incomes] decimal, [Expenses] decimal, [Taxes] decimal, [Financial Result] decimal)", connection);
                command.ExecuteScalar();

                foreach (var vendor in items)
                {
                    OleDbCommand insertQuery = new OleDbCommand(
                        @"INSERT INTO [Sheet1$] ([Vendor], [Incomes], [Expenses], [Taxes], [Financial Result]) 
                VALUES (@vendorName, @incomes, @expenses, @taxes, @result)", connection);

                    insertQuery.Parameters.AddWithValue("@vendorName", vendor.VendorName);
                    insertQuery.Parameters.AddWithValue("@income", vendor.Income);
                    insertQuery.Parameters.AddWithValue("@expenses", vendor.Expenses);
                    insertQuery.Parameters.AddWithValue("@taxes", vendor.Tax);
                    insertQuery.Parameters.AddWithValue("@result", vendor.FinancialResult);

                    insertQuery.ExecuteNonQuery();
                }
            }
        }
    }
}
