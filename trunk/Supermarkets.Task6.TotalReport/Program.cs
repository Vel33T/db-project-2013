using System;
using System.Linq;
using Supermarkets.SQLite.EntityFramework;

namespace Supermarkets.Task6.TotalReport
{
    static class Program
    {
        static void Main(string[] args)
        {
            using (var sqlite = new SQLiteTaxesEntities())
            {
                ExcelWriter.GenerateExcel(sqlite.VendorFinancialResults, @"..\..\Products-Total-Report.xlsx");
            }
        }
    }
}