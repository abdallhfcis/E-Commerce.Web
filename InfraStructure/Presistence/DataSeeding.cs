using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Identity.Core;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using DomainLayer.Models.IdentityModule;
using Presistence.Identity;

namespace Presistence
{
    public class DataSeeding(StoreDbContext _storeDbContext
        , UserManager<ApplicationUser> _userManager
        , RoleManager<IdentityRole> _roleManager
        ,StoreIdentityDbContext _identityDbContext
        ) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                var PendingMigrations = await _storeDbContext.Database.GetPendingMigrationsAsync();
                if (PendingMigrations.Any())
                    await _storeDbContext.Database.MigrateAsync();

                if (!_storeDbContext.Set<ProductBrand>().Any())
                {
                    //var ProductBrandData = File.ReadAllText(@"..\InfraStructure\Presistence\Data\DataSeed\brands.json");
                    var ProductBrandData = File.OpenRead(@"..\InfraStructure\Presistence\Data\DataSeed\brands.json");

                    //Conver to C# Objects[Productbrand]
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands is not null && ProductBrands.Any())
                    {
                        await _storeDbContext.ProductBrands.AddRangeAsync(ProductBrands);
                    }
                }


                if (!_storeDbContext.Set<ProductType>().Any())
                {
                    var ProductTypedata = File.OpenRead(@"..\InfraStructure\Presistence\Data\DataSeed\types.json");
                    //Conver to C# Objects[Productbrand]
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypedata);
                    if (ProductTypes != null && ProductTypes.Any())
                    {
                        await _storeDbContext.ProductTypes.AddRangeAsync(ProductTypes);
                    }
                }


                if (!_storeDbContext.Set<Product>().Any())
                {
                    var ProductsData = File.OpenRead(@"..\InfraStructure\Presistence\Data\DataSeed\products.json");
                    //Conver to C# Objects[Productbrand]
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                    if (Products != null && Products.Any())
                    {
                        await _storeDbContext.Products.AddRangeAsync(Products);
                    }
                }


                if (!_storeDbContext.Set<DeliveryMethod>().Any())
                {
                    var DeliveryMethodsData = File.OpenRead(@"..\InfraStructure\Presistence\Data\DataSeed\delivery.json");
                    //Conver to C# Objects[Productbrand]
                    var DeliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryMethodsData);
                    if (DeliveryMethods != null && DeliveryMethods.Any())
                    {
                        await _storeDbContext.Set<DeliveryMethod>().AddRangeAsync(DeliveryMethods);
                    }
                }

                await _storeDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //TODo
            }


        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any()) 
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                   await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                
                }

                if(!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    { 
                        Email="Abdallah@gmail.com",
                        DisplayName="ABdallah Abdelmoghny",
                        PhoneNumber="01126622069",
                        UserName="AbdallahAbdelmoghny"
                    
                    };
                    var User02 = new ApplicationUser()
                    {
                        Email = "Nada@gmail.com",
                        DisplayName = "Nada Saeid",
                        PhoneNumber = "01126622069",
                        UserName = "NadaSaeid"

                    };

                    await _userManager.CreateAsync(User01, "P@ssw0rd");
                    await _userManager.CreateAsync(User02, "P@ssw0rd");

                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin");
                    
                }

                await _identityDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            { 
            
            }
        }

       
    }
}
