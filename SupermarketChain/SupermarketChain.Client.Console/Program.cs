using System.Runtime.InteropServices;

namespace SupermarketChain.Client.Console
{
    using System;
    using System.Globalization;
    using System.Linq;

    using SupermarketChain.Model;
    using SupermarketChain.Data.SqlServer;
    using SupermarketChain.Data.SqlServer.Repositories;

    public class Program
    {
        public static void Main(string[] args)
        {
            
            //// Testing SupermarketChainDbContext
            //var dbContext = new SupermarketChainDbContext();
            //Console.WriteLine(dbContext.Vendors.First(v => v.Name == "Kamenitza").Name);
            //Console.WriteLine(dbContext.Vendors.Count());

            //// Testing repository
            //var dbVendors = new Repository<Vendor>();
            //dbVendors.Add(new Vendor { Name = "Zagorka" });
            //dbVendors.SaveChanges();

            //// Testing unit of work
             var data = new SupermarketChainData();
            //Console.WriteLine(data.Vendors.All().FirstOrDefault(v => v.Name == "Zagorka").Name);
            //Console.WriteLine(data.Supermarkets.All().FirstOrDefault(v => v.Name == "Supermarket “Bourgas – Plaza”").Name);

            //PDFReportGenerator.GeneratePdfReport(DateTime.ParseExact("20-Jul-2014", "dd-MMM-yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact("23-Jul-2014", "dd-MMM-yyyy", CultureInfo.InvariantCulture), data);
            //JsonReportsToMongoDB.Export();
             XMLReportSalesByVendor.GenerateReport(DateTime.ParseExact("20-Jul-2014", "dd-MMM-yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact("23-Jul-2014", "dd-MMM-yyyy", CultureInfo.InvariantCulture), data);
        }
    }
}