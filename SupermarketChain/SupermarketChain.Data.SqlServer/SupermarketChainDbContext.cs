namespace SupermarketChain.Data.SqlServer
{
    using System;
    using System.Linq;

    using System.Data.Entity;

    using SupermarketChain.Model;
    using SupermarketChain.Data.SqlServer.Migrations;

    public class SupermarketChainDbContext : DbContext
    {
        public SupermarketChainDbContext()
            : base("SupermarketChainSqlServerConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SupermarketChainDbContext, Configuration>());
        }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Vendor> Vendors { get; set; }

        public IDbSet<Measure> Measures { get; set; }
    }
}