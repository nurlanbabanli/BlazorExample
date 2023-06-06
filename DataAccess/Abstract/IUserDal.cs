using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal:IEfEntityRepository<User>
    {
        Task<List<OperationClaim>> GetClaimsAsync(User user);
    }
}
