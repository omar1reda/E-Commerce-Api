using AutoMapper;
using Talabat.APIS.DTOs;
using Talabat.Core.Entityes;

namespace Talabat.APIS.Helper
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
           
          _configuration = configuration;
        }


        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if(source.PictureUrl!=null)
            {
                return $"{_configuration["ImageUrl"]}{source.PictureUrl}";
            }
            return "";
        }
    }
}
