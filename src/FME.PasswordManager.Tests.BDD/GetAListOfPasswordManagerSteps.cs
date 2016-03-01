using System;
using System.Linq;
using FME.PasswordManager.Configuration;
using FME.PasswordManager.Interfaces;
using FME.PasswordManager.Registry;
using Shouldly;
using StructureMap;
using TechTalk.SpecFlow;

namespace FME.PasswordManager.Tests.BDD
{
    [Binding]
    public class GetAListOfPasswordManagerSteps
    {
        private IContainer _container;
        private IRepository<PasswordEntity> _repository;
        private IConfiguration _configuration;

        private int _testCount = 0;

        [BeforeScenario]
        public void TestInitialize()
        {
            var registry = new StructureMap.Registry();
            registry.IncludeRegistry<PasswordManagerRegistry>();
            _container = new Container(registry);

            _repository = _container.GetInstance<IRepository<PasswordEntity>>();
            _configuration = _container.GetInstance<IConfiguration>();
        }

        [Given(@"I have a secret password of '(.*)'")]
        public void GivenIHaveASecretPasswordOf(string p0)
        {
            _configuration.MasterKey = p0;
        }
        
        [Given(@"I have a list of passwords")]
        public void GivenIHaveAListOfPasswords()
        {
            _testCount = _repository.GetAll().Count();
        }
        
        [When(@"I add a new password")]
        public void WhenIAddANewPassword()
        {
            _repository.Insert(new PasswordEntity
            {
                CommonName = "Sample",
                Password = "Sample",
                Url = "Sample",
                UserName = "Sample"
            });
        }
        
        [Then(@"the result should be count of passwords plus one")]
        public void ThenTheResultShouldBeCountOfPasswordsPlusOne()
        {
            int newcount = _repository.GetAll().Count();
            _testCount.ShouldBeLessThan(newcount);
        }
    }
}
