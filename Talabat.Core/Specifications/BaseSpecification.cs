﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes;

namespace Talabat.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntitye
    {
        public Expression<Func<T, bool>> Criateria { get  ; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set; }

        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagenation { get; set; } 

        public BaseSpecification()
        {
            Includes = new List<Expression<Func<T, object>>>();

        }
        public BaseSpecification(Expression<Func<T,bool>> criateria)
        {
            Criateria = criateria;
            Includes = new List<Expression<Func<T, object>>>();
        }


        public void AddOrderBy(Expression<Func<T, object>> expression)
        {
            OrderBy = expression;
        }

        public void AddOrderByDesc(Expression<Func<T, object>> expression)
        {
            OrderByDesc = expression;
        }

        public void AddPagination(int skip, int take)
        {
            IsPagenation = true;
            Skip = skip;
            Take = take;
            
        }

    }
}
