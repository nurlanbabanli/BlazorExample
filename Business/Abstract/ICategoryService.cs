using Core.Results.Abstract;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        public Task<IDataResult<CategoryDto>> AddAsync(CategoryDto categoryDto);
        public Task<IDataResult<CategoryDto>> UpdateAsync(CategoryDto categoryDto);
        public Task<IResult> DeleteAsync(int categoryId);
        public Task<IDataResult<CategoryDto>> GetAsync(int categoryId);
        public Task<IDataResult<IEnumerable<CategoryDto>>> GetAllAsync();
    }
}
