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

        // GET: api/PasswordManager
        //IEnumerable<PasswordEntity>
        [Route("api/PasswordManager")]
        [HttpPost]
        public IEnumerable<PasswordEntity> GetFromBody([FromBody]string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            throw new ApiParameterNullException("MasterKey was not been provided via Body");

            _repository.MasterKey = value;
            return _repository.GetAll();
        }

        // GET: api/PasswordManager/5
        public PasswordEntity Get(string id)
        {
            return _repository.GetById(id);
        }

        // POST: api/PasswordManager
        [HttpPost]
        [Route("api/PasswordManager/Insert")]
        public PasswordEntity Post([FromBody]PasswordManagementArgs value)
        {
            _repository.MasterKey = value.MasterKey;
            return _repository.Insert(value.Entity);
        }

        // PUT: api/PasswordManager/5
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
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

    public class PasswordManagementArgs
    {
        public PasswordEntity Entity { get; set; }
        public string MasterKey { get; set; }
    }
}
