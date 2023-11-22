using AutoMapper;
using Talabat.APIS.DTOs;
using Talabat.Core.Entityes;
using Talabat.Core.Entityes.Identity;
using Talabat.Core.Entityes.Order_Module;

namespace Talabat.APIS.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(p=>p.productType, O=>O.MapFrom(p=>p.productType.Name))
                .ForMember(p=>p.productBrand , O=>O.MapFrom(p=>p.productBrand.Name))
                .ForMember(p => p.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());
            //.ForMember(p => p.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>())

            CreateMap<AppUser, UserDTO>();
            CreateMap < Address , AddressDTO > ().ReverseMap();

            CreateMap<AddressOrder, AddressDTO>().ReverseMap();

            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(O=>O.Status , r=>r.MapFrom(O=>O.Status))
                .ForMember(O=>O.DeliveryMethodtName , r=>r.MapFrom(O=>O.DeliveryMethod.ShortName))
                .ForMember(O=>O.DeliveryMethodCost , r=>r.MapFrom(O=>O.DeliveryMethod.Cost))
                .ForMember(O=>O.Total , r=>r.MapFrom(O=>O.GetTotal()));
        }
    }
}
