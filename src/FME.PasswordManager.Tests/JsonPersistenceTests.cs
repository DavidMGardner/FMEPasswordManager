using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Serilog;
using Serilog.Core;
using Shouldly;
using StructureMap;

namespace FME.PasswordManager.Tests
{
    class JsonPersistenceTests
    {
        private IContainer _container;

        [SetUp]
        public void Init()
        {
            _container = IoC.Container;

            var levelSwitch = new LoggingLevelSwitch();

            var log = new LoggerConfiguration()
              .MinimumLevel.ControlledBy(levelSwitch)
              .WriteTo.ColoredConsole()
              .CreateLogger();

            _container.ForObject(log);

            _container.Configure(x =>
            {
                x.Scan(scan =>
                {
                    scan.WithDefaultConventions();
                });
                x.For<IEntityPersistence<PasswordEntity>>().Use<JsonFilePersistence<PasswordEntity>>();
                x.For<IConfiguration>().Use<Configuration>();
                x.For<IEncryptionStrategy>().Use<TripleDESEncryptionStrategy>();
                x.For<ILogger>().Use(log);
            });
        }

        [Test]
        public void SaveJsonStorageStructureShouldSucceed()
        {
            // arrange
            var storage = _container.GetInstance<IEntityPersistence<PasswordEntity>>();
            storage.Configuration.MasterKey = Guid.NewGuid().ToString();

            // act
            bool success = storage.PutList(new List<PasswordEntity>
            {
                new PasswordEntity
                {
                    CommonName = "Amazon",
                    Url = "www.amazon.com",
                    UserName = "dave@sample.com",
                    Password = "pass@word1"
                }
            });

            // assert
            success.ShouldBeTrue();
        }
    }
    
}
