using Microsoft.EntityFrameworkCore;
using Talabat.Repository.Data;

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Linq.Expressions;
using Talabat.APIS.ExtensionMethode;
using Talabat.APIS.Helper;
using Talabat.Core.Entityes;
using Talabat.Core.Entityes.Identity;
using Talabat.Core.I_Repository;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Talabat.Core.I_Services;
using Talabat.Services;

namespace Talabat.APIS
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configur Servies - Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // _______DbContext ==>
            builder.Services.AddDbContext<StoreContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            // ____________ Identity Context ==> 
            builder.Services.AddDbContext<IdentityContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            // ----------Database Basket Redis ==>
            builder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
            {
                var Connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(Connection);
            });



            // coll  Identity  ==>
            builder.Services.AddIdentityAppplication(builder.Configuration);

            // coll All Services ==> 
            builder.Services.AddAppplicationServices();

            #endregion


            var app = builder.Build();


            //___________Update-Database ==> 
            #region Update-Database && Logger Factory Service
            using var Scope = app.Services.CreateScope();

            var Services = Scope.ServiceProvider;

            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                // ---- Update - Database ==> Dbcontext )
                var DbContext = Services.GetService<StoreContext>();
                await DbContext.Database.MigrateAsync();
                // ----- Update-Database ==> Identity )
                var identityContext = Services.GetService<IdentityContext>();
                await identityContext.Database.MigrateAsync();

                StorContextSeed.SeedAsync(DbContext);
            } 
            catch(Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error in Migration ##########################");
            }

          
            #endregion

            // Configure the HTTP request pipeline.
            #region Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //Lode Image or File
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            // ==> 
            app.UseAuthorization();
           // ==>
            app.UseAuthentication();

            app.MapControllers();
            #endregion

          


            app.Run();
        }

        private static void Typeof(IMapperConfigurationExpression expression)
        {
            throw new NotImplementedException();
        }
    }
}