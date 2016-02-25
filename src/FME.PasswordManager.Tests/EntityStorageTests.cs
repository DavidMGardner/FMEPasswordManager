using System;
using System.Configuration;
using System.Linq;
using FME.PasswordManager.Interfaces;
using FME.PasswordManager.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Serilog;
using Serilog.Core;
using Shouldly;
using StructureMap;

namespace FME.PasswordManager.Tests
{
    [TestFixture]
    public class EntityStorageTests
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
                x.For<IConfiguration>().Use<Configuration>();

                // use the memory persistence purly for testing
                x.For<IEntityPersistence<PasswordEntity>>().Use<EntityMemoryPersistence<PasswordEntity>>();
                x.For<IPasswordManagement>().Use<PasswordManagement>();
                x.For<ILogger>().Use(log);
                x.For<IRepository<PasswordEntity>>().Use<EntityRepository<PasswordEntity>>();
            });
        }

        [Test]
        public void SaveSingleEntityShouldAddOnlyOneRecordAndReturnAnEntityWithId()
        {
            // arrange
            var jsonPersistence = _container.GetInstance<IRepository<PasswordEntity>>();

            var listOfPasswordEntity = new PasswordEntity
            {
                CommonName = "Amazon",
                Url = "www.amazon.com",
                UserName = "dave@sample.com",
                Password = "pass@word1"
            };

            // act
            var entity = jsonPersistence.Insert(listOfPasswordEntity);

            // assert
            jsonPersistence.GetAll().Count().ShouldBe(1);
            jsonPersistence.GetAll().First().Id.ShouldNotBeEmpty();
        }

        [Test]
        public void UpdateEntityShouldChangeOnlyTheEntityWithMatchingId()
        {
            // arrange
            var jsonPersistence = _container.GetInstance<IRepository<PasswordEntity>>();

            var originalEntity = jsonPersistence.Insert(new PasswordEntity
            {
                CommonName = "Amazon",
                Url = "www.amazon.com",
                UserName = "dave@sample.com",
                Password = "pass@word1"
            });

            // act
            var updatedEntity = jsonPersistence.Update(new PasswordEntity
            {
                Id = originalEntity.Id,
                CommonName = "Updated",
                Password = "Updated",
                Url = "Updated",
                UserName = "Updated"
            });

            // assert
            jsonPersistence.GetAll().Count().ShouldBe(1);
            jsonPersistence.GetAll().First().Id.ShouldNotBeEmpty();

            originalEntity.Id.ShouldBe(updatedEntity.Id);
            originalEntity.CommonName.ShouldNotBe(updatedEntity.CommonName);
            originalEntity.Password.ShouldNotBe(updatedEntity.Password);
            originalEntity.Url.ShouldNotBe(updatedEntity.Url);
            originalEntity.UserName.ShouldNotBe(updatedEntity.UserName);
        }

        [Test]
        public void SaveSingleEntityShouldAddOnlyOneRecord()
        {
            // arrange
            var jsonPersistence = _container.GetInstance<IRepository<PasswordEntity>>();

            var listOfPasswordEntity = new PasswordEntity
            {
                CommonName = "Amazon",
                Url = "www.amazon.com",
                UserName = "dave@sample.com",
                Password = "pass@word1"
            };

            // act
            jsonPersistence.Insert(listOfPasswordEntity);

            // assert
            jsonPersistence.GetAll().Count().ShouldBe(1);
            jsonPersistence.GetAll().First().Id.ShouldNotBeEmpty();
        }


        [Test]
        public void DeleteSingleEntityWithNoRecordsShouldNotFail()
        {
            // arrange
            var jsonPersistence = _container.GetInstance<IRepository<PasswordEntity>>();

            var listOfPasswordEntity = new PasswordEntity
            {
                CommonName = "Amazon",
                Url = "www.amazon.com",
                UserName = "dave@sample.com",
                Password = "pass@word1"
            };

            // act
            jsonPersistence.Delete(listOfPasswordEntity);

            // assert
            jsonPersistence.GetAll().Count().ShouldBe(0);
        }

        [Test]
        public void DeleteSingleEntityWithIdShouldRemoveThatEntity()
        {
            // arrange
            var jsonPersistence = _container.GetInstance<IRepository<PasswordEntity>>();

            var listOfPasswordEntity = new PasswordEntity
            {
                CommonName = "Amazon",
                Url = "www.amazon.com",
                UserName = "dave@sample.com",
                Password = "pass@word1"
            };
            jsonPersistence.Insert(listOfPasswordEntity);
            var entityToRemove = jsonPersistence.GetAll().First();

            // act
            jsonPersistence.Delete(entityToRemove);

            // assert
            jsonPersistence.GetAll().Count().ShouldBe(0);
        }

        //[TestMethod]
        //public void GeneratePasswordShouldNotBeBlank()
        //{
        //    // Arrange
        //    var password = PasswordManagement.GeneratePassword();

        //    // Act

        //    // Assert
        //    password.ShouldNotBeNullOrEmpty();
        //}
    }
}