using AutoMapper;
using Business.Abstract;
using Business.Rules;
using Business.Validation.FluentValidation;
using Core.Aspect.Autofac.Exception;
using Core.Aspect.Autofac.Validation;
using Core.Business;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Results.Abstract;
using Core.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _autoMapper;
        public ProductManager(IMapper autoMapper, IProductDal productDal, ICategoryService categoryService)
        {
            _autoMapper=autoMapper;
            _productDal=productDal;
            _categoryService=categoryService;
        }

        [ValidationAspect(typeof(AddProductDtoValidation), Priority = 3)]
        [ExceptionLogAspect(typeof(MssqlLogger), Priority = 2)]
        //[SecuredOperation("user,superUser,admin", Priority = 1)]
        public async Task<IDataResult<ProductDto>> AddAsync(ProductDto productDto)
        {
            IResult ruleCheckResult=BusinessRules.RunRules(await ProductRules.IsProductExists(_productDal,productDto), 
                await ProductRules.IsCategoryExists(_categoryService,productDto));
            if (!ruleCheckResult.IsSuccess)
            {
                return new ErrorDataResult<ProductDto>(null, ruleCheckResult.Message);
            }

            var product = _autoMapper.Map<ProductDto, Product>(productDto);
            var addResult=await _productDal.AddAsync(product);
            if (addResult==null) return new ErrorDataResult<ProductDto>(null, "Product add error");

            var mappedAddResult = _autoMapper.Map<Product, ProductDto>(addResult);

            return new SuccessDataResult<ProductDto>(mappedAddResult);
        }

        [ValidationAspect(typeof(AddCategoryDtoValidation), Priority = 3)]
        [ExceptionLogAspect(typeof(MssqlLogger), Priority = 2)]
        public async Task<IResult> DeleteAsync(int productId)
        {
            var productToDelete = await _productDal.GetAsync(p => p.Id==productId);
            if (productToDelete==null) return new ErrorResult("Category not found");

            await _productDal.DeleteAsync(productToDelete);

            return new SuccessResult("Category deleted");
        }

        [ValidationAspect(typeof(AddCategoryDtoValidation), Priority = 3)]
        [ExceptionLogAspect(typeof(MssqlLogger), Priority = 2)]
        public async Task<IDataResult<IEnumerable<ProductDto>>> GetAllAsync()
        {
            Expression<Func<Product, Category>> includeExpression = p => p.Category;

            var products = await _productDal.GetAllAsync<Category>(includeExpression);
            if (products==null) return new ErrorDataResult<IEnumerable<ProductDto>>(null, "Product not found");

            var mappedProducts=_autoMapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);

            return new SuccessDataResult<IEnumerable<ProductDto>>(mappedProducts);
        }

        [ValidationAspect(typeof(AddCategoryDtoValidation), Priority = 3)]
        [ExceptionLogAspect(typeof(MssqlLogger), Priority = 2)]
        public async Task<IDataResult<ProductDto>> GetAsync(int productId)
        {
            Expression<Func<Product, Category>> includeExpression = p => p.Category;
            var product = await _productDal.GetAsync<Category>(includeExpression, c => c.Id==productId);

            //var product = await _productDal.GetAsync(c => c.Id==productId);
            if (product==null) return new ErrorDataResult<ProductDto>(null, "Product not found");

            var mappedProduct=_autoMapper.Map<Product, ProductDto>(product);

            return new SuccessDataResult<ProductDto>(mappedProduct);
        }


        [ValidationAspect(typeof(AddCategoryDtoValidation), Priority = 3)]
        [ExceptionLogAspect(typeof(MssqlLogger), Priority = 2)]
        //[SecuredOperation("user,superUser,admin", Priority = 1)]
        public async Task<IDataResult<ProductDto>> UpdateAsync(ProductDto productDto)
        {
            var productToUpdate=await _productDal.GetAsync(p=>p.Id==productDto.Id);
            if (productToUpdate==null) return new ErrorDataResult<ProductDto>(null);

            var mappedProductToUpdate = _autoMapper.Map<ProductDto, Product>(productDto);
            var updatedProduct=await _productDal.UpdateAsync(mappedProductToUpdate);
            if (updatedProduct==null) return new ErrorDataResult<ProductDto>(null, "Update product error");

            var mappedUpdatedProduct = _autoMapper.Map<Product, ProductDto>(updatedProduct);
            return new SuccessDataResult<ProductDto>(mappedUpdatedProduct);
        }
    }
}
