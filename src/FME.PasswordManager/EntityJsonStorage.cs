using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FME.PasswordManager
{
    public class EntityJsonStorage<T> : IEntityStorage<T> where T : IEntity
    {
        private readonly IEntityPersistence<T> _persistence;
        private readonly IPasswordManagement _passwordManagement;

        public string MasterKey
        {
            set { _persistence.EncryptionStrategy.Configuration.MasterKey = value; } 
        }

        public EntityJsonStorage(IConfiguration configuration, IEntityPersistence<T> persistence, IPasswordManagement passwordManagement, IEncryptionStrategy encryptionStrategy)
        {
            _persistence = persistence;
            _passwordManagement = passwordManagement;
            _persistence.EncryptionStrategy = encryptionStrategy;
            _persistence.EncryptionStrategy.Configuration = configuration;
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
            return records.First(w => w.Id == id.ToString());
        }

        public T Insert(T agregateRoot)
        {
            agregateRoot.Id = _passwordManagement.GeneratePassword();

            var records = _persistence.GetList();
            records.Add(agregateRoot);

            if(_persistence.PutList(records))
                return agregateRoot;

            return default(T);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return _persistence.GetList().AsQueryable().Where(predicate);
        }
    }
}
