using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes.Identity;
using Talabat.Core.I_Services;

namespace Talabat.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration Configuration;

        public TokenServices(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task<string> CreateToken(AppUser appUser)
        {
            //Private Claims => 
            var AuthClaim = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName, appUser.DisplayName),
                new Claim(ClaimTypes.Email, appUser.Email)
            };
            // Is Key =>
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]));

            var Token = new JwtSecurityToken(
                issuer: Configuration["JWT:ValidIssuer"],
                audience: Configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(Configuration["JWT:DurationInDays"])),
                claims: AuthClaim,
                signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
    }
}
