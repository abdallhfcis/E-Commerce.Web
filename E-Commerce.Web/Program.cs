
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presistence.Data;
using Presistence.Repositories;
using ServiceAbstraction;
using Services;
using Services.MappingProfilies;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefultConnection"));
            });
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(AssemblyRefernce).Assembly);
            builder.Services.AddScoped<IServiceManager,ServiceManager>();
            #endregion
            
            var app = builder.Build();

            #region DataSeeding
            using var Scoope = app.Services.CreateScope();
            var ObjectOfDataSeding=Scoope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeding.DataSeedAsync();
            #endregion


            #region Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.MapControllers();
            #endregion

            app.Run();


            //Session 02 Start
            
        }
    }
}
