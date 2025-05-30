using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.ProductModule;
using Shared;

namespace Services.Specifications
{
    class ProuductCountSpecification : BaseSpecifications<Product, int>
    {
        public ProuductCountSpecification(ProductQueryParams queryParams)
            : base(P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId)
            && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId)
            && (string.IsNullOrWhiteSpace(queryParams.search) || P.Name.ToLower().Contains(queryParams.search.ToLower()))
            )
        {

        }
    }
}
