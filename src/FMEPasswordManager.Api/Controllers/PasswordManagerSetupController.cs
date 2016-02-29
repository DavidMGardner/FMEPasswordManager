using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FME.PasswordManager;
using FME.PasswordManager.Configuration;
using FME.PasswordManager.Interfaces;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace FMEPasswordManager.Api.Controllers
{
    [GlobalExceptionFilter]
    public class PasswordManagerSetupController : ApiController
    {
        private readonly IRepository<PasswordEntity> _repository;
        private readonly IConfiguration _configuration;

        public PasswordManagerSetupController(IRepository<PasswordEntity> repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("api/PasswordManagerSetup/CreateOrValidateRepository")]
        [MasterKeyHeaderFilter]
        public string EnsureContainer()
        {
            if (_repository.EnsureRepository())
                return "Success";

            return "Failure";
        }

        [HttpGet]
        [Route("api/management/EncryptMasterKey")]
        public string EncryptMasterKey()
        {
            var header = Request.GetFirstHeaderValueOrDefault<string>("X-MasterKey");
            if (String.IsNullOrWhiteSpace(header))
                throw new ApiParameterNullException("MasterKey was not been provided via http header");

            _configuration.MasterKey = header;
            return _configuration.EncryptedMasterKey;
        }

        [HttpGet]
        [Route("api/PasswordManagerSetup/Schema")]
        public JSchema GetSchema()
        {
            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema schema = generator.Generate(typeof(List<PasswordEntity>));

            return schema;
        }

        [HttpGet]
        [Route("api/PasswordManagerSetup/EntitySchema")]
        public JSchema GetEntitySchema()
        {
            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema schema = generator.Generate(typeof(PasswordEntity));

            return schema;
        }

        [HttpGet]
        [Route("api/PasswordManagerSetup/EntityExample")]
        public PasswordEntity GetEntityExample()
        {
            return new PasswordEntity
            {
                Id = Guid.Empty.ToString(),
                CommonName = String.Empty,
                Password = String.Empty,
                Url = String.Empty,
                UserName = String.Empty
            };
        }

        [HttpGet]
        [Route("api/PasswordManagerSetup/EntityListExample")]
        public List<PasswordEntity> GetListEntityExample()
        {
            return new List<PasswordEntity>()
            {
                new PasswordEntity
                {
                    Id = Guid.Empty.ToString(),
                    CommonName = String.Empty,
                    Password = String.Empty,
                    Url = String.Empty,
                    UserName = String.Empty
                }
            };
        }
    }
}
    