using AutoMapper;
using Core.DomainModel;
using Identity;

namespace Adoption.Config
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, ApplicationUser>();
            CreateMap<Volunteer, ApplicationUser>();
        }
    }
}