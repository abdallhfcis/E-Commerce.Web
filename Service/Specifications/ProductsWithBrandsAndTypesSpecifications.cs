using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.ProductModule;
using Shared;

namespace Services.Specifications
{
    class ProductsWithBrandsAndTypesSpecifications : BaseSpecifications<Product, int>
    {
        public ProductsWithBrandsAndTypesSpecifications(ProductQueryParams queryParams) 
            :base(P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId) 
            && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId)
            && (string.IsNullOrWhiteSpace(queryParams.search) || P.Name.ToLower().Contains(queryParams.search.ToLower()))
            )
           
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

            switch (queryParams.sort)
            {
                case ProductSortingOption.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOption .NameDesc:
                    AddOrderByDesxending(P => P.Name);
                    break;
                case ProductSortingOption.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortingOption.PriceDesc:
                    AddOrderByDesxending (P => P.Price);
                    break;
                default:break;
            }

            ApplyPagintion(queryParams.PageSize, queryParams.pageNumber);

        }
        public ProductsWithBrandsAndTypesSpecifications(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
