
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Presistence
{
    public static class InfraStructureServicesRegistertion
    {
        public static IServiceCollection AddInfraStructureServices(this IServiceCollection Services,IConfiguration Configuration)
        {
            Services.AddDbContext<StoreDbContext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("DefultConnection"));
            });
            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            return Services;
        }
    }
}
