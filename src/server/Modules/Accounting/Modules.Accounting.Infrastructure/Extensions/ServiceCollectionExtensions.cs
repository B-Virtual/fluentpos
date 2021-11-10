using System.Reflection;
using FluentPOS.Modules.Accounting.Core.Abstractions;
using FluentPOS.Modules.Accounting.Infrastructure.Persistence;
using FluentPOS.Shared.Infrastructure.Extensions;
using FluentPOS.Shared.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.Accounting.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSalesInfrastructure(this IServiceCollection services)
        {
            services
                 .AddDatabaseContext<AccountingDbContext>()
                 .AddScoped<IAccountingDbContext>(provider => provider.GetService<AccountingDbContext>());
            services.AddExtendedAttributeDbContextsFromAssembly(typeof(AccountingDbContext), Assembly.GetAssembly(typeof(IAccountingDbContext)));
            return services;
        }

        public static IServiceCollection AddSalesValidation(this IServiceCollection services)
        {
            services.AddControllers().AddSalesValidation();
            return services;
        }
    }
}