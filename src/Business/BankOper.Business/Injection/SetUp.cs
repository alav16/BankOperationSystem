using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOper.Business.Services;
using BankOper.Business.Services.Implemantation;
using BankOper.Data.Injection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankOper.Business.Injection
{
    public static class SetUp
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services, IConfiguration config)
        {
            services.AddDataLayer(config);

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IClientService, ClientService>();

            return services;
        }
    }
}
