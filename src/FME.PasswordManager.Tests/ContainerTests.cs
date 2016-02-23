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
            });
        }


        [Test]
        public void ResolveStaticContainerTypeShouldNotBeNull()
        {
            var testData = _container.GetInstance<ITestInterface>();

            testData.Success().ShouldBe("Success");
        }
    }



    public class TestConcrete : ITestInterface
    {
        public string Success()
        {
            return "Success";
        }
    }

    public interface ITestInterface
    {
        string Success();
    }
}
