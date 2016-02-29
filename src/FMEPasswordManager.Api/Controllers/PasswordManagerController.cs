using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FME.PasswordManager;
using FME.PasswordManager.Configuration;
using FME.PasswordManager.Interfaces;
using FMEPasswordManager.Api.Models;

namespace FMEPasswordManager.Api.Controllers
{
    [GlobalExceptionFilter]
    public class PasswordManagerController : BaseApiController
    {
        private readonly IRepository<PasswordEntity> _repository;

        public PasswordManagerController(IRepository<PasswordEntity> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/PasswordManager")]
        [MasterKeyHeaderFilter]
        public IEnumerable<PasswordEntity> Get()
        {
            return _repository.GetAll();
        }

        [HttpGet]
        [Route("api/PasswordManager/{id}")]
        [MasterKeyHeaderFilter]
        public PasswordEntity GetById(string id)
        {
            return _repository.GetById(id);
        }

        [HttpPost]
        [Route("api/PasswordManager")]
        [MasterKeyHeaderFilter]
        public PasswordEntity Post([FromBody]PasswordEntity value)
        {
            return _repository.Insert(value);
        }

        [HttpPut]
        [Route("api/PasswordManager")]
        [MasterKeyHeaderFilter]
        public PasswordEntity Put([FromBody]PasswordEntity value)
        {
            return _repository.Update(value);
        }

        // DELETE: api/PasswordManager/5
        [MasterKeyHeaderFilter]
        public void Delete(string id)
        {
            var aggregateRoot = _repository.GetById(id);
            if(aggregateRoot != null)
                _repository.Delete(aggregateRoot);
        }
    }
}
