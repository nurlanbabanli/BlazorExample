using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.IoC;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Core.Tools.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Tools.Security.Encryption;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacBusinessModule());
});

// Add services to the container.

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters=new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer=tokenOptions.Issuer,
        ValidAudience=tokenOptions.Audience,
        ValidateIssuerSigningKey=true,
        IssuerSigningKey=SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
        ValidateLifetime = true,
        ClockSkew=TimeSpan.Zero,
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedOrigin", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDependencyResolvers(new ICoreModule[] { new CoreModule() });

var app = builder.Build();

app.UseCors("AllowedOrigin");
app.ConfigureCustonExceptionMiddleware();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
