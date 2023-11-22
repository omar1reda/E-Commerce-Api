using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes;
using Talabat.Core.Specifications;

namespace Talabat.Core.I_Repository
{
    public interface IGenericRepository <T> where T : BaseEntitye
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);
        Task AddAsync(T item);

        Task<IEnumerable<T>> GetAllSpecificationAsync(ISpecification<T> Spsfc);

        Task<T> GetByIdASpecificationAsync(ISpecification<T> Spsfc);

    }
}
