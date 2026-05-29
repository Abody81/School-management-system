using AutoMapper;
using SMS.Application.UseCases.People.Commands.CreatePerson;
using SMS.Domain.Entities;
using SMS.WebAPI.DTOs.PersonDTOs;

namespace SMS.WebAPI.Mappings
{
    public class PersonMappings: Profile
    {
        public PersonMappings()
        {
            CreateMap<Person, PersonResponse>()
                .ForMember(dest => dest.ImageURl, opt => opt.MapFrom(src => src.ImagePath)).
                 ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.CountryName));  // dest => destination (الهدف) , src => source
                
        }
    }
    
}

