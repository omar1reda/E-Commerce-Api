using Talabat.APIS.Helper;
using Talabat.Core.Entityes;
using Talabat.Core.I_Repository;
using Talabat.Repository.Data;
using Talabat.Repository;
using Microsoft.EntityFrameworkCore;
using Talabat.Repository.Repositorys;
using Talabat.Core;
using Talabat.Core.I_Services;
using Talabat.Services;

namespace Talabat.APIS.ExtensionMethode
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddAppplicationServices(this IServiceCollection Services)
        {
            
            //Generic To => GenericRepositor ==>
            Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();


            Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            Services.AddScoped(typeof(IOrderServise), typeof(OrderServise));

            //Generic To => All GenericRepositor ==>
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            // Mapping All Profiles ==>
            Services.AddAutoMapper(typeof(MappingProfiles));


            return Services;
        }
}
}
