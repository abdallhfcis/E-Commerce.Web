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
           Services.AddScoped<IServiceManager, ServiceManager>();
            return Services;
        }
    }
}
