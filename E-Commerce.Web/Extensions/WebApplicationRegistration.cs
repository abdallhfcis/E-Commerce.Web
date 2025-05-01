using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddelWare;

namespace E_Commerce.Web.Extensions
{
    public  static class WebApplicationRegistration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {
            using var Scoope = app.Services.CreateScope();
            var ObjectOfDataSeding = Scoope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeding.DataSeedAsync();
        }
        public static IApplicationBuilder UseCustomExceptionmiddelWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            return app;
        }
        public static IApplicationBuilder UseSwaggerMiddelWare(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
