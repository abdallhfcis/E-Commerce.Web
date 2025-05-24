using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.OrderModule;

namespace Services.Specifications.OrderModuleSpecifications
{
    internal class OrderSpecifications:BaseSpecifications<Order ,Guid>
    {
        public OrderSpecifications(string Email):base(O => O.UserEmail == Email )
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            AddOrderByDesxending(O => O.OrderDate); 
        }
        public OrderSpecifications(Guid Id) : base(O => O.Id == Id)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            
        }
    }
}
