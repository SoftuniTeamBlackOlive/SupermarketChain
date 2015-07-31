namespace SupermarketChain.Data.SqlServer
{
    using System;
    using System.Linq;

    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using SupermarketChain.Model;
    using SupermarketChain.Data.SqlServer.Migrations;

    public class SupermarketChainDbContext : DbContext, ISupermarketChainDbContext
    {
        public SupermarketChainDbContext()
            : base("SupermarketChainSqlServerConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SupermarketChainDbContext, Configuration>());
        }

        public virtual IDbSet<Product> Products { get; set; }

        public virtual IDbSet<Expense> Expenses { get; set; }

        public virtual IDbSet<Vendor> Vendors { get; set; }

        public virtual IDbSet<Measure> Measures { get; set; }

        public virtual IDbSet<Supermarket> Supermarkets { get; set; }

        public virtual IDbSet<Sale> Sales { get; set; }
        
        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}