using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceAbstraction;
using Services.Specifications;
using Shared;
using Shared.DataTransferObjects;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Brands= await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
            return  _mapper.Map<IEnumerable<ProductBrand>,IEnumerable<BrandDto>>(Brands);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(int? BrandId, int? TypeId,ProductSortingOption sortingOption)
        {
            var specifications=new ProductsWithBrandsAndTypesSpecifications(BrandId,TypeId,sortingOption);
            var Products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(specifications);
            return _mapper.Map<IEnumerable<Product>,IEnumerable<ProductDto>>(Products);
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
            return _mapper.Map<Product,ProductDto>(Product);
        }
    }
}
