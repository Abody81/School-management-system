using AutoMapper;
using SMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Text;

namespace SMS.Business.PersonServices.Mappings
{
    public class PersonMappings : Profile
    {
        public PersonMappings()
        {
            CreateMap<Person, Person>()
                .ForMember(d => d.PersonID, o => o.Ignore())
                .ForMember(d => d.User, o => o.Ignore())
                .ForMember(d => d.Teacher, o => o.Ignore())
                .ForMember(d => d.Guardian, o => o.Ignore())
                .ForMember(d => d.Student, o => o.Ignore())
                .ForMember(d => d.Country, o => o.Ignore())
                .ForMember(d => d.ImagePath, o => o.Ignore());


        }
    }
}
