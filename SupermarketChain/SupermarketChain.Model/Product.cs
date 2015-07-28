namespace SupermarketChain.Model
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public virtual Vendor Vendor { get; set; }

        public int VendorId { get; set; }

        public virtual Measure Measure { get; set; }

        public int MeasureId { get; set; }
    }
}
