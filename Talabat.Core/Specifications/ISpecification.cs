using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes;

namespace Talabat.Core.Specifications
{
    public interface ISpecification<T>  where T : BaseEntitye
    {
        //where(p=>p.Id == Id)
        public Expression<Func<T,bool>> Criateria { get; set; }
        //Include(p=>p.ProductBrandId)
        public List< Expression<Func<T, object>>> Includes { get; set; }
        //OrderBy
        public Expression<Func<T, object>> OrderBy { get; set; }
        //OrderByDesc
        public Expression<Func<T, object >> OrderByDesc { get; set; }
        //PageIndex
        public int Skip { get; set; }
        //PageSize
        public int Take { get; set; }

        public bool IsPagenation { get; set; }
    }
}
