﻿using System;
using System.Diagnostics;
using FME.PasswordManager.Configuration;
using FME.PasswordManager.Encryption;
using FME.PasswordManager.Exceptions;
using FME.PasswordManager.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Serilog;
using Serilog.Core;
using Shouldly;
using StructureMap;

namespace FME.PasswordManager.Tests
{
    [TestFixture]
    public class EncryptionItegrationTests
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
                x.For<IConfiguration>().Singleton().Use<Configuration.Configuration>();
                x.For<IEncryptionStrategy>().Use<TripleDESEncryptionStrategy>();
                x.For<ILogger>().Use(log);
            });
        }

        [Test]
        public void SaltShouldNotEmpty()
        {
            // arrange
            string salt = CipherUtility.GenerateSimpleSalt();
            var logger = _container.GetInstance<ILogger>();

            // act
            logger.Information(salt);

            // assert
            salt.ShouldNotBeNullOrWhiteSpace();
        }

        [Test]
        public void TripleDESShouldEncriptAndDecriptSameString()
        {
            // arrange
            var logger = _container.GetInstance<ILogger>();
            var encryptor = _container.GetInstance<IEncryptionStrategy>();

            string messageToEncrypt =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam venenatis eros viverra, lacinia nisi ac, bibendum erat. Sed dolor quam.";

            _container.GetInstance<IConfiguration>().MasterKey = Guid.NewGuid().ToString();

            // act
            string encryptedMessage = encryptor.Encrypt(messageToEncrypt);
            logger.Information(encryptedMessage);

            string decryptedMessage = encryptor.Decrypt(encryptedMessage);
            logger.Information(decryptedMessage);

            // assert
            decryptedMessage.ShouldNotBeNullOrEmpty();
            decryptedMessage.ShouldBe(messageToEncrypt);
        }

        [Test]
        public void EncryptionShouldThrowExceptionWhenUsingMistachedSalts()
        {
            // arrange
            var encryptor = _container.GetInstance<IEncryptionStrategy>();


            string messageToEncrypt = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam venenatis eros viverra, lacinia nisi ac, bibendum erat. Sed dolor quam.";
            _container.GetInstance<IConfiguration>().MasterKey = Guid.NewGuid().ToString();

            // act
            string encryptedMessage = encryptor.Encrypt(messageToEncrypt);
            _container.GetInstance<IConfiguration>().EncryptionSalt = "Bad Salt";

            // assert
            Should.Throw<EncryptionStrategyException>(() => encryptor.Decrypt(encryptedMessage));
        }

        [Test]
        public void EncryptionShouldThrowExceptionWhenUsingMistachedMasterKeys()
        {
            // arrange
            var encryptor = _container.GetInstance<IEncryptionStrategy>();

            string messageToEncrypt = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam venenatis eros viverra, lacinia nisi ac, bibendum erat. Sed dolor quam.";
            _container.GetInstance<IConfiguration>().MasterKey = "Good Key";

            // act
            string encryptedMessage = encryptor.Encrypt(messageToEncrypt);
            _container.GetInstance<IConfiguration>().MasterKey = "Bad Key";
            
            // assert
            Should.Throw<EncryptionStrategyException>(()=> encryptor.Decrypt(encryptedMessage));
        }

        [Test]
        public void AesShouldEncriptAndDecriptSameString()
        {
            // arrange
            _container.Configure(x =>
            {
                x.For<IEncryptionStrategy>().ClearAll().Use<AesEncryptionStrategy>();
            });

            var logger = _container.GetInstance<ILogger>();
            var encryptor = _container.GetInstance<IEncryptionStrategy>();
            var config = _container.GetInstance<IConfiguration>();

            string messageToEncrypt =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam venenatis eros viverra, lacinia nisi ac, bibendum erat. Sed dolor quam.";

            config.MasterKey = Guid.NewGuid().ToString();

            // act
            string encryptedMessage = encryptor.Encrypt(messageToEncrypt);
            logger.Information(encryptedMessage);

            string decryptedMessage = encryptor.Decrypt(encryptedMessage);
            logger.Information(decryptedMessage);

            // assert
            decryptedMessage.ShouldNotBeNullOrEmpty();
            decryptedMessage.ShouldBe(messageToEncrypt);
        }

        [Test]
        public void NullStrategyShouldEncriptAndDecriptSameString()
        {
            // arrange
            _container.Configure(x =>
            {
                x.For<IEncryptionStrategy>().ClearAll().Use<NullEncryptionStrategy>();
            });

            string salt = CipherUtility.GenerateSimpleSalt();
            var logger = _container.GetInstance<ILogger>();
            var encrypter = _container.GetInstance<IEncryptionStrategy>();
            var config = _container.GetInstance<IConfiguration>();

            string messageToEncrypt =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam venenatis eros viverra, lacinia nisi ac, bibendum erat. Sed dolor quam.";

            config.MasterKey = Guid.NewGuid().ToString();

            // act
            string encryptedMessage = encrypter.Encrypt(messageToEncrypt);
            logger.Information(encryptedMessage);

            string decryptedMessage = encrypter.Decrypt(encryptedMessage);
            logger.Information(decryptedMessage);

            // assert
            decryptedMessage.ShouldNotBeNullOrEmpty();
            decryptedMessage.ShouldBe(messageToEncrypt);
        }


        
    }
}
