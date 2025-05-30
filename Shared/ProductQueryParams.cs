using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParams
    {
        private const int DefultPageSize = 5;
        private const int MaxPageSize = 10;

        public int? BrandId {  get; set; }
        public int? TypeId{ get; set; }
        public ProductSortingOption sort { get; set; } 
        public string? search { get; set; }
        public int pageNumber { get; set; } = 1;
        private int pageSize= DefultPageSize;
        public int PageSize
        { 
            get => pageSize;
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

    }
}
