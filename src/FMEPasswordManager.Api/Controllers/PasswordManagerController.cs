using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FME.PasswordManager;
using FME.PasswordManager.Interfaces;
using FMEPasswordManager.Api.Models;

namespace FMEPasswordManager.Api.Controllers
{
    [GlobalExceptionFilter]
    public class PasswordManagerController : ApiController
    {
        private readonly IRepository<PasswordEntity> _repository;

        public PasswordManagerController(IRepository<PasswordEntity> repository)
        {
            _repository = repository;
        }

        private void SetMasterKey()
        {
            var header = Request.GetFirstHeaderValueOrDefault<string>("X-MasterKey");
            if (String.IsNullOrWhiteSpace(header))
                throw new ApiParameterNullException("MasterKey was not been provided via http header");

            _repository.MasterKey(header);
        }

        [HttpGet]
        [Route("api/PasswordManager")]
        public IEnumerable<PasswordEntity> Get()
        {
            SetMasterKey();
            return _repository.GetAll();
        }

        [HttpGet]
        [Route("api/PasswordManager/{id}")]
        public PasswordEntity GetById(string id)
        {
            SetMasterKey();
            return _repository.GetById(id);
        }

        [HttpPost]
        [Route("api/PasswordManager")]
        public PasswordEntity Post([FromBody]PasswordEntity value)
        {
            SetMasterKey();
            return _repository.Insert(value);
        }

        [HttpPut]
        [Route("api/PasswordManager")]
        public PasswordEntity Put([FromBody]PasswordEntity value)
        {
            SetMasterKey();
            return _repository.Update(value);
        }

        // DELETE: api/PasswordManager/5
        public void Delete(string id)
        {
            SetMasterKey();
            var aggregateRoot = _repository.GetById(id);
            if(aggregateRoot != null)
                _repository.Delete(aggregateRoot);
        }
    }

    public class ApiParameterNullException : Exception
    {
        public ApiParameterNullException(string value) : base(value) {}
    }
}
