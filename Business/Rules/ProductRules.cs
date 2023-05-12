using Business.Abstract;
using Core.Results.Abstract;
using Core.Results.Concrete;
using DataAccess.Abstract;
using Entities.Dtos;

namespace Business.Rules
{
    internal static class ProductRules
    {
        internal static async Task<IResult> IsProductExists(IProductDal productDal, ProductDto productDto)
        {
            if (productDto==null) return new ErrorResult("Product is empty");

            var checkResult = await productDal.GetAsync(c => c.Name==productDto.Name);
            if (checkResult==null) return new SuccessResult();

            return new ErrorResult("Product name is exists");
        }

        internal static async Task<IResult> IsCategoryExists(ICategoryService categoryService, ProductDto productDto)
        {
            if (productDto==null) return new ErrorResult("Product is empty");

            var checkResult = await categoryService.GetAsync(productDto.CategoryId);
            if (checkResult==null) return new ErrorResult("Error");

            if (!checkResult.IsSuccess)
            {
                return new ErrorResult("Category not found");
            }

            return new SuccessResult();
        }
    }
}
