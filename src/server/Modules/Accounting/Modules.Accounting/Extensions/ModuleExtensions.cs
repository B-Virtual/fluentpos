using FluentPOS.Modules.Accounting.Core.Extensions;
using FluentPOS.Modules.Accounting.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.Accounting.Extensions
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddAccountingModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAccountingCore()
                .AddSalesInfrastructure()
                .AddSalesValidation();
            return services;
        }

        public static IApplicationBuilder UseAccountingModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}