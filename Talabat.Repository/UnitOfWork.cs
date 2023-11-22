using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entityes;
using Talabat.Core.I_Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Repositorys;

namespace Talabat.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly StoreContext _context;
        private readonly  Hashtable _Hashtable;

        public UnitOfWork(StoreContext context)
        {
            _context = context;
            _Hashtable = new Hashtable();
        }

        public async Task<int> CompleteAsync()
         {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }



        public IGenericRepository<T> RepositoryUonitOfWork<T>() where T : BaseEntitye
        {
            
            var type = typeof(T).Name;

            if(!_Hashtable.ContainsKey(type))
            {
                var Repository = new GenericRepository<T>(_context);
                _Hashtable.Add(type, Repository);

            }

            return ( IGenericRepository < T> )_Hashtable[type];

        }


    }
}
