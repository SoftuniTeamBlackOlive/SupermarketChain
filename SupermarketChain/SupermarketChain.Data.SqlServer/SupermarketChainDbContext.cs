namespace SupermarketChain.Data.SqlServer
{
    using System;
    using System.Linq;

    using System.Data.Entity;

    using SupermarketChain.Model;

    public class SupermarketChainDbContext : DbContext
    {
        // Your context has been configured to use a 'SupermarketChainDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'SupermarketChain.Data.SqlServer.SupermarketChainDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'SupermarketChainDbContext' 
        // connection string in the application configuration file.
        public SupermarketChainDbContext()
            : base("name=SupermarketChainDbContext")
        {
        }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Vendor> Vendors { get; set; }

        public IDbSet<Measure> Measures { get; set; }
    }
}