using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using Services.MappingProfilies;

namespace Services
{
    public static class ApplicationServicesRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
           Services.AddAutoMapper(typeof(AssemblyRefernce).Assembly);
           Services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();

            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<Func<IProductService>>(Provider => () => Provider.GetRequiredService<IProductService>());

            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<Func<IBasketService>>(Provider => () => Provider.GetRequiredService<IBasketService>());

            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<Func<IAuthenticationService>>(Provider => () => Provider.GetRequiredService<IAuthenticationService>());

            Services.AddScoped<IOrderServicecs, OrderService>();
            Services.AddScoped<Func<IOrderServicecs>>(Provider => () => Provider.GetRequiredService<IOrderServicecs>());

            Services.AddScoped<ICacheService,CacheService>();

            return Services;
        }
    }
}
