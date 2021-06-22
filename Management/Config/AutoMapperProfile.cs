using AutoMapper;
using Core.DomainModel;
using Identity;

namespace Management.Config
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Volunteer, ApplicationUser>();
            CreateMap<Customer, ApplicationUser>();
        }
    }
}