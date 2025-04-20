using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence
{
    public class DataSeeding(StoreDbContext _storeDbContext): IDataSeeding
    {
        public void DataSeed()
        {
            try
            {
                if (_storeDbContext.Database.GetPendingMigrations().Any())
                    _storeDbContext.Database.Migrate();

                if (!_storeDbContext.ProductBrands.Any())
                {
                    var ProductBrandData = File.ReadAllText(@"..\InfraStructure\Presistence\Data\DataSeed\brands.json");
                    //Conver to C# Objects[Productbrand]
                    var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands != null && ProductBrands.Any())
                    {
                        _storeDbContext.ProductBrands.AddRange(ProductBrands);
                    }
                }


                if (!_storeDbContext.ProductTypes.Any())
                {
                    var ProductTypedata = File.ReadAllText(@"..\InfraStructure\Presistence\Data\DataSeed\types.json");
                    //Conver to C# Objects[Productbrand]
                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypedata);
                    if (ProductTypes != null && ProductTypes.Any())
                    {
                        _storeDbContext.ProductTypes.AddRange(ProductTypes);
                    }
                }


                if (!_storeDbContext.Products.Any())
                {
                    var ProductsData = File.ReadAllText(@"..\InfraStructure\Presistence\Data\DataSeed\products.json");
                    //Conver to C# Objects[Productbrand]
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    if (Products != null && Products.Any())
                    {
                        _storeDbContext.Products.AddRange(Products);
                    }
                }

                _storeDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODo
            }


        }
    }
}
