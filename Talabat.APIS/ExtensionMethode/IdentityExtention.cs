using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Talabat.Core.Entityes.Identity;
using Talabat.Core.I_Services;
using Talabat.Repository.Identity;
using Talabat.Services;

namespace Talabat.APIS.ExtensionMethode
{
    public static class IdentityExtention
    {
        public static IServiceCollection AddIdentityAppplication(this IServiceCollection Services , IConfiguration configuration)
        {
            // Depndancy=> Identity - Roles 
            Services.AddIdentity<AppUser, IdentityRole>()
               .AddEntityFrameworkStores<IdentityContext>();

            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:ValidIssuer"],

                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudience"],

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))

                    };
                    
                });

            //Token Services
            Services.AddSingleton<ITokenServices, TokenServices>();

            return Services;
        }
    }
}
