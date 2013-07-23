using System;
using System.Data.OleDb;
using System.IO;
using System.Linq;

namespace Supermarkets.Task6.TotalReport
{
    class ExcelWriter
    {
        public static void GenerateExcel(IQueryable<VendorsTotalReport> items, string pathToExcel)
        {
            if (File.Exists(pathToExcel))
            {
                File.Delete(pathToExcel);
            }

            var connectionString = new OleDbConnectionStringBuilder();
            connectionString.Provider = "Microsoft.ACE.OLEDB.12.0";
            connectionString.Add("Extended Properties", "Excel 12.0");

            InsertIntoExcelDoc(connectionString, items);
        }

        public static void InsertIntoExcelDoc(OleDbConnectionStringBuilder connectionString, IQueryable<VendorsTotalReport> items)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString.ToString()))
            {
                connection.Open();

                OleDbCommand command = new OleDbCommand(@"CREATE TABLE [Sheet33] 
([Vendor Name] string, [Incomes] decimal, [Expenses] decimal, [Taxes] decimal, [Financial Result] decimal)", dbCon);
                command.ExecuteScalar();

                foreach (var vendor in items)
                {
                    OleDbCommand insertQuery = new OleDbCommand(
                        @"INSERT INTO [Sheet33$] ([Vendor], [Incomes], [Expenses], [Taxes], [Financial Result]) 
                VALUES (@vendorName, @incomes, @expenses, @taxes, @result)", connection);

                    insertQuery.Parameters.AddWithValue("@vendorName", vendor.VendorName);
                    insertQuery.Parameters.AddWithValue("@income", vendor.Incomes);
                    insertQuery.Parameters.AddWithValue("@expenses", vendor.Expences);
                    insertQuery.Parameters.AddWithValue("@taxes", vendor.Taxes);
                    insertQuery.Parameters.AddWithValue("@result", vendor.Result);

                    insertQuery.ExecuteNonQuery();
                }
            }
        }
    }
}
