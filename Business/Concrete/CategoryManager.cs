using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
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
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly IMapper _autoMapper;

        public CategoryManager(ICategoryDal categoryDal, IMapper autoMapper)
        {
            _categoryDal=categoryDal;
            _autoMapper=autoMapper;
        }


        [ValidationAspect(typeof(AddCategoryDtoValidation), Priority =3)]
        [ExceptionLogAspect(typeof(MssqlLogger),Priority =2)]
        //[SecuredOperation("user,superUser,admin", Priority = 1)]
        public async Task<IDataResult<CategoryDto>> AddAsync(CategoryDto categoryDto)
        {


            //throw new Exception("This is exception test");

            IResult rulecheckResult=BusinessRules.RunRules(await CategoryRules.IsCategoryExists(_categoryDal,categoryDto));
            rulecheckResult=null;
            if (!rulecheckResult.IsSuccess)
            {
                return new ErrorDataResult<CategoryDto>(null, rulecheckResult.Message);
            }


            var category=_autoMapper.Map<CategoryDto,Category>(categoryDto);
            var addResult =  await _categoryDal.AddAsync(category);
            if (addResult==null) return new ErrorDataResult<CategoryDto>(null, "Category add error");

            var mappedAddResult=_autoMapper.Map<Category,CategoryDto>(addResult);
            return new SuccessDataResult<CategoryDto>(mappedAddResult,"Category Added");
        }


        [ValidationAspect(typeof(AddCategoryDtoValidation), Priority = 3)]
        [ExceptionLogAspect(typeof(MssqlLogger), Priority = 2)]
        public async Task<IResult> DeleteAsync(int categoryId)
        {
            //await Task.Delay(5000);

            var categoryToDelete= await _categoryDal.GetAsync(c=>c.Id==categoryId);
            if (categoryToDelete==null) return new ErrorResult("Category not found");

            await _categoryDal.DeleteAsync(categoryToDelete);

            return new SuccessResult("Category deleted");
        }


        [ValidationAspect(typeof(AddCategoryDtoValidation), Priority = 3)]
        [ExceptionLogAspect(typeof(MssqlLogger), Priority = 2)]
        public async Task<IDataResult<CategoryDto>> GetAsync(int categoryId)
        {
            var category=await _categoryDal.GetAsync(c=>c.Id == categoryId);
            if (category==null) return new ErrorDataResult<CategoryDto>(null, "Category not found");

            var mappedCategory = _autoMapper.Map<Category, CategoryDto>(category);

            return new SuccessDataResult<CategoryDto>(mappedCategory);
        }


        [ValidationAspect(typeof(AddCategoryDtoValidation), Priority = 3)]
        [ExceptionLogAspect(typeof(MssqlLogger), Priority = 2)]
        public async Task<IDataResult<IEnumerable<CategoryDto>>> GetAllAsync()
        {
            //Thread.Sleep(5000);
            var categories =await _categoryDal.GetAllAsync();
            if (categories==null) return new ErrorDataResult<IEnumerable<CategoryDto>>(null, "Category not found");

            var mappedCategories = _autoMapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(categories);

            return new SuccessDataResult<IEnumerable<CategoryDto>>(mappedCategories);
        }


        [ValidationAspect(typeof(AddCategoryDtoValidation), Priority = 3)]
        [ExceptionLogAspect(typeof(MssqlLogger), Priority = 2)]
        //[SecuredOperation("user,superUser,admin", Priority = 1)]
        public async Task<IDataResult<CategoryDto>> UpdateAsync(CategoryDto categoryDto)
        {
            var categoryyToUpdate=await _categoryDal.GetAsync(c=>c.Id==categoryDto.Id);
            if (categoryyToUpdate==null) return new ErrorDataResult<CategoryDto>(null, "Category not found");

            categoryyToUpdate.Name = categoryDto.Name;

            var updatedCategory=await _categoryDal.UpdateAsync(categoryyToUpdate);
            if (updatedCategory==null) return new ErrorDataResult<CategoryDto>(null, "Update category error");

            var mappedUpdatetCategory=_autoMapper.Map<Category,CategoryDto>(updatedCategory);

            return new SuccessDataResult<CategoryDto>(mappedUpdatetCategory);
        }
    }
}
