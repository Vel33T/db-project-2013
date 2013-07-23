using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Supermarkets.Data;
using Supermarkets.Model;
using System.IO;

namespace Supermarkets
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetIn(new StringReader("N\nY\nN"));

            if (Ask("Run Task 1?"))
            {
                using (var sqlserver = new SupermarketsEntities(true))
                {
                    Database.SetInitializer<SupermarketsEntities>(new DropCreateDatabaseAlways<SupermarketsEntities>());
                    sqlserver.Database.Initialize(true);

                    if (Ask("Transfer from MySQL?"))
                    {
                        MySqlTransfer.Transfer(sqlserver);
                        Console.WriteLine("Transfer complete");
                    }

                    if (Ask("Transfer from Excel?"))
                    {
                        ExcelTransfer.Transfer(sqlserver);
                        Console.WriteLine("Transfer complete");
                    }

                }
            }

            if (Ask("Run Task 2?"))
            {
                var file = @"output\report-aggregate-sales.pdf";

                using (var sqlserver = new SupermarketsEntities())
                {
                    Directory.CreateDirectory("output");
                    Supermarkets.Task2.PDF.PdfSalesReport.GeneratePdfReport(sqlserver, file);
                }

                Console.WriteLine("PDF aggregate sales report in " + file);

            }

            if (Ask("Run Task 3?"))
            {
                var file = @"output\report-vendor-sales.xml";

                using (var sqlserver = new SupermarketsEntities())
                {
                    Directory.CreateDirectory("output");
                    Supermarkets.Task3.XML.GenerateXMLFile.GenerateAggregateReport(sqlserver, file);
                }

                Console.WriteLine("XML vendor sales report in " + file);

            }

        }

        static bool Ask(string what)
        {
            Console.WriteLine(what);
            Console.WriteLine("(Y for yes, any other string for no)");
            var response = Console.ReadLine();
            if (response == null)
                return false;
            if (response.ToUpper().Trim() == "Y")
                return true;
            return false;
        }


    }

}
