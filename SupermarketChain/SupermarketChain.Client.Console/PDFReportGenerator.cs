namespace SupermarketChain.Client.Console
{
    using System;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;

    using SupermarketChain.Data.SqlServer;
    using System.Text;

    public abstract class PDFReportGenerator
    {
        public static void GeneratePdfReport(DateTime startDate, DateTime endDate, SupermarketChainData data)
        {
            var htmlContent = new StringBuilder();

            htmlContent.Append(
                @"<!DOCTYPE html>
                <html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">
                <head>
                    <meta charset=""utf-8"" />
                    <title>Aggregated Sales Report</title>
                    <style>
                        table, td {
                            border: 1px solid black;
                        }
                        tr td:nth-child(1) {
                            width: 250px;
                        }

                        tr td:nth-child(2) {
                            width: 150px;
                            text-align: center;
                        }

                        tr td:nth-child(3) {
                            width: 150px;
                            text-align: center;
                        }

                        tr td:nth-child(4) {
                            width: 300px;
                        }

                        tr td:nth-child(5) {
                            width: 150px;
                            text-align: right;
                        }

                        .dateFormat {
                            background-color: lightgray;
                        }

                        tr.DailyReportHeader td {
                            background-color: grey;
                            text-align: center;
                            font-weight: bold;
                        }

                        tr.subtotal td:nth-child(1) {
                            text-align: right;            
                        }

                        tr.subtotal td:nth-child(2) {
                            font-weight: bold;
                            text-align: right;            
                        }

                        tfoot {
                            background-color: lightblue;
                        }

                        tfoot td:nth-child(1) {
                            text-align: right;
                        }

                        tfoot td:nth-child(2) {
                            text-align: right;
                            font-weight: bold;
                        }
                    </style>
                </head>
                <body>
                    <table>
                        <thead>
                            <tr><th colspan=""5"">Aggregated Sales Report</th></tr>
                        </thead>
                        <tbody>");

            var currentDate = startDate;
            decimal totalSum = 0;

            while (currentDate <= endDate)
            {
                decimal subTotalSum = 0;
                var currentDateSales = data.Sales.All().Where(s => s.Date == currentDate)
                        .Include(s => s.Product);
                
                if (currentDateSales.Any())
                {
                    htmlContent.AppendLine(@"<tr><td class=""dateFormat"" colspan=""5"">Date: " + currentDate.ToString("dd-MMM-yyyy") + "</td></tr>"
                                                           + @"<tr class=""DailyReportHeader"">
                                        <td>Product</td>
                                        <td>Quantity</td>
                                        <td>Unit Price</td>
                                        <td>Location</td>
                                        <td>Sum</td>
                                        </tr>");

                    foreach (var sale in currentDateSales)
                    {
                        htmlContent.AppendLine(@"<tr class=""ProductList"">")
                            .AppendLine("<td>" + sale.ProductName + "</td>")
                            .AppendLine("<td>" + sale.Quantity + " " + sale.MeasureName + "</td>")
                            .AppendLine("<td>" + sale.UnitPrice + "</td>")
                            .AppendLine("<td>" + sale.SupermarketName + "</td>")
                            .AppendLine("<td>" + sale.Sum + "</td>");

                        subTotalSum += sale.Sum;

                    }

                    htmlContent.AppendLine(@"<tr class=""subtotal"">
                        <td colspan=""4"">Total sum for " + currentDate.ToString("dd-MMM-yyyy") + @":</td>
                        <td colspan=""1"">" + subTotalSum + "</td></tr>");                    
                }
                
                currentDate = currentDate.AddDays(1);
                totalSum += subTotalSum;
            }

            htmlContent.AppendLine(@"</tbody>
                <tfoot>
                    <tr>
                    <td colspan=""4"">Grand total:</td>
                    <td colspan=""1"">" + totalSum + @"</td>
                    </tr>
                    </tfoot>
                </table>
            </body>
            </html>");

            var pdfBytes = new NReco.PdfGenerator.HtmlToPdfConverter().GeneratePdf(htmlContent.ToString());
            File.WriteAllBytes(Path.GetFullPath(@".\..\..\..\..\..\Sales Report from " + startDate.ToString("dd-MMM-yyyy") + " to " + endDate.ToString("dd-MMM-yyyy") + ".pdf"), pdfBytes);
            
            Console.WriteLine("PDF Report Successfully generated!");
        }
    }
}
