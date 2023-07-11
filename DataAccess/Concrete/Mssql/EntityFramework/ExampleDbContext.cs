using Core.Entities.Concrete;
using Core.IoC;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Mssql.EntityFramework
{
    public class ExampleDbContext : DbContext
    {
        public ExampleDbContext(DbContextOptions<ExampleDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public ExampleDbContext()
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //        var configurationTest = ServiceTool.ServiceProvider.GetService<IConfiguration>();
            //        var connectionString = configurationTest.GetSection("ConnectionStrings:DefaultConnection").Get<string>()
            //?? throw new Exception("DB configuration error");
            //var configuration = new ConfigurationBuilder().AddJsonFile("dBSettings.json").Build();

            var configuration = new ConfigurationBuilder().AddJsonFile("dBSettings.json").Build();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

            //optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExampleDbContext).Assembly);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
