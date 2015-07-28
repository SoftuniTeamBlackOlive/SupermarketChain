namespace SupermarketChain.Data.SqlServer.Migrations
{
    using System;
    using System.Linq;

    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    using SupermarketChain.Model;
    using System.Globalization;

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

            Supermarket[] supermarkets =
                {
                    new Supermarket
                        {
                            Name = "Supermarket “Kaspichan – Center”"
                        },
                    new Supermarket
                        {
                            Name = "Supermarket “Bourgas – Plaza”"
                        },
                    new Supermarket
                        {
                            Name = "Supermarket “Bay Ivan” – Zmeyovo"
                        },
                    new Supermarket
                        {
                            Name = "Supermarket “Plovdiv – Stolipinovo”"
                        },
                };

            foreach (var supermarket in supermarkets)
            {
                context
                    .Supermarkets
                    .Add(supermarket);
            }

            Sale[] sales =
                {
                    new Sale
                        {
                            ProductName = "Beer “Beck’s”",
                            Date = DateTime.ParseExact("22-Jul-2014", "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                            Quantity = 18,
                            UnitPrice = 1.2m,
                            Sum = 21.60m,
                            SupermarketName = "Supermarket “Kaspichan – Center”" 
                        },
                    new Sale
                        {
                            ProductName = "Beer “Zagorka”",
                            Date = DateTime.ParseExact("22-Jul-2014", "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                            Quantity = 90,
                            UnitPrice = 0.92m,
                            Sum = 82.80m,
                            SupermarketName = "Supermarket “Kaspichan – Center”" 
                        },
                    new Sale
                        {
                            ProductName = "Chocolate “Milka”",
                            Date = DateTime.ParseExact("22-Jul-2014", "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                            Quantity = 14,
                            UnitPrice = 2.9m,
                            Sum = 40.60m,
                            SupermarketName = "Supermarket “Kaspichan – Center”" 
                        },
                    new Sale
                        {
                            ProductName = "Vodka “Targovishte”",
                            Date = DateTime.ParseExact("22-Jul-2014", "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                            Quantity = 12,
                            UnitPrice = 7.7m,
                            Sum = 92.40m,
                            SupermarketName = "Supermarket “Plovdiv – Stolipinovo”" 
                        },
                    new Sale
                        {
                            ProductName = "Beer “Beck’s”",
                            Date = DateTime.ParseExact("22-Jul-2014", "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                            Quantity = 60,
                            UnitPrice = 1.05m,
                            Sum = 63m,
                            SupermarketName = "Supermarket “Plovdiv – Stolipinovo”" 
                        },
                    new Sale
                        {
                            ProductName = "Beer “Zagorka”",
                            Date = DateTime.ParseExact("22-Jul-2014", "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                            Quantity = 230,
                            UnitPrice = 0.88m,
                            Sum = 202.40m,
                            SupermarketName = "Supermarket “Plovdiv – Stolipinovo”" 
                        },
                    new Sale
                        {
                            ProductName = "Beer “Zagorka”",
                            Date = DateTime.ParseExact("20-Jul-2014", "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                            Quantity = 37,
                            UnitPrice = 1m,
                            Sum = 37m,
                            SupermarketName = "Supermarket “Bourgas – Plaza”" 
                        },
                    new Sale
                        {
                            ProductName = "Vodka “Targovishte”",
                            Date = DateTime.ParseExact("20-Jul-2014", "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                            Quantity = 14,
                            UnitPrice = 8.5m,
                            Sum = 119m,
                            SupermarketName = "Supermarket “Bourgas – Plaza”" 
                        },
                };

            foreach (var sale in sales)
            {
                context
                    .Sales
                    .Add(sale);
            }

            context.SaveChanges();
        }
    }
}
