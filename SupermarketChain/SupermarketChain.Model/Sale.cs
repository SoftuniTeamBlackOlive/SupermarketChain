﻿namespace SupermarketChain.Model
{
    using System;

    public class Sale
    {
        public int Id { get; set; }

        public virtual string ProductName { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public virtual string SupermarketName { get; set; }

        public decimal Sum { get; set; }

        public DateTime Date { get; set; }

        public virtual string MeasureName { get; set; }
    }
}
