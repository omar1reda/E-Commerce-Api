using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications
{
    public class ProductParams
    {
    
        public string? Sort { get; set; }
        public  int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public int PageIndex { get; set; } = 1;

        public int pageSize = 5;

        public int PageSize
        {
            get {  return pageSize; } 
            set { pageSize = value > 10 ? 10  : value ; }
        }

        public string? Search { get; set; }
    }
}
