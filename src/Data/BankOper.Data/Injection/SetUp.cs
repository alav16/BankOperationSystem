using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOper.Data.DB;
using BankOper.Data.Repositories;
using BankOper.Data.Repositories.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankOper.Data.Injection
{
   

    public static class SetUp
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();

            return services;
        }
    }
}
