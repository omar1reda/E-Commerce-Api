using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes;

namespace Talabat.Core.Specifications
{
    public  class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpecification(ProductParams Params)
            : base(P =>
            (string.IsNullOrEmpty(Params.Search) || P.Name.ToLower().Contains(Params.Search) )
            &&
            (!Params.TypeId.HasValue || P.ProductTypeId == Params.TypeId)
            &&
            (!Params.BrandId.HasValue || P.ProductBrandId == Params.BrandId))
        {
           
            Includes.Add(p => p.productBrand);
            Includes.Add(p => p.productType);

            if (Params.Sort != null)
            {
                Params.Sort = Params.Sort.ToLower();

                if (Params.Sort =="pricedesc")
                {
                    AddOrderByDesc(p=>p.Price);
                }
                else if(Params.Sort == "priceasc")
                {
                    AddOrderBy(p=>p.Price);
                }
                else
                {
                    AddOrderBy(p => p.Name);
                }
            }

           
            AddPagination(Params.PageSize * (  Params.PageIndex - 1), Params.PageSize);
        }

        public ProductWithBrandAndTypeSpecification(int id) : base(p=>p.Id == id )
        {
          
            Includes.Add(p => p.productBrand);
            Includes.Add(p => p.productType);
        }

      
    }
}
