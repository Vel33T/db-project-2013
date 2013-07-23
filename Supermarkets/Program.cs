using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Data.Entity;
using Supermarkets.Data;
using Supermarkets.Model;
using System.IO;

namespace Supermarkets
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sqlserver = new SupermarketsEntities(true))
            {
                sqlserver.Database.CreateIfNotExists();

                MySqlTransfer.Transfer(sqlserver);

                // ExcelTransfer.Transfer(sqlserver);
            }

            using (var sqlserver = new SupermarketsEntities())
            {
                // Directory.CreateDirectory("output");

                // Supermarkets.Task3.XML.GenerateXMLFile.GenerateAggregateReport(sqlserver, "aggregate-report.xml");
            }

        }

    }

}
