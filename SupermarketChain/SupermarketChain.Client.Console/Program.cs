namespace SupermarketChain.Client.Console
{
    using System;
    using System.Linq;

    using SupermarketChain.Data.SqlServer;
    using SupermarketChain.Model;

    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new SupermarketChainDbContext();

            Console.WriteLine(dbContext.Vendors.FirstOrDefault(v => v.Name == "Kamenitza").Name);
        }
    }
}