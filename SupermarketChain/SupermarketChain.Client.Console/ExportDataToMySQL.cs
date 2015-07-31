

using SupermarketChain.Data.SqlServer;

namespace SupermarketChain.Client.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MySql.Data.MySqlClient;
    class ExportDataToMySQL
    {
        public static void Export()
        {
            string MyConString = "SERVER=localhost;DATABASE=supermarketchain;UID=root;";
            MySqlConnection connection = new MySqlConnection(MyConString);
            var context= new SupermarketChainDbContext();
            var vendors = context.Vendors.Select(v => v.Name);
            AddVendorsToMySQL(vendors, connection);
            AddProductsToMySQL(context, connection);
            AddExpensesToMySQL(context, connection);
            var incomes = context.Sales.GroupBy(s => s.Product).Select(p => new
            {
                productName=p.Select(s=>s.Product.Name),
                sum=p.Sum(s=>s.Sum)
            });
            foreach (var income in incomes)
            {
                try
                {
                    string cmdPull = @"SELECT id FROM supermarketchain.products where name='" + income.productName.FirstOrDefault() + "'";
                    MySqlCommand pull = new MySqlCommand(cmdPull, connection);

                    connection.Open();
                    MySqlDataReader reader = pull.ExecuteReader();
                    int? productId = null;
                    while (reader.Read())
                    {
                        productId = int.Parse(reader.GetString(0));
                    }
                    connection.Close();
                    string cmdPush = "INSERT INTO incomes(sum,product_id)VALUES ('" + income.sum + "','" + productId + "')";
                    MySqlCommand cmd = new MySqlCommand(cmdPush, connection);
                    connection.Open();
                    int result = cmd.ExecuteNonQuery();
                    connection.Close();

                    //lblError.Text = "Data Saved";
                }
                catch (Exception ex)
                {
                    //Console.Write("not entered");
                    Console.WriteLine(ex.Message);
                    //lblError.Text = ex.Message;
                }
            }

        }

        private static void AddExpensesToMySQL(SupermarketChainDbContext context, MySqlConnection connection)
        {
            var expenses = context.Expenses.Select(e => new
            {
                sum = e.Sum,
                date = e.Date,
                vendorName = e.Vendor.Name
            });
            foreach (var expense in expenses)
            {
                try
                {
                    string cmdPull = @"SELECT id FROM supermarketchain.vendors where name='" + expense.vendorName + "'";
                    MySqlCommand pull = new MySqlCommand(cmdPull, connection);

                    connection.Open();
                    MySqlDataReader reader = pull.ExecuteReader();
                    int? vendorId = null;
                    while (reader.Read())
                    {
                        vendorId = int.Parse(reader.GetString(0));
                    }
                    connection.Close();
                    string cmdPush = "INSERT INTO expenses(sum,period,vendor_id)VALUES ('" + expense.sum + "','" +
                                     expense.date.ToString("yyyy-MM-dd HH:mm:ss") + "','" + vendorId + "')";
                    MySqlCommand cmd = new MySqlCommand(cmdPush, connection);
                    connection.Open();
                    int result = cmd.ExecuteNonQuery();
                    connection.Close();

                    //lblError.Text = "Data Saved";
                }
                catch (Exception ex)
                {
                    //Console.Write("not entered");
                    Console.WriteLine(ex.Message);
                    //lblError.Text = ex.Message;
                }
            }
        }

        private static void AddProductsToMySQL(SupermarketChainDbContext context, MySqlConnection connection)
        {
            var products = context.Products.Select(p => new
            {
                name = p.Name,
                vendorName = p.Vendor.Name
            });
            foreach (var product in products)
            {
                try
                {
                    string cmdPull = @"SELECT id FROM supermarketchain.vendors where name='" + product.vendorName + "'";
                    MySqlCommand pull = new MySqlCommand(cmdPull, connection);

                    connection.Open();
                    MySqlDataReader reader = pull.ExecuteReader();
                    int? vendorId = null;
                    while (reader.Read())
                    {
                        vendorId = int.Parse(reader.GetString(0));
                    }
                    connection.Close();
                    string cmdPush = "INSERT INTO products(name,vendor_id)VALUES ('" + product.name + "','" + vendorId + "')";
                    MySqlCommand cmd = new MySqlCommand(cmdPush, connection);
                    connection.Open();
                    int result = cmd.ExecuteNonQuery();
                    connection.Close();

                    //lblError.Text = "Data Saved";
                }
                catch (Exception ex)
                {
                    //Console.Write("not entered");
                    Console.WriteLine(ex.Message);
                    //lblError.Text = ex.Message;
                }
            }
        }

        private static void AddVendorsToMySQL(IQueryable<string> vendors, MySqlConnection connection)
        {
            foreach (var vendor in vendors)
            {
                try
                {
                    string cmdText = "INSERT INTO vendors(name)VALUES ('" + vendor + "')";
                    MySqlCommand cmd = new MySqlCommand(cmdText, connection);

                    connection.Open();

                    int result = cmd.ExecuteNonQuery();
                    connection.Close();

                    //lblError.Text = "Data Saved";
                }
                catch (Exception ex)
                {
                    //Console.Write("not entered");
                    Console.WriteLine(ex.Message);
                    //lblError.Text = ex.Message;
                }
            }
        }
    }
}
