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


            using (var sqlserver = new SupermarketsEntities())
            {
                if (Ask("Run Task 2?"))
                {
                    var file = @"output\report-aggregate-sales.pdf";


                    Directory.CreateDirectory("output");
                    Supermarkets.Task2.PDF.PdfSalesReport.GeneratePdfReport(sqlserver, file);
                }

                Console.WriteLine("PDF aggregate sales report in " + file);


                if (Ask("Run Task 3?"))
                {
                    var file = @"output\report-vendor-sales.xml";


                    Directory.CreateDirectory("output");
                    Supermarkets.Task3.XML.GenerateXMLFile.GenerateAggregateReport(sqlserver, file);

                    Console.WriteLine("XML vendor sales report in " + file);

                }

                if (Ask("Run task 4?"))
                {
                    var file = @"output\task-4.xml";

                    Supermarkets.Task4.MongoDB.InsertIntoMongoDB.GenerateMongoDBProductReport();

                    Console.WriteLine("Task 4 report in " + file);

                }

                if (Ask("Run task 5?"))
                {
                    var file = @"output\vendor-expenses-report.xml";

                    Supermarkets.Task5.VendorExpencesXML.GenerateVendorExpenses.WriteVendorExpensesReport(file);

                    Console.WriteLine("Task 5 report in " + file);

                }

                if (Ask("Run Task 6?"))
                {
                    var file = @"output\final-report.xlsx";

                    // Supermarkets.Task6.TotalReport.ExcelWriter.GenerateExcel(sqlserver, file);

                    Console.WriteLine("Final report in " + file);

                }

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
