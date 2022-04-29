using NetCore3WithReact.DAL.Entities.Sales;
using NetCore3WithReact.DAL.Repositories;
using System;

namespace NetCore3WithReact.DAL.DataProviders
{
    public interface IDataManager: IDisposable
    {
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<Vendor> VendorRepository { get; }
        int Save();
    }
}
