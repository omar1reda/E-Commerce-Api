using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes;

namespace Talabat.Core.I_Repository
{
    public interface IBasketRepository
    {
        Task<Baskets?> GetBasketAcync(string IdBasket);

        Task<Baskets?> CreateOrUpdateAcync(Baskets baskets);

        Task<bool> DeleteBasketAcync(string id);
    }
}
