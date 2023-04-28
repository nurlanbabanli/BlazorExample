using Core.Results.Abstract;
using Core.Results.Concrete;
using DataAccess.Abstract;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Rules
{
    internal static class CategoryRules
    {
        internal static async Task<IResult> IsCategoryExists(ICategoryDal categoryDal, CategoryDto categoryDto)
        {
            var checkResult=await categoryDal.GetAsync(c=>c.Name==categoryDto.Name);
            if (checkResult==null) return new SuccessResult();

            return new ErrorResult("Category name is existing");
        }
    }
}
