using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entityes.Order_Module
{
    public class DeliveryMethod:BaseEntitye
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string shortName, string descreption, string deliveryTime, decimal cost)
        {
            ShortName = shortName;
            Descreption = descreption;
            DeliveryTime = deliveryTime;
            Cost = cost;
        }

        public string ShortName { get; set; }
        public string Descreption{ get; set; }
        public string DeliveryTime { get; set; }
        public Decimal Cost { get; set; }

    }
}
