using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using System.ComponentModel;
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

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            var config = GlobalConfiguration.Configuration;

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}