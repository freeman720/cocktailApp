using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CocktailDAL
{
    public class RepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext { get; set; }

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public IEnumerable<T> Find()
        {
            return RepositoryContext.Set<T>();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return RepositoryContext.Set<T>().Where(expression);
        }

        public void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }

        public void Delete()
        {
            RepositoryContext.Set<T>().RemoveRange(Find());
            RepositoryContext.SaveChanges();
        }

        public void Save()
        {
            RepositoryContext.SaveChanges();
        }
    }
}
