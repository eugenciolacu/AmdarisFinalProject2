using AmdarisInternship.Domain;
using AmdarisInternship.Infrastructure.Context;
using AmdarisInternship.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace AmdarisInternship.Infrastructure.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T Find(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.OrderByDescending(x => x.Id);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public T Update(T entity)
        {
            return _dbSet.Update(entity).Entity;
        }
    }
}
