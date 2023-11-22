using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes.Order_Module;

namespace Talabat.Core.Specifications
{
    public class OrderWithBrandAndTypeSpecification:BaseSpecification<Order>
    {
        public OrderWithBrandAndTypeSpecification(string email):base(O=>O.BuyerEmail==email)
        {
          
          Includes.Add(O=>O.DeliveryMethod);
            Includes.Add(O => O.OrderItems);
            AddOrderByDesc(O => O.OrderDate );

        }

        public OrderWithBrandAndTypeSpecification(string email , int OrderId) : base( O=> O.Id == OrderId && O.BuyerEmail == email  )
        {
      
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.OrderItems);
           

        }
    }
}
