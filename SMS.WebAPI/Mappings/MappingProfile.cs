using AutoMapper;
using SMS.Core.Entities;
using SMS.WebAPI.DTOs.PersonDTOs;

namespace SMS.WebAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonResponseDTO>()
                .ForMember(dest => dest.ImageURl, opt => opt.MapFrom(src => src.ImagePath)).
                 ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.CountryName));  // dest => destination (الهدف) , src => source

            CreateMap<PersonCreateDTO, Person>();

            CreateMap<PersonUpdateDTO, Person>();
                
        }
    }
}

