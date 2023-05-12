using Core.Results.Abstract;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        public Task<IDataResult<ProductDto>> AddAsync(ProductDto productDto);
        public Task<IDataResult<ProductDto>> UpdateAsync(ProductDto productDto);
        public Task<IResult> DeleteAsync(int productId);
        public Task<IDataResult<ProductDto>> GetAsync(int productId);
        public Task<IDataResult<IEnumerable<ProductDto>>> GetAllAsync();
    }
}
