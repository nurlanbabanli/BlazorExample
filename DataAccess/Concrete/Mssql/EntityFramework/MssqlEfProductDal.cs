using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.Mssql.EntityFramework
{
    public class MssqlEfProductDal:EfEntityRepositoryBase<Product, ExampleDbContext>, IProductDal
    {
    }
}
