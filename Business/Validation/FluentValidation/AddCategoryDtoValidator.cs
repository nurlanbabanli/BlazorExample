using Entities.Dtos;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.FluentValidation
{
    public class AddCategoryDtoValidator:AbstractValidator<CategoryDto>
    {
        public AddCategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Validation error: Category name is empty");
        }

        protected override bool PreValidate(ValidationContext<CategoryDto> context, ValidationResult result)
        {
            if (context.InstanceToValidate==null)
            {
                result.Errors.Add(new ValidationFailure("", "Validation error: Category is null"));
                return false;
            }
            return true;
        }
    }
}
