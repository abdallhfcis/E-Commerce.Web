using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAbstraction;

namespace Services
{
    public class ServiceManagerWithFactoryDelegate (Func<IProductService> ProductFactory
        ,Func<IBasketService> BasketFactory
        ,Func<IAuthenticationService> AuthenticationFactory
        ,Func<IOrderServicecs> OrderFactory): IServiceManager
    {
        public IProductService ProductService => ProductFactory.Invoke();

        public IBasketService BasketService => BasketFactory.Invoke();

        public IAuthenticationService AuthenticationService => AuthenticationFactory.Invoke();

        public IOrderServicecs OrderServicecs => OrderFactory.Invoke();
    }
}
