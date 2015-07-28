namespace SupermarketChain.Data.SqlServer
{
    using System;
    using System.Collections.Generic;

    using SupermarketChain.Data.SqlServer.Repositories;
    using SupermarketChain.Model;

    public class SupermarketChainData : ISupermarketChainData
    {
        private ISupermarketChainDbContext context;
        private IDictionary<Type, object> repositories;

        public SupermarketChainData()
            : this(new SupermarketChainDbContext())
        {
        }

        public SupermarketChainData(ISupermarketChainDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Product> Products
        {
            get 
            {
                return this.GetRepository<Product>();
            }
        }

        public IRepository<Vendor> Vendors
        {
            get 
            {
                return this.GetRepository<Vendor>();
            }
        }

        public IRepository<Measure> Measures
        {
            get 
            {
                return this.GetRepository<Measure>();
            }
        }

        private IRepository<T> GetRepository<T>()
            where T : class
        {
            Type typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                Type typeOfRepository = typeof(Repository<T>);
                var newRepository = Activator.CreateInstance(typeOfRepository, this.context);
                this.repositories.Add(typeOfModel, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfModel];
        }
    }
}
