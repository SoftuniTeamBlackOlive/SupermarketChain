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
            string[] vendors = { "Kamenitza", "Nestle Sofia Corp.", "Zagorka Corp.", "Targovishte Bottling Company Ltd." };

            foreach (var vendor in vendors)
            {
                context
                    .Vendors
                    .Add(new Vendor
                    {
                        Name = vendor
                    });                
            }

            string[] measures = { "liters", "pieces", "kg" };

            foreach (var measure in measures)
            {
                context
                    .Measures
                    .Add(new Measure
                    {
                        Name = measure
                    });
            }

            context.SaveChanges();

            Product[] products =
                {
                    new Product
                    {
                        VendorId = 3,
                        Name = "Beer “Zagorka”",
                        MeasureId = 1,
                        Price = 0.86m
                    },
                    new Product
                    {
                        VendorId = 4,
                        Name = "Vodka “Targovishte”",
                        MeasureId = 1,
                        Price = 7.56m                            
                    },
                    new Product
                    {
                        VendorId = 3,
                        Name = "Beer “Beck’s”",
                        MeasureId = 1,
                        Price = 1.03m                            
                    },
                    new Product
                    {
                        VendorId = 4,
                        Name = "Chocolate “Milka”",
                        MeasureId = 2,
                        Price = 2.80m                            
                    }
                };

            foreach (var product in products)
            {
                context
                    .Products
                    .Add(product);
            }

            context.SaveChanges();
        }
    }
}
