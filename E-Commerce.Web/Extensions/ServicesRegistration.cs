using System.Text;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace E_Commerce.Web.Extensions
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddSwagerServices(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
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
