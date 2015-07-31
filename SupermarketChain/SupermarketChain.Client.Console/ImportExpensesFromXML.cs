using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using SupermarketChain.Data.SqlServer;
using SupermarketChain.Model;

namespace SupermarketChain.Client.Console
{
    public class ImportExpensesFromXML
    {
        public static void Import()
        {
            var context= new SupermarketChainDbContext();
            var doc = XDocument.Load("../../Vendor-Expenses.xml");
            var vendors = doc.XPathSelectElements("/expenses-by-month/vendor");
            foreach (var vendor in vendors)
            {
                var vendorName = vendor.Attribute("name").Value;
                //System.Console.WriteLine(vendorName);
                var expenses = vendor.XPathSelectElements("expenses");
                foreach (var expense in expenses)
                {
                    try
                    {
                        decimal amount = decimal.Parse(expense.Value);
                        //System.Console.WriteLine(amount);
                        var date = DateTime.Parse(expense.Attribute("month").Value);
                        //System.Console.WriteLine(date);
                        var expenseItem = new Expense()
                        {
                            Vendor = context.Vendors.First(v => v.Name == vendorName),
                            Sum = amount,
                            Date = date
                        };
                        if (context.Expenses.FirstOrDefault(e => e.Vendor.Name == vendorName && e.Date == date) != null)
                        {
                            string msg = "Expense statement for " + vendorName + ", for the period " + date.ToString("MMMM") + " " + date.Year +
                                         " already exists";
                            throw new ArgumentException(msg);
                        }
                        context.Expenses.AddOrUpdate(expenseItem);
                        context.SaveChanges();
                        System.Console.WriteLine("Added expese statement in the database for {0} for {1} {2}", vendorName, date.ToString("MMMM"), date.Year);

                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine("Error:{0}",ex.Message);
                    }


                }
            }
        }
    }
}
