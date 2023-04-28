using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public interface IEfEntityRepository<TData> where TData : class, IEntity, new()
    {
        Task<List<TData>> GetAllAsync(Expression<Func<TData, bool>> expression = null);
        Task<TData> GetAsync(Expression<Func<TData, bool>> expression);
        Task<TData> AddAsync(TData entity);
        Task DeleteAsync(TData entity);
        Task<TData> UpdateAsync(TData entity);
    }
}
