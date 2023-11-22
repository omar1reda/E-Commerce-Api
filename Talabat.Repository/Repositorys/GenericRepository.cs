using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes;
using Talabat.Core.I_Repository;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository.Repositorys
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntitye
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext Context)
        {
            _context = Context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>)await _context.Products.Include(p => p.ProductTypeId).Include(p => p.ProductBrandId).ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {

            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddItemAsync(T item)
        {

             await _context.Set<T>().AddAsync(item);
        }


        public async Task<IEnumerable<T>> GetAllSpecificationAsync(ISpecification<T> Spsfc)
        {

            return await SpecificationEvlautor<T>.GetQuery(_context.Set<T>(), Spsfc).ToListAsync();
        }

        public async Task<T> GetByIdASpecificationAsync(ISpecification<T> Spsfc)
        {
            return await SpecificationEvlautor<T>.GetQuery(_context.Set<T>(), Spsfc).FirstOrDefaultAsync();
        }

        public async Task AddAsync(T item)
        {
            await _context.Set<T>().AddAsync(item);
        }
    }
}
