namespace SupermarketChain.Client.Console
{
    using System;
    using System.Linq;

    using SupermarketChain.Model;
    using SupermarketChain.Data.SqlServer;
    using SupermarketChain.Data.SqlServer.Repositories;

    class Program
    {
        static void Main(string[] args)
        {
            // Testing SupermarketChainDbContext
            var dbContext = new SupermarketChainDbContext();
            Console.WriteLine(dbContext.Vendors.FirstOrDefault(v => v.Name == "Kamenitza").Name);
            Console.WriteLine(dbContext.Vendors.FirstOrDefault(v => v.Name == "Kamenitza").Name);
            Console.WriteLine(dbContext.Vendors.Find(2).Name);
            
            // Testing repository
            var dbVendors = new Repository<Vendor>();
            dbVendors.Add(new Vendor { Name = "Zagorka" });
            dbVendors.SaveChanges();

            // Testing unit of work
            var data = new SupermarketChainData();
            Console.WriteLine(data.Vendors.All().FirstOrDefault(v => v.Name == "Zagorka").Name);
        }
    }
}