using Business.Abstract;
using Core.Results.Abstract;
using Core.Results.Concrete;
using Entities.Dtos;

namespace Business.Rules
{
    internal static class AuthRules
    {
        internal static async Task<IResult> IsUserEmailExitsAsync(IUserService userService, UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto==null) return new ErrorResult("User data is empty");

            var userCheckDataResult=await userService.GetByEmailAsync(userRegisterDto.Email);
            if (userCheckDataResult==null) throw new Exception("Internal server error");

            if (userCheckDataResult.IsSuccess) return new ErrorResult("User email is used");
            return new SuccessResult();
        }

        internal static IResult CheckPasswordLength(UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto==null) return new ErrorResult("User data is empty");

            if (userRegisterDto.Password.Length<3) return new ErrorResult("Password length must be more than 3 charcter");

            return new SuccessResult();
        }
    }
}
