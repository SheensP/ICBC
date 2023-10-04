using System;
using System.Linq;
using System.Linq.Expressions;
using ICBC.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ICBC.DAL.Repository
{
    public class RepositoryBase<T> where T : class
    {
        private readonly ICBCContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public RepositoryBase(ICBCContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).SingleOrDefault();
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual IList<T> GetList()
        {
            return _dbSet.ToList();
        }

        public virtual IList<T> GetList(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void AddRange(T[] entities)
        {
            _dbSet.AddRange(entities);
        }
    }
}

