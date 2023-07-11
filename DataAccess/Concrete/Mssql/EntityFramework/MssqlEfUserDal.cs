using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Tools.LocalLogger;
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
        public async Task<bool> DeleteUserAsync(User user)
        {
            if (user==null) return false;
            using (var context=new ExampleDbContext())
            {
                using (var transaction=context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Entry<User>(user).State=EntityState.Deleted;

                        var userClaims = context.UserOperationClaims.Where(x => x.UserId==user.Id);
                        context.UserOperationClaims.RemoveRange(userClaims);

                        var saveChangesResult = await context.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return true;
                    }
                    catch (Exception exeption)
                    {
                        transaction.Rollback();
                        LocalLogHandler.Log(exeption.Message, LogLevel.Error);
                        return false;
                    }
                }
            }
        }

        public async Task<List<OperationClaim>> GetClaimsAsync(User user)
        {
            using (var context=new ExampleDbContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.Id
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name, };
                return await result.ToListAsync();
            }
        }
    }
}
