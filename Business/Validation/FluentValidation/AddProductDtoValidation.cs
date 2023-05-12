using Entities.Dtos;
using FluentValidation;
using FluentValidation.Results;

namespace Business.Validation.FluentValidation
{
    public class AddProductDtoValidation : AbstractValidator<ProductDto>
    {
        public AddProductDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Validation error: Category name is empty");
        }

        protected override bool PreValidate(ValidationContext<ProductDto> context, ValidationResult result)
        {
            if (context.InstanceToValidate==null)
            {
                result.Errors.Add(new ValidationFailure("", "Validation error: Product is null"));
                return false;
            }

            return true;
        }
    }
}
