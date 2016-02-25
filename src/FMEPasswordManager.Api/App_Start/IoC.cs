using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using FME.PasswordManager;
using FME.PasswordManager.Encryption;
using FME.PasswordManager.Interfaces;
using FME.PasswordManager.Persistence;
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
            container.ForObject(log);

            container.Configure(x =>
            {
                x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
                x.For<IEntityPersistence<PasswordEntity>>().Use<JsonFilePersistence<PasswordEntity>>();
                x.For<IRepository<PasswordEntity>>().Use<JsonRepository<PasswordEntity>>();
                x.For<IConfiguration>().Use<Configuration>();
                x.For<IEncryptionStrategy>().Use<TripleDESEncryptionStrategy>();
                x.For<IPasswordManagement>().Use<PasswordManagement>();
                x.For<ILogger>().Use(log);
            });

            return container;
        }
    }
}