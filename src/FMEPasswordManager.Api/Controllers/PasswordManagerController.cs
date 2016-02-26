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

        [HttpGet]
        [Route("api/PasswordManager")]
        public IEnumerable<PasswordEntity> Get()
        {
            this.SetMasterKey((IKey)_repository);
            return _repository.GetAll();
        }

        [HttpGet]
        [Route("api/PasswordManager/{id}")]
        public PasswordEntity GetById(string id)
        {
            this.SetMasterKey((IKey)_repository);
            return _repository.GetById(id);
        }

        [HttpPost]
        [Route("api/PasswordManager")]
        public PasswordEntity Post([FromBody]PasswordEntity value)
        {
            this.SetMasterKey((IKey)_repository);
            return _repository.Insert(value);
        }

        [HttpPut]
        [Route("api/PasswordManager")]
        public PasswordEntity Put([FromBody]PasswordEntity value)
        {
            this.SetMasterKey((IKey)_repository);
            return _repository.Update(value);
        }

        // DELETE: api/PasswordManager/5
        public void Delete(string id)
        {
            this.SetMasterKey((IKey)_repository);
            var aggregateRoot = _repository.GetById(id);
            if(aggregateRoot != null)
                _repository.Delete(aggregateRoot);
        }
    }

    
}
