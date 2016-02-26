using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Shouldly;
using StructureMap;

namespace FME.PasswordManager.Tests
{
    [TestFixture]
    public class ContainerTests
    {
        private IContainer _container;

        [SetUp]
        public void Init()
        {
            _container = IoC.Container;
            _container.Configure(x =>
            {
                x.For<ITestInterface>().Use<TestConcrete>();
                x.For<IKey>().Use<TestConcrete>();
            });
        }


        [Test]
        public void ResolveStaticContainerTypeShouldNotBeNull()
        {
            var container = new Container(_ =>
            {
                _.For<ITestInterface>().Use<TestConcrete>();
                _.Forward<ITestInterface, IKey>();
            });


            container.GetInstance<ITestInterface>().ShouldBeOfType<TestConcrete>();

            var testData = container.GetInstance<ITestInterface>();
            var keyData = (IKey) testData;

            testData.Success().ShouldBe("Success");
            testData.ShouldBeSameAs(keyData);
        }
    }



    public class TestConcrete : ITestInterface, IKey
    {
        public string Success()
        {
            return "Success";
        }

        public string MasterKey
        {
            get { throw new NotImplementedException(); }
        }
    }

    public interface IKey
    {
        string MasterKey { get; }
    }

    public interface ITestInterface
    {
        string Success();
    }
}
