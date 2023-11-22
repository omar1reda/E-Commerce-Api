using Talabat.Core.Entityes.Order_Module;

namespace Talabat.APIS.DTOs
{
    public class OrderToReturnDTO
    {


        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public AddressOrder ShippingAddress { get; set; }
        public string Status { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public string DeliveryMethodtName { get; set; }
        public Decimal DeliveryMethodCost { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }

        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
