using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FME.PasswordManager.Configuration;
using FME.PasswordManager.Interfaces;
using Moo.Extenders;

namespace FME.PasswordManager
{
    public class EntityRepository<T> : IKey, IRepository<T> where T : IEntity
    {
        private readonly IConfiguration _configuration;
        private readonly IEntityPersistence<T> _persistence;
        private readonly IPasswordManagement _passwordManagement;

        public string MasterKey
        {
            set { _configuration.EncryptedMasterKey = value; } 
        }

        public EntityRepository(IConfiguration configuration, IEntityPersistence<T> persistence, IPasswordManagement passwordManagement)
        {
            _configuration = configuration;
            _persistence = persistence;
            _passwordManagement = passwordManagement;
        }

        public void Delete(T agregateRoot)
        {
            var records = _persistence.GetList();
            records.Remove(agregateRoot);
            _persistence.PutList(records);
        }

        public IQueryable<T> GetAll()
        {
            return _persistence.GetList().AsQueryable();
        }

        public T GetById(string id)
        {
            var records = _persistence.GetList();
            return records.First(w => w.Id == id.ToString()).MapTo<T>();
        }

        public T Insert(T agregateRoot)
        {
            agregateRoot.Id = _passwordManagement.GeneratePassword();

            var records = _persistence.GetList();
            records.Add(agregateRoot);

            if(_persistence.PutList(records))
                return agregateRoot.MapTo<T>();

            return default(T);
        }

        public T Update(T agregateRoot)
        {
            var records = _persistence.GetList();

            if(records.Count == 0)
                throw new Exception("Trying to update a record with an empty repository");

            var record = agregateRoot.MapTo(records.First(w => w.Id == agregateRoot.Id));

            if (_persistence.PutList(records))
                return record.MapTo<T>();

            return default(T);
        }

        public bool EnsureRepository()
        {
            return _persistence.EnsureList();
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return _persistence.GetList().AsQueryable().Where(predicate);
        }
    }
}
