namespace SupermarketChain.Client.Console
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text.RegularExpressions;

    using SupermarketChain.Data.SqlServer;
    using System.Xml.Linq;

    using MongoDB.Bson.Serialization.Serializers;

    using SupermarketChain.Model;

    public abstract class XMLReportSalesByVendor
    {
        public static void GenerateReport(DateTime startDate, DateTime endDate, SupermarketChainData data)
        {
            var context = new SupermarketChainDbContext();

            var salesByVendors = 
                context.Sales.Where(s => s.Date >= startDate && s.Date < endDate)
                .GroupBy(s => new { Vendor = s.Product.Vendor.Name, Date = s.Date})
                .Select(s => new
                                 {
                                     Vendor = s.Key.Vendor,
                                     Date = s.Key.Date,
                                     TotalSum = s.Sum(a => a.Sum)
                                 });

            var groupedByDaysAndVendorsSales =
                salesByVendors.GroupBy(s => s.Vendor).ToList();

            var report = new XDocument();
            var salesReportXml = new XElement("sales");



            foreach (var sale in groupedByDaysAndVendorsSales)
            {
                var vendorXML = new XElement("sale", new XAttribute("vendor", sale.Key));
                
                foreach (var row in sale)
                {
                    var newRowXML = new XElement("summary", new XAttribute("date", row.Date.ToString("dd-MMM-yyyy")), new XAttribute("total-sum", row.TotalSum));
                    vendorXML.Add(newRowXML);
                }

                salesReportXml.Add(vendorXML);
            }

            report.Add(salesReportXml);

            Console.WriteLine(report); 
            
            report.Save("../../salesReportByVendors.xml");            

        }
    }
}
