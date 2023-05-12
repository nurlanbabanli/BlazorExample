using Core.DataAccess.EntityFramework;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEfEntityRepository<Product>
    {
        //Task<List<Product>> GetAllProductsWithCategory();
    }
}
