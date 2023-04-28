using Core.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Core.CrossCuttingConcerns.Logging.Models;
using Serilog.Sinks.MSSqlServer;
using Serilog;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    public class MssqlLogger:LoggerServiceBase
    {
        public MssqlLogger()
        {
            var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
            var logConfiguration = configuration.GetSection("SeriLogConfigurations:MsSqlConfiguration").Get<MssqlConfiguration>()
                ?? throw new Exception("Serilog configuration error");

            var sinkOptions=new MSSqlServerSinkOptions { TableName ="Logs", AutoCreateSqlTable = true };
            var seriLogConfiguration=new LoggerConfiguration().WriteTo.MSSqlServer(connectionString: logConfiguration.ConnectionString,sinkOptions:sinkOptions).CreateLogger();

            Logger=seriLogConfiguration;
        }
    }
}
