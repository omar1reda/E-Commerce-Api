using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes;
using Talabat.Core.I_Repository;

namespace Talabat.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    { 

        IGenericRepository<T> RepositoryUonitOfWork<T>() where T : BaseEntitye;

      Task<int > CompleteAsync();
    }
}
