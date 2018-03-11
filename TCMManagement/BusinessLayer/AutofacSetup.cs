using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using System.Reflection;
using System.Web.Http;
using TCMManagement.Models;

namespace TCMManagement.BusinessLayer
{
    public class AutofacSetup
    {
        public static void ConfigureAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(AutoMapperConfig.Mapper)
                .As<IMapper>()
                .SingleInstance();

            IEntityServices<Person> personService = new PersonService();
            builder.RegisterInstance(personService)
                .As<IEntityServices<Person>>()
                .SingleInstance();

            IEntityServices<Patient> patientService = new PatientService();
            builder.RegisterInstance(patientService)
                .As<IEntityServices<Patient>>()
                .SingleInstance();

            IEntityServices<TreatmentRecord> treatmentRecordService = new TreatmentRecordService();
            builder.RegisterInstance(treatmentRecordService)
                .As<IEntityServices<TreatmentRecord>>()
                .SingleInstance();
/*
            IEntityServices<MedicalHistoryRecord> medicalRecordService = new MedicalRecordService();
            builder.RegisterInstance(medicalRecordService)
                .As<IEntityServices<MedicalHistoryRecord>>()
                .SingleInstance();
*/
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            var config = GlobalConfiguration.Configuration;

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}