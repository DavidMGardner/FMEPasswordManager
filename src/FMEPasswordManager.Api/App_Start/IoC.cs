using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using FME.PasswordManager;
using FME.PasswordManager.Configuration;
using FME.PasswordManager.Encryption;
using FME.PasswordManager.Interfaces;
using FME.PasswordManager.Persistence;
using FME.PasswordManager.Serialization;
using Serilog;
using Serilog.Core;
using StructureMap;
using StructureMap.Graph;

namespace FMEPasswordManager.Api.App_Start
{
    public static class IoC
    {
        private static readonly Lazy<Container> ContainerBuilder = new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container => ContainerBuilder.Value;

        private static Container DefaultContainer()
        {
            var levelSwitch = new LoggingLevelSwitch();

            var log = new LoggerConfiguration()
              .MinimumLevel.ControlledBy(levelSwitch)
              .WriteTo.ColoredConsole()
              .CreateLogger();

            var container = new Container();    
            
            container.Configure(x =>
            {
                x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
                x.For<IEntityPersistence<PasswordEntity>>().Use<JsonFilePersistence<PasswordEntity>>();
                x.For<IConfiguration>().Singleton().Use<Configuration>();
                x.For<ISerialization<PasswordEntity>>().Use<FileSerialization<PasswordEntity>>();
                x.For<IEncryptionStrategy>().Use<TripleDESEncryptionStrategy>();
                x.For<IPasswordManagement>().Use<PasswordManagement>();
                x.For<ILogger>().Use(log);
                x.For<IFilterProvider>().Use<StructureMapFilterProvider>();
                x.For<IRepository<PasswordEntity>>().Use<EntityRepository<PasswordEntity>>();

                x.For<IKeyPersistenceStrategy>().Singleton().Use<SynchronizedEncryptedKeyPersistenceStrategy>();

                x.Policies.FillAllPropertiesOfType<IConfiguration>().Singleton().Use<Configuration>();
            });

            container.AssertConfigurationIsValid();
            

            return container;
        }
    }

    internal class StructureMapFilterProvider : ActionDescriptorFilterProvider, IFilterProvider
    {
        private readonly IContainer _container;

        public StructureMapFilterProvider(IContainer container)
        {
            _container = container;
        }

        public new IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(configuration, actionDescriptor);

            var filterInfos = filters as IList<FilterInfo> ?? filters.ToList();
            foreach (var filter in filterInfos)
            {
                _container.BuildUp(filter.Instance);
            }

            return filterInfos;
        }
    }
}