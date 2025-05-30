using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using ServiceAbstraction;
using Services.Specifications.OrderModuleSpecifications;
using Shared.DataTransferObjects.OrderDtos;
using Shared.Identity;

namespace Services
{
    public class OrderService (IMapper _mapper,IBasketRepository _basketRepository,IUnitOfWork _unitOfWork): IOrderServicecs
    {
        public async Task<OrderToReturn> CreateOrder(OrderDto orderDto, string Email)
        {
            //map Address To Order Address 
            var OrderAddress = _mapper.Map<AddressDto, OrderAddress>(orderDto.shipToAddress);
            //Get Basket
            var Basket = await _basketRepository.GetBasketAsync(orderDto.BasketId)
                ?? throw new BasketNotFoundException(orderDto.BasketId);
            //Create ItemList
            List<OrderItem> OrderItems= [];
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);

                OrderItems.Add(Createorderitem(item, Product));
            }
            //Get Delivery Method
            var DeliveryMethod= await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId)
                            ?? throw new  DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);
            //Calculate Sub Total

            var SubTotal =OrderItems.Sum(O => O.Quantity * O.Price);

            var Order = new Order(Email, OrderAddress, DeliveryMethod, OrderItems, SubTotal);

            await _unitOfWork.GetRepository<Order,Guid>().AddAsync(Order);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Order,OrderToReturn>(Order);
        }

        private static OrderItem Createorderitem(DomainLayer.Models.BasketModule.BasketItem item, Product Product)
        {
            return  new OrderItem()
            {
                Product = new ProductItemOrdered() { ProductId = Product.Id, PictureUrl = Product.PictureUrl, ProductName = Product.Name },
                Price = item.Price,
                Quantity = item.Quantity
            };
        }

        public async Task<IEnumerable<DeliveryMethodsDtos>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();

            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodsDtos>>(DeliveryMethods);
        }

        public async Task<IEnumerable<OrderToReturn>> GetAllOrdesrsAsync(string Email)
        {
            var Spec=new OrderSpecifications(Email);
            var Orders=await _unitOfWork.GetRepository<Order ,Guid>().GetAllAsync(Spec);

            return _mapper.Map<IEnumerable<Order>,IEnumerable<OrderToReturn>>(Orders);
        }

        public async Task<OrderToReturn> GetOrderByIdAsync(Guid Id)
        {
            var Spec=new OrderSpecifications(Id);
            var Order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(Spec);
            return _mapper.Map<Order ,OrderToReturn>(Order);
        }
    }
}
