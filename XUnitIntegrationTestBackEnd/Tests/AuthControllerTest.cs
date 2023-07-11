using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitIntegrationTestBackEnd.Tests
{
    public class AuthControllerTest:IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private readonly IUserService _userService;
        public AuthControllerTest(WebApplicationFactory<Program> webApplicationFactory, IUserService userService)
        {
            _httpClient=webApplicationFactory.CreateClient();
            _userService=userService;
        }


        [Fact]
        public async Task TestUserRegisterAndLogin()
        {
            var userToRegister = new UserRegisterDto()
            {
                Email="test10001@gmail.com",
                FirstName="Test10001FirstName",
                LastName="Test10001LastName",
                Password="Test10001Password"
            };

            var jsonRegisterRequestModel = JsonConvert.SerializeObject(userToRegister);
            var postRegisterContent = new StringContent(jsonRegisterRequestModel, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/auth/registerUser", postRegisterContent);
            Assert.True(response.IsSuccessStatusCode);

            var content=await response.Content.ReadAsStringAsync();
            Assert.NotNull(content);

            var userRegisterResult=JsonConvert.DeserializeObject<UserDto>(content);
            Assert.NotNull(userRegisterResult);

            Assert.Equal(userRegisterResult.Email, userRegisterResult.FirstName);
            Assert.Equal(userRegisterResult.LastName, userRegisterResult.LastName);
            Assert.Equal(userRegisterResult.FirstName, userRegisterResult.FirstName);



            var userToLogin = new UserLoginDto()
            {
                Email=userToRegister.Email,
                Password=userToRegister.Password,
            };


            var jsonLoginRequestModel = JsonConvert.SerializeObject(userToLogin);
            var requestLoginContent = new StringContent(jsonLoginRequestModel, Encoding.UTF8, "application/json");
            var loginResponse = await _httpClient.PostAsync("/api/Product/getProductById", requestLoginContent);
            
            Assert.True(loginResponse.IsSuccessStatusCode);

            var loginResponseContent=await loginResponse.Content.ReadAsStringAsync();
            Assert.NotNull(loginResponseContent);

            var loginDataResult=JsonConvert.DeserializeObject<UserLoginResponseDto>(loginResponseContent);
            Assert.NotNull(loginDataResult);

            Assert.Equal(loginDataResult.Email, userToLogin.Email);
            Assert.Equal(loginDataResult.FirstName,userToRegister.FirstName);
            Assert.Equal(loginDataResult.LastName,userToRegister.LastName);


            await _userService.DeleteUserAsync(userRegisterResult.Id);
        }
    }
}
