using System.ComponentModel;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using TCMManagement.Controllers;
using TCMManagement.Models;

namespace TCMManagement.BusinessLayer
{
    public class AutofacSetup
    {
        private static IContainer Container{get; set;}
        public static void ConfigureAutofac(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(AutoMapperConfig.Mapper)
                .As<IMapper>()
                .SingleInstance();
            
            builder.RegisterType<PersonService>()
                .As<IEntityServices<Person>>()
                .SingleInstance();
            
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            Container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }
    }
}