namespace SupermarketChain.Data.SqlServer
{
    using SupermarketChain.Data.SqlServer.Repositories;
    using SupermarketChain.Model;

    public interface ISupermarketChainData
    {
        IRepository<Product> Products { get; }

        IRepository<Vendor> Vendors { get; }

        IRepository<Measure> Measures { get; }
    }
}
