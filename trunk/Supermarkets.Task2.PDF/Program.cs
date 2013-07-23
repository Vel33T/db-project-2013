﻿using System;
using System.Linq;
using Supermarkets.Data;
using Supermarkets.Model;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace Supermarkets.Task2.PDF
{
    class Program
    {
        static void Main()
        {
            SupermarketsEntities context = new SupermarketsEntities();
            using (context)
            {
                //context.Database.CreateIfNotExists();

                PdfPTable table = new PdfPTable(5);
                AddTableHeader(table);

                var aggregatedSales = from sale in context.Sales
                                      group sale by sale.DateSold into g
                                      select new { Date = g.Key, Sum = g.Sum(y => y.Quantity * y.UnitPrice) };

                foreach (var saleDate in aggregatedSales)
                {
                    AddDayHeader(table, saleDate.Date);

                    var salesByDay = from sale in context.Sales
                                     where sale.DateSold == saleDate.Date
                                     select new
                                     {
                                         Product = sale.Product.ProductName,
                                         Quantity = sale.Quantity,
                                         Mea = sale.Product.Measure.Name,
                                         UnitPrice = sale.UnitPrice,
                                         Location = sale.Supermarket.Name,
                                         Sum = sale.Quantity * sale.UnitPrice
                                     };

                    foreach (var sale in salesByDay)
                    {
                        table.AddCell(sale.Product);
                        table.AddCell(sale.Quantity.ToString() + " " + sale.Mea);
                        table.AddCell(sale.UnitPrice.ToString("C2"));
                        table.AddCell(sale.Location);
                        table.AddCell(sale.Sum.ToString("C2"));
                    }

                    AddDayTotal(table, saleDate.Date, saleDate.Sum);
                }

                SaveToFile(table);

            }
        }

        private static void SaveToFile(PdfPTable table)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document();
                //PdfWriter.GetInstance(doc, ms);
                PdfWriter writer = PdfWriter.GetInstance(doc, 
                    new FileStream(Path.Combine(@"D:\", "SalesReport" + DateTime.Today.ToString("yyyyMMdd") + ".pdf"),
                        FileMode.Create));
                //doc.Add(table);
                doc.Open();
                doc.Add(table);
                doc.Close();
            }
        }

        private static void AddTableHeader(PdfPTable table)
        {
            PdfPCell header = new PdfPCell(new Phrase("Aggregated Sales Report", new Font(Font.FontFamily.UNDEFINED, 12, 1)));
            header.PaddingTop = 10;
            header.PaddingBottom = 10;
            header.Colspan = table.NumberOfColumns;
            header.HorizontalAlignment = 1;
            table.AddCell(header);
        }

        private static void AddDayHeader(PdfPTable table, DateTime date)
        {
            PdfPCell dayHeader = new PdfPCell(new Phrase("Date: " + date.ToString("dd-MMM-yyyy")));
            dayHeader.Colspan = table.NumberOfColumns;
            dayHeader.BackgroundColor = BaseColor.LIGHT_GRAY;

            table.AddCell(dayHeader);

            string[] columnHeaders = new string[] {
                "Product",
                "Quantity",
                "UnitPrice",
                "Location",
                "Sum",
            };

            foreach (var header in columnHeaders)
            {
                PdfPCell columnHeader = new PdfPCell(new Phrase(header, new Font(Font.FontFamily.UNDEFINED, 12, 1)));
                columnHeader.BackgroundColor = BaseColor.LIGHT_GRAY;
                table.AddCell(columnHeader);
            }
        }

        private static void AddDayTotal(PdfPTable table, DateTime date, decimal sum)
        {
            PdfPCell dayFooter = new PdfPCell(new Phrase("Total sum for " + date.ToString("dd-MMM-yyyy") + ":"));
            dayFooter.Colspan = table.NumberOfColumns - 1;
            dayFooter.BackgroundColor = BaseColor.LIGHT_GRAY;

            table.AddCell(dayFooter);

            table.AddCell(new PdfPCell(new Phrase(sum.ToString("C2"), new Font(Font.FontFamily.UNDEFINED, 12, 1))));
        }
    }
}
