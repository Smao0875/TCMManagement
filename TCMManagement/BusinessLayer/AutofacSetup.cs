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

            IEntityServices<TreatmentRecord> treatmentRecordService = new TreatmentRecordService();
            builder.RegisterInstance(treatmentRecordService)
                .As<IEntityServices<TreatmentRecord>>()
                .SingleInstance();

            IEntityServices<Appointment> appointmentService = new AppointmentService();
            builder.RegisterInstance(appointmentService)
                .As<IEntityServices<AppointmentService>>()
                .SingleInstance();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            var config = GlobalConfiguration.Configuration;

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}