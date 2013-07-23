using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Supermarkets.Task6.TotalReport
{
    static class Program
    {
        static void Main(string[] args)
        {
            SQLiteConnection dbCon = new SQLiteConnection(@"..\..\taxes.sqlite");

            using (dbCon)
            {
                dbCon.Open();

                //                var create = new SQLiteCommand(
                //    @"CREATE TABLE IF NOT EXISTS ProductsTax
                //(
                //    ProductName NVARCHAR(64),
                //    Tax INTEGER
                //)",
                //    dbCon);
                //                create.ExecuteNonQuery();

            }

        }

        static void ListAllProductsTax(this SQLiteConnection conn)
        {
            var command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM ProductsTax";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Console.WriteLine("{0}", reader.ShowReaderRow());
                }
            }
        }

        static void AddProductTax(this SQLiteConnection conn, string productName, int tax)
        {
            var command = conn.CreateCommand();
            command.CommandText = "INSERT INTO ProductsTax(ProductName, Tax) VALUES(@productName, @tax)";
            command.Parameters.Add(new SQLiteParameter("@ProductName", productName));
            command.Parameters.Add(new SQLiteParameter("@title", tax));
            command.ExecuteNonQuery();
        }
    }
}