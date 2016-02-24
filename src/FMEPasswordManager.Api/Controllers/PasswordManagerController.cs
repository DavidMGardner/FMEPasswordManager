using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FME.PasswordManager;

namespace FMEPasswordManager.Api.Controllers
{
    public class PasswordManagerController : ApiController
    {
        private readonly IRepository<PasswordEntity> _repository;

        public PasswordManagerController(IRepository<PasswordEntity> repository)
        {
            _repository = repository;
        }

        // GET: api/PasswordManager
        [Route("api/PasswordManager")]
        [HttpPost]
        public IEnumerable<PasswordEntity> GetFromBody([FromBody]string value)
        {
            _repository.MasterKey = value;
            return _repository.GetAll();
        }

        // GET: api/PasswordManager/5
        public PasswordEntity Get(string id)
        {
            return _repository.GetById(id);
        }

        // POST: api/PasswordManager
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

    public class PasswordManagementArgs
    {
        public PasswordEntity Entity { get; set; }
        public string MasterKey { get; set; }
    }
}
