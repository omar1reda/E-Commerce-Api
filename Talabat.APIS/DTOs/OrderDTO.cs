using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entityes.Identity;
using Talabat.Core.Entityes.Order_Module;

namespace Talabat.APIS.DTOs
{
    public class OrderDTO
    {
        [Required]
        public int DeliveryMethodId { get; set; }
        [Required]
        public AddressDTO addressDTO { get; set; }
        [Required]
        public string BasketId { get; set; }
    }
}
