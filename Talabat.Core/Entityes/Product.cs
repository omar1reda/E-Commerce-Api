using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entityes
{
    public class Product :BaseEntitye
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int ProductBrandId { get; set; }
        public int ProductTypeId { get; set; }

        public ProductBrand productBrand { get; set;}

        public ProductType productType { get; set; }
    }
}
