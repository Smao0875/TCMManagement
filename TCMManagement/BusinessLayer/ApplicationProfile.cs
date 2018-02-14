using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TCMManagement.Models;

namespace TCMManagement.BusinessLayer
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Person, Person>().ReverseMap();
        }
    }
}