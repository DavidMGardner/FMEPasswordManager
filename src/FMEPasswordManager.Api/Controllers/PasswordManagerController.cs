using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FME.PasswordManager;

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

        [HttpPost]
        [Route("api/PasswordManager")]
        public IEnumerable<PasswordEntity> GetFromBody([FromBody]string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ApiParameterNullException("MasterKey was not been provided via Body");

            _repository.MasterKey = value;
            return _repository.GetAll();
        }

        [HttpPost]
        [Route("api/PasswordManager/Insert")]
        public PasswordEntity Post([FromBody]PasswordManagementViewModel value)
        {
            if (String.IsNullOrWhiteSpace(value.MasterKey))
                throw new ApiParameterNullException("MasterKey was not been provided via Body");

            _repository.MasterKey = value.MasterKey;
            return _repository.Insert(value.Entity);
        }

        [HttpPut]
        [Route("api/PasswordManager")]
        public PasswordEntity Put([FromBody]PasswordManagementViewModel value)
        {
            if (String.IsNullOrWhiteSpace(value.MasterKey))
                throw new ApiParameterNullException("MasterKey was not been provided via Body");

            _repository.MasterKey = value.MasterKey;
            return _repository.Update(value.Entity);
        }

        // DELETE: api/PasswordManager/5
        public void Delete(string id)
        {
            var aggregateRoot = _repository.GetById(id);
            if(aggregateRoot != null)
                _repository.Delete(aggregateRoot);
        }
    }

    public class ApiParameterNullException : Exception
    {
        public ApiParameterNullException(string value) : base(value) {}
    }

    public class PasswordManagementViewModel
    {
        public PasswordEntity Entity { get; set; }
        public string MasterKey { get; set; }
    }
}
