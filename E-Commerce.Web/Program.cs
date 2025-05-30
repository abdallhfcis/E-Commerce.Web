
using System.Text.Json;
using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddelWare;
using E_Commerce.Web.Extensions;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presistence.Data;
using Presistence.Repositories;
using ServiceAbstraction;
using Services;
using Services.MappingProfilies;
using Shared.ErrorModels;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSwagerServices();
            builder.Services.AddInfraStructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWebApllicationServices();
            builder.Services.AddJwtServices(builder.Configuration);

            #endregion
            
            var app = builder.Build();

            #region DataSeeding

            app.SeedDataBaseAsync();

            #endregion


            #region Configure the HTTP request pipeline.
            app.UseCustomExceptionmiddelWare();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(Option =>
                {
                    Option.ConfigObject = new ConfigObject()
                    {
                        DisplayRequestDuration =true
                    };
                    Option.DocumentTitle = "My E-Commerce API";

                    Option.JsonSerializerOptions = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    Option.DocExpansion(DocExpansion.None);
                    Option.EnableFilter();
                    Option.EnablePersistAuthorization();
                });
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            #endregion

            app.Run();

           
            //Session 04 Start

        }
    }
}
