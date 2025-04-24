using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace Services.Specifications
{
     class ProductsWithBrandsAndTypesSpecifications : BaseSpecifications<Product, int>
    {
        public ProductsWithBrandsAndTypesSpecifications():base(null)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
        public ProductsWithBrandsAndTypesSpecifications(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
