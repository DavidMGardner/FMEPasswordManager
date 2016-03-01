using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FME.PasswordManager.Configuration;
using FME.PasswordManager.Encryption;
using FME.PasswordManager.Interfaces;
using FME.PasswordManager.Persistence;
using FME.PasswordManager.Serialization;
using Serilog;
using Serilog.Core;
using StructureMap;
using StructureMap.Graph;

namespace FME.PasswordManager.Registry
{
    public class PasswordManagerRegistry : StructureMap.Registry
    {
        public PasswordManagerRegistry()
        {
            var levelSwitch = new LoggingLevelSwitch();

            var log = new LoggerConfiguration()
              .MinimumLevel.ControlledBy(levelSwitch)
              .WriteTo.ColoredConsole()
              .CreateLogger();
            
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
           
            For<IEntityPersistence<PasswordEntity>>().Use<JsonFilePersistence<PasswordEntity>>();
            For<IConfiguration>().Singleton().Use<Configuration.Configuration>();
            For<ISerialization<PasswordEntity>>().Use<FileSerialization<PasswordEntity>>();
            For<IEncryptionStrategy>().Use<TripleDESEncryptionStrategy>();
            For<IPasswordManagement>().Use<PasswordManagement>();
            For<ILogger>().Use(log);
            For<IRepository<PasswordEntity>>().Use<EntityRepository<PasswordEntity>>();
            For<IKeyPersistenceStrategy>().Singleton().Use<SynchronizedEncryptedKeyPersistenceStrategy>();

            Policies.FillAllPropertiesOfType<IConfiguration>().Singleton().Use<Configuration.Configuration>();
        }
    }
}
