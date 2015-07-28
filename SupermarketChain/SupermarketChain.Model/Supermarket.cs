namespace SupermarketChain.Model
{
    using System.Collections.Generic;

    public class Supermarket
    {
        private ICollection<Sale> sales;

        public Supermarket()
        {
            this.sales = new HashSet<Sale>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Sale> Sales
        {
            get { return this.sales; }
            set { this.sales = value; }
        }
    }
}
