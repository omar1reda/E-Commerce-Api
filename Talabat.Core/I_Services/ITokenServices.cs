using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes.Identity;

namespace Talabat.Core.I_Services
{
    public interface ITokenServices
    {
        public Task<string> CreateToken(AppUser appUser);
    }
}
