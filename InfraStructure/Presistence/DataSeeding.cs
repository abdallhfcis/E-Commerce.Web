using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence
{
    public class DataSeeding(StoreDbContext _storeDbContext): IDataSeeding
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

                await _storeDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //TODo
            }


        }

       
    }
}
