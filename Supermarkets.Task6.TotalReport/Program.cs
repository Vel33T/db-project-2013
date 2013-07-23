using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
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