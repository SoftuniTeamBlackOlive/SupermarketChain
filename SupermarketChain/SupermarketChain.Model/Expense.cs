using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketChain.Model
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        public decimal Sum { get; set; }
        public DateTime Date { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
