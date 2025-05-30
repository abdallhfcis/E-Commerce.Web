using System.Text;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace E_Commerce.Web.Extensions
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddSwagerServices(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen(Options =>
            {
                Options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Name="Authorization",
                    Type=SecuritySchemeType.ApiKey,
                    Scheme="Bearer",
                    Description="Enter 'Bearer' Followed By Space and Your Token"
                });

                Options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference=new OpenApiReference
                        {
                            Id="Bearer",
                            Type=ReferenceType.SecurityScheme
                        }
                    },
                    new string[] {}
                    }
                });
            });
            return Services;
        }

        public static IServiceCollection AddWebApllicationServices(this IServiceCollection Services)
        {
            Services.Configure<ApiBehaviorOptions>((Options) =>
            {
                Options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorsResponse;
            });
            return Services;
        }

        public static IServiceCollection AddJwtServices(this IServiceCollection Services,IConfiguration _configuration)
        {

            Services.AddAuthentication(Config =>
                {
                    Config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    Config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(Option =>
                {
                    Option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer=true,
                        ValidIssuer = _configuration["JWTOptions:Issuer"],

                        ValidateAudience = true,
                        ValidAudience = _configuration["JWTOptions:Audience"],

                        ValidateLifetime = true,
                        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTOptions:SecretyKey"]))
                    };

                });


            return Services;
        }

    }
}
