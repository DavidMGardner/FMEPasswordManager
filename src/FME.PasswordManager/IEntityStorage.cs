using System;
using System.Linq;
using System.Linq.Expressions;

namespace FME.PasswordManager
{
    public interface IEntityStorage<T> where T : IEntity 
    {
        void Delete(T agregateRoot);
        IQueryable<T> GetAll();
        T GetById(string id);
        T Insert(T agregateRoot);
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
    }
}