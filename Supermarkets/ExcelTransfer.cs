using System;
using System.Linq;
using System.Collections.Generic;
using Supermarkets.Data;
using Supermarkets.Model;


namespace Supermarkets
{
    class ExcelTransfer
    {
        public static void Transfer(SupermarketsEntities sqlserver)
        {
            var data = Supermarkets.Task1.Excel.ExcelReader.GetReportsData("files\\reports.zip", "files\\temp");

            foreach (var superMarketSales in data.GroupBy(o => o[0]))
            {
                var supermarket = sqlserver.Supermarkets.Where(v => v.Name == superMarketSales.Key).FirstOrDefault();

                if (supermarket == null)
                {
                    supermarket = new Supermarket { Name = superMarketSales.Key };
                    sqlserver.Supermarkets.Add(supermarket);
                }
                foreach (var saleData in superMarketSales)
                {
                    var sale = new ProductSupermarketSale
                    {
                        Supermarket = supermarket,
                        DateSold = DateTime.Parse(saleData[1]),
                        ProductId = int.Parse(saleData[2]),
                        UnitPrice = decimal.Parse(saleData[3]),
                        Quantity = decimal.Parse(saleData[4]),
                    };

                    sqlserver.Sales.Add(sale);
                }

            }

            sqlserver.SaveChanges();
        }
    }
}