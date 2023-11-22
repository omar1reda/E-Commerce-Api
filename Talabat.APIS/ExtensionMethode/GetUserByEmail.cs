using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entityes.Identity;

namespace Talabat.APIS.ExtensionMethode
{
    public static class GetUserByEmail
    {
        public static async Task<AppUser> GetUserByEmailAsync(  this UserManager<AppUser> userManager , ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.Users.Include(u => u.address).FirstOrDefaultAsync(u => u.Email == email);
      
            return user ; 
           

        }
    }
}
