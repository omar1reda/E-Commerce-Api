using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entityes;
using Talabat.Core.Entityes.Order_Module;
using Talabat.Core.I_Repository;
using Talabat.Core.I_Services;
using Talabat.Core.Specifications;

namespace Talabat.Services
{
    public class OrderServise : IOrderServise
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IGenericRepository<Product> _productRep;
        private readonly IUnitOfWork _unitOfWork;

        public OrderServise(IBasketRepository basketRepository , IGenericRepository<Product> ProductRep,
    
            IUnitOfWork unitOfWork)
        {
             _basketRepository = basketRepository;
            _productRep = ProductRep;
            this._unitOfWork = unitOfWork;
        }


        public async Task<Order> CreateOrderAsynk(string BuyerEmail, string BasketId, int DeliveryMethodId, AddressOrder addressOrder)
        {
            //get basket =>
          var  Basket =await _basketRepository.GetBasketAcync(BasketId);

          var BasketItems = new List<OrderItem>();

          decimal SubTotal = 0;

            foreach (var item in Basket.Items)
            {
                var product = await _unitOfWork.RepositoryUonitOfWork<Product>().GetByIdAsync(item.Id);

                var basketItem = new OrderItem(item.Id, item.ProductName, item.PictureUrl, product.Price, item.Quantity);

                BasketItems.Add(basketItem);
                SubTotal += product.Price * item.Quantity; 
            }

            var deliveryMethod =await _unitOfWork.RepositoryUonitOfWork<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);


            var order = new Order(addressOrder, BasketItems, deliveryMethod, BuyerEmail, SubTotal);

            await _unitOfWork.RepositoryUonitOfWork<Order>().AddAsync(order);

            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0)
            {
                return null;
            }
            return order;
        }

        public async Task<Order?> GetOrderByIdSpsifcationUserAsynk(string BuyerEmail, int OrderId)
        {
            var spec = new OrderWithBrandAndTypeSpecification(BuyerEmail , OrderId);
            var Order = await _unitOfWork.RepositoryUonitOfWork<Order>().GetByIdASpecificationAsync(spec);

            if (Order == null)
                return null;

            return Order;
        }

        public async Task<IEnumerable<Order?>> GetOrdersForSpsifcationUserAsynk(string BuyerEmail)
        {
            var spec = new OrderWithBrandAndTypeSpecification(BuyerEmail);
            var Orders = await _unitOfWork.RepositoryUonitOfWork<Order>().GetAllSpecificationAsync(spec);
            if (Orders == null)
                return null;

            return Orders;
        }


        public async Task<IEnumerable<DeliveryMethod?>> GetDeliveryMetodesAsync()
        {
            
            var deliveryMethod = await _unitOfWork.RepositoryUonitOfWork<DeliveryMethod>().GetAllAsync();

            if (deliveryMethod == null)
                return null;

            return deliveryMethod;
        }

    }
}
