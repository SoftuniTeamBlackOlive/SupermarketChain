namespace SupermarketChain.Client.Console
{
    using System;

    using SupermarketChain.Data.SqlServer;
    using SupermarketChain.Model;

    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new SupermarketChainDbContext();

            dbContext
                .Vendors
                .Add(new Vendor
                {
                    Name = "Kamenitza"
                });

            dbContext.SaveChanges();
        }
    }
}