using Autofac;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using XUnitTestBackEnd.AutofacContainerBuild;

namespace XUnitTestBackEnd.DataAccessTests
{


    public class UserDalTests
    {
        private readonly IContainer _container;

        public UserDalTests()
        {
            _container=AutofacContainerBuilder.GetAutofacContainer();
        }

        [Fact]
        public async Task TestGetUserClaims()
        {
            using (var scope=_container.BeginLifetimeScope())
            {
                var userDal = scope.Resolve<IUserDal>();

                var user = new User
                {
                    Id=1,
                    FirstName="TestName1",
                    LastName="TestLastName1",
                    Email="TestName1@gmail.com"
                };

                var claims = await userDal.GetClaimsAsync(user);
            }
        }

        [Fact]
        public async Task TestGetUserByEmail()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var userDal = scope.Resolve<IUserDal>();

                var user = await userDal.GetAsync(x => x.Email=="testEmail@gmail.com");
                //Assert.NotNull(user);
            }
        }

        [Fact]
        public async Task TestDeleteUser()
        {
            using (var scope=_container.BeginLifetimeScope())
            {
                var userDal=scope.Resolve<IUserDal>();

                //var userToDelete=await AddUser(userDal);

                var user = new User
                {
                    Email="testUser123@gmail.com",
                    FirstName ="TestUserFirstName123",
                    LastName="TestUserLastName123",
                    IsActive=false,
                    PasswordHash=new byte[] { },
                    PasswordSalt=new byte[] { }
                };

                var deleteResult=await userDal.DeleteUserAsync(user);
                Assert.True(deleteResult);
            }
        }

        public async Task<User> AddUser(IUserDal userDal)
        {
            var user = new User
            {
                Email="testUser123@gmail.com",
                FirstName ="TestUserFirstName123",
                LastName="TestUserLastName123",
                IsActive=false,
                PasswordHash=new byte[] { },
                PasswordSalt=new byte[] { }
            };

           

            return await userDal.AddAsync(user);
        }
    }
}
