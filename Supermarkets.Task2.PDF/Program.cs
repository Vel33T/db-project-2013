using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarkets.Data;
using Supermarkets.Model;

namespace Supermarkets.Task2.PDF
{
    class Program
    {
        static void Main()
        {
            SupermarketsEntities context = new SupermarketsEntities();
            using (context)
            {
                context.Database.CreateIfNotExists();

                var aggregatedSales = from sale in context.Sales
                                      group sale by sale.DateSold into g
                                      select new { Date = g.Key, Sum = g.Sum(y => y.Quantity * y.UnitPrice) };

                foreach (var saleDate in aggregatedSales)
                {
                    Console.WriteLine("{0}, {1}", saleDate.Date, saleDate.Sum);
                }

            }
        }
    }
}
