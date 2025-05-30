using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.ProductModule;
using ServiceAbstraction;
using Services.Specifications;
using Shared;
using Shared.DataTransferObjects.ProductModuleDtos;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Brands= await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
            return  _mapper.Map<IEnumerable<ProductBrand>,IEnumerable<BrandDto>>(Brands);
        }

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var specifications=new ProductsWithBrandsAndTypesSpecifications(queryParams);
            var Products = await Repo.GetAllAsync(specifications);
            var Data=_mapper.Map<IEnumerable<Product>,IEnumerable<ProductDto>>(Products);
            var ProductCount =Products.Count();
            var CountSpec=new ProuductCountSpecification(queryParams);
            var TotalCount= await Repo.CountAsync(CountSpec);
            return new PaginatedResult<ProductDto>(queryParams.pageNumber,ProductCount, TotalCount, Data);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {

            var Types= await _unitOfWork.GetRepository<ProductType,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>,IEnumerable<TypeDto>>(Types);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var specifications = new ProductsWithBrandsAndTypesSpecifications(id);
            var Product= await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(specifications);
            if(Product == null)
            {
                throw new ProductNotFound(id);
            }
            return _mapper.Map<Product,ProductDto>(Product);
        }
    }
}
