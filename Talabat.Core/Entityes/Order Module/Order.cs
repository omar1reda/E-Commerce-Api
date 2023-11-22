using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entityes.Order_Module
{
    public class Order:BaseEntitye
    {
        public Order() { }
        public Order( AddressOrder shippingAddress, ICollection<OrderItem> orderItems, DeliveryMethod deliveryMethod ,string buyerEmail , decimal subTotal)
        {
        
            ShippingAddress = shippingAddress;
            OrderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            BuyerEmail = buyerEmail;
            SubTotal = subTotal;
        }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public AddressOrder ShippingAddress { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public  ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public DeliveryMethod DeliveryMethod { get; set; }
        public decimal SubTotal { get; set; }

        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethod.Cost;
        }

        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
