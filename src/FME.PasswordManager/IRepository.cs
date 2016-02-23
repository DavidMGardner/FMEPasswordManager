using System;
using System.Linq;
using System.Linq.Expressions;

namespace FME.PasswordManager
{
    public interface IRepository<T> where T : IEntity
    {
        void Insert(T agregateRoot);

        void Delete(T agregateRoot);

        IQueryable<T> GetAll();

        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);

        T GetById(Guid id);
    }
}