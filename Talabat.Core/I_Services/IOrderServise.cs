using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes.Order_Module;

namespace Talabat.Core.I_Services
{
    public interface IOrderServise
    {
        public Task<Order> CreateOrderAsynk(string BuyerEmail, string BasketId , int DeliveryMethodId , AddressOrder addressOrder);
        public Task<Order> GetOrderByIdSpsifcationUserAsynk(string BuyerEmail, int OrderId);
        public Task<IEnumerable<Order>> GetOrdersForSpsifcationUserAsynk(string BuyerEmail );

    }
}
