
using System.Globalization;

namespace SupermarketChain.Client.Console
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using SupermarketChain.Data.SqlServer;

    public class JsonReportsToMongoDB
    {
        public static void Export()
        {
            bool validDate;
            var startDate = EnterFirstDate();
            var endDate = EnterEndDate(startDate);
            var context = new SupermarketChainDbContext();
            var client = new MongoClient();
            var db = client.GetDatabase("SuperMarketChainReports");
            var collection = db.GetCollection<BsonDocument>("SalesByProductReports");
            var products = context.Sales.Where(s => s.Date <= endDate && s.Date >= startDate)
                .GroupBy(s => s.ProductName)
                .Select(
                    p => new
                    {
                        productName=p.Key,
                        productId=p.Select(s=>s.Product.Id),
                        vendorName=p.Select(s=>s.Product.Vendor.Name),
                        totalQuantity=p.Sum(s=>s.Quantity),
                        totalIncome=p.Sum(s=>s.Sum)
                    });
            foreach (var product in products)
            {
                var document = new BsonDocument
                    {
                        {"product-id", product.productId.First()},
                        {"product-name", product.productName},
                        {"vendor-name", product.vendorName.First()},
                        {"total-quantity-sold",product.totalQuantity },
                        {"total-incomes", product.totalIncome.ToString()}
                    };
                //Upload to the DB
                collection.InsertOneAsync(document).Wait();
                //Export Data
                SaveToJson(document, product.productId.First());
            }
        }

        private static DateTime EnterEndDate(DateTime startDate)
        {
            bool validDate;
            Console.WriteLine("Enter an end date after {0}  :", startDate.ToString("dd/MM/yyyy"));
            var endDateString = Console.ReadLine();
            validDate = ValidateDate(endDateString);
            var endDate = DateTime.MinValue;
            while (!validDate || startDate > endDate)
            {
                if (!validDate)
                {
                    Console.WriteLine("You have entered an unvalid date.Try again!Use this format -> dd/MM/yyyy ");
                    endDateString = Console.ReadLine();
                    validDate = ValidateDate(endDateString);
                }
                else
                {
                    endDate = DateTime.ParseExact(endDateString, "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo);
                    if (startDate > endDate)
                    {
                        Console.WriteLine("The date you entered was before the start date - please enter a new one after {0}",
                            startDate.ToString("dd/MM/yyyy"));
                        endDateString = Console.ReadLine();
                        validDate = ValidateDate(endDateString);
                    }
                }
            }
            return endDate;
        }

        private static DateTime EnterFirstDate()
        {
            Console.WriteLine("Configure the period from which to generate your reports.Use this format -> dd/MM/yyyy .");
            Console.WriteLine("Enter start date :");
            var startDateString = Console.ReadLine();
            bool validDate = ValidateDate(startDateString);
            while (!validDate)
            {
                Console.WriteLine("You have entered an unvalid date.Try again!Use this format -> dd/MM/yyyy ");
                startDateString = Console.ReadLine();
                validDate = ValidateDate(startDateString);
            }
            var startDate = DateTime.ParseExact(startDateString, "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo);
            return startDate;
        }

        private static void SaveToJson(BsonDocument document, int id)
        {
            var json = MongoDB.Bson.BsonExtensionMethods.ToJson(document);

            var path = "../../Json-Reports/" + id + ".json";
            File.WriteAllText(path, json);
        }
        public static bool ValidateDate(string Date)
        {
            try
            {
                DateTime.ParseExact(Date, "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo);
                //Console.WriteLine("Date Valid.");
            }
            catch
            {
                //Console.WriteLine("Date Invalid.");
                return false;
            }
            return true;
        }
    }
}
