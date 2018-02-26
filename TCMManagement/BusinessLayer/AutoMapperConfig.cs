using AutoMapper;
using TCMManagement.DTOs;
using TCMManagement.Models;

namespace TCMManagement.BusinessLayer
{
    public class AutoMapperConfig
    {
        public static IMapper Mapper;
        public static void ConfigureAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>{
                cfg.AddProfile(new PersonProfile());
            });  

            Mapper = config.CreateMapper();
            config.AssertConfigurationIsValid();
        }
    }

    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonCreation, Person>(MemberList.Source);
            CreateMap<TreatmentCreation, TreatmentRecord>(MemberList.Source);
            CreateMap<MedicalRecordCreation, MedicalHistoryRecord>(MemberList.Source);
        }
    }
}