using AutoMapper;
using Business.Abstract;
using Business.Rules;
using Business.Validation.FluentValidation;
using Core.Aspect.Autofac.Exception;
using Core.Aspect.Autofac.Logging;
using Core.Aspect.Autofac.Validation;
using Core.Business;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Results;
using Core.Results.Abstract;
using Core.Results.Concrete;
using Core.Tools.Security.Hashing;
using Core.Tools.Security.JWT;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IMapper _autoMapper;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, IMapper autoMapper, ITokenHelper tokenHelper)
        {
            _userService=userService;
            _autoMapper=autoMapper;
            _tokenHelper=tokenHelper;
        }

        [ExceptionLogAspect(typeof(MssqlLogger), skipValueLog: true, Priority = 3)]
        [LogAspects(typeof(MssqlLogger), skipValueLog: true, Priority = 4)]
        [ValidationAspect(typeof(UserLoginDtoValidator), Priority = 2)]
        public async Task<IDataResult<UserLoginResponseDto>> LoginAsync(UserLoginDto userLoginDto)
        {
            var userDataResult=await _userService.GetByEmailAsync(userLoginDto.Email);
            var filterResult = DataResultHandler.FilterDataResults<User, UserLoginResponseDto>(userDataResult);
            if (filterResult!=null) return filterResult;


            //if(FilterDataResults<User, UserLoginResponseDto>(userDataResult)!=null) return 
            //return FilterDataResults<User, UserLoginResponseDto>(userDataResult);
            //if (userDataResult == null) return new ErrorDataResult<UserLoginResponseDto>(null,"Get user error", internalServerError: true);
            //if(userDataResult.InternalServerError) return new ErrorDataResult<UserLoginResponseDto>(null,userDataResult.Message, internalServerError: true);
            //if (!userDataResult.IsSuccess) return new ErrorDataResult<UserLoginResponseDto>(null, userDataResult.Message);

            var passwordHashSalt = new PasswordHashSaltDto
            {
                PasswordSalt=userDataResult.Data.PasswordSalt,
                PasswordHash=userDataResult.Data.PasswordHash
            };
            var isPasswordVerified=HashingHelper.VerifyPasswordHash(userLoginDto.Password, passwordHashSalt);
            if (!isPasswordVerified) return new ErrorDataResult<UserLoginResponseDto>(null, "Password is not correct");

            var operationClaimsDataResult = await _userService.GetClaimsAsync(userDataResult.Data);
            if (operationClaimsDataResult == null) return new ErrorDataResult<UserLoginResponseDto>(null, "Get operation claims error", internalServerError: true);
            if(!operationClaimsDataResult.IsSuccess) return new ErrorDataResult<UserLoginResponseDto>(null, operationClaimsDataResult.Message);

            var accessTokenDataResult = _tokenHelper.CreateToken(userDataResult.Data, operationClaimsDataResult.Data);
            if (accessTokenDataResult == null) return new ErrorDataResult<UserLoginResponseDto>(null, "Get token error", internalServerError: true);


            var userLoginResponseDto=new UserLoginResponseDto
            {
                Token=,
                TokenExpiration=
            }

            return null;
        }

        [ExceptionLogAspect(typeof(MssqlLogger), skipValueLog: true, Priority = 3)]
        [LogAspects(typeof(MssqlLogger), skipValueLog: true, Priority = 4)]
        [ValidationAspect(typeof(RegisterUserDtoValidator),Priority =2)]
        public async Task<IDataResult<UserDto>> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            var ruleCheckResult = BusinessRules.RunRules(await AuthRules.IsUserEmailExitsAsync(_userService, userRegisterDto),
                AuthRules.CheckPasswordLength(userRegisterDto));
            if (!ruleCheckResult.IsSuccess) return new ErrorDataResult<UserDto>(null, ruleCheckResult.Message);


            var paswordHashSalt = HashingHelper.CreatePasswordHash(userRegisterDto.Password);

            var user = _autoMapper.Map<UserRegisterDto, User>(userRegisterDto);
            user.PasswordSalt = paswordHashSalt.PasswordSalt;
            user.PasswordHash=paswordHashSalt.PasswordHash;

            var addUserResult=await _userService.AddAsync(user);
            if (addUserResult==null) return new ErrorDataResult<UserDto>(null,internalServerError:true);
            if(!addUserResult.IsSuccess) return new ErrorDataResult<UserDto>(null, addUserResult.Message);

            var userDtoResult = _autoMapper.Map<User, UserDto>(addUserResult.Data);

            return new SuccessDataResult<UserDto>(userDtoResult);
        }
    }
}
