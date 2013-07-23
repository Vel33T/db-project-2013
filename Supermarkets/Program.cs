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
            using (var sqlserver = new SupermarketsEntities(true))
            {
                Database.SetInitializer<SupermarketsEntities>(new DropCreateDatabaseAlways<SupermarketsEntities>());
                sqlserver.Database.Initialize(true);

                MySqlTransfer.Transfer(sqlserver);

                // ExcelTransfer.Transfer(sqlserver);
            }

            using (var sqlserver = new SupermarketsEntities())
            {
                Directory.CreateDirectory("output");

                Supermarkets.Task3.XML.GenerateXMLFile.GenerateAggregateReport(sqlserver, @"output\aggregate-report.xml");
            }

        }


    }

}
