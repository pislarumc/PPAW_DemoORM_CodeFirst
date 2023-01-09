using Autofac.Integration.WebApi;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NivelAccesDate_ORM_CodeFirst.Models;
using Business_Layer.Interfaces;
using System.Web.Http;
using Business_Layer;
using NivelAccesDate_ORM_CodeFirst.Interfaces;
using Business_Layer.CoreService.Interfaces;

namespace API_02.Interfaces
{
    public class ContainerConfigurer
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            // Register dependencies in controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            // Register individual types
            builder.RegisterType<DatabaseContext>()
            .As<IDatabaseContext>();

            //for user
            builder.RegisterType<UsersService>()
            .As<IUsers>();

            //for image
            builder.RegisterType<ImagesService>()
            .As<IImages>();

            //for history
            builder.RegisterType<HistoriesService>()
            .As<IHistories>();

            //for effect
            builder.RegisterType<EffectsService>()
            .As<IEffects>();

            //for cache
            builder.RegisterType<MemoryCacheService>()
           .As<ICache>();
            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;
            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }
    }
}
