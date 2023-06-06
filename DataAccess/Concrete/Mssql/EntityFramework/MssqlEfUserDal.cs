using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Mssql.EntityFramework
{
    public class MssqlEfUserDal : EfEntityRepositoryBase<User, ExampleDbContext>, IUserDal
    {
        public async Task<List<OperationClaim>> GetClaimsAsync(User user)
        {
            using (var context=new ExampleDbContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperations
                             on operationClaim.Id equals userOperationClaim.Id
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name, };
                return await result.ToListAsync();
            }
        }
    }
}
