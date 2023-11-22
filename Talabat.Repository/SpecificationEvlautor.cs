using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationEvlautor<T>  where T : BaseEntitye
    {
        // StartQuery =>  _context.Set<T>()
        // Specific   =>  Specific.Criateria && Specific.Inclode
        public static IQueryable<T> GetQuery(IQueryable<T> StartQuery,ISpecification<T> baseSpecification)
        {
            var query = StartQuery;

            if (baseSpecification.Criateria != null) 
                query = query.Where(baseSpecification.Criateria);
            

            //OrderBy
            if (baseSpecification.OrderBy != null)    
                query = query.OrderBy(baseSpecification.OrderBy);

            if (baseSpecification.OrderByDesc != null)
                query = query.OrderByDescending(baseSpecification.OrderByDesc);

            //Pagenation
            if (baseSpecification.IsPagenation)
            {
                query = query.Skip(baseSpecification.Skip).Take(baseSpecification.Take);
            }
            //Include
            query = baseSpecification.Includes.Aggregate(query, (CurrentQuery, InclodeExpretion) => CurrentQuery.Include(InclodeExpretion));

            return query;

           


        }
    }
}
