namespace SupermarketChain.Data.SqlServer
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using SupermarketChain.Model;

    public interface ISupermarketChainDbContext
    {
        IDbSet<Product> Products { get; set; }

        IDbSet<Vendor> Vendors { get; set; }

        IDbSet<Measure> Measures { get; set; }

        void SaveChanges();

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
