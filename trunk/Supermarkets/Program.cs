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


namespace Supermarkets
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sqlserver = new SupermarketsEntities())
            {
                sqlserver.Database.CreateIfNotExists();

                MySqlTransfer.Transfer(sqlserver);
                ExcelTransfer.Transfer(sqlserver);

                Directory.Create("output");

                Supermarkets.Task3.XML.GenerateXMLFile.GenerateAggregateReport(sqlserver, "aggregate-report.xml");
            }

        }

    }

}
