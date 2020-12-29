using AutoMapper;
using Meetings.Domain.Entities;
using Meetings.Infrastructure.ViewModel;

namespace Meetings.Infrastructure.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<MeetingModel, Customer>()
                .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.CustomerId))
                .ReverseMap();
        }
    }
}
