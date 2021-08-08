using FluentPOS.Modules.Catalog.Extensions;
using FluentPOS.Modules.Identity.Extensions;
using FluentPOS.Modules.People.Extensions;
using FluentPOS.Modules.Sales.Extensions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Bootstrapper
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDistributedMemoryCache()
                .AddSerialization(_config)
                .AddSharedInfrastructure(_config)
                .AddIdentityModule(_config)
                .AddSharedApplication(_config)
                .AddCatalogModule(_config)
                .AddPeopleModule(_config)
                .AddSalesModule(_config);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSharedInfrastructure();
        }
    }
}