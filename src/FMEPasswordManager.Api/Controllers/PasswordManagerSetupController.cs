using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FME.PasswordManager;
using FME.PasswordManager.Interfaces;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace FMEPasswordManager.Api.Controllers
{
    [GlobalExceptionFilter]
    public class PasswordManagerSetupController : ApiController
    {
        private readonly IRepository<PasswordEntity> _repository;

        public PasswordManagerSetupController(IRepository<PasswordEntity> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/PasswordManagerSetup/CreateOrValidateRepository")]
        public string EnsureContainer()
        {
            this.SetMasterKey((IKey)_repository);
            if (_repository.EnsureRepository())
                return "Success";
            else
            {
                return "Failure";
            }
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
    