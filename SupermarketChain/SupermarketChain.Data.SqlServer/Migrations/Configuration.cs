namespace SupermarketChain.Data.SqlServer.Migrations
{
    using System;
    using System.Linq;

    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    using SupermarketChain.Model;

    public sealed class Configuration : DbMigrationsConfiguration<SupermarketChainDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "SupermarketChain.Data.SqlServer.SupermarketChainDbContext";
        }

        protected override void Seed(SupermarketChainDbContext context)
        {
            if (!context.Vendors.Any())
            {
                this.SeedInitialDataForTesting(context);
            }
        }

        private void SeedInitialDataForTesting(SupermarketChainDbContext context)
        {
            // TODO: Add more seed data for testing!
            context
                .Vendors
                .Add(new Vendor
                {
                    Name = "Kamenitza"
                });

            context.SaveChanges();
        }
    }
}
