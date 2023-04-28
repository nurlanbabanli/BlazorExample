using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Mssql.EntityFramework
{
    public class MssqlEfCategoryDal : EfEntityRepositoryBase<Category, ExampleDbContext>, ICategoryDal
    {
    }
}
