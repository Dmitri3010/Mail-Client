using MailClient.DAL.EF;
using MailClient.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MailClient.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        private void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IQueryable<TEntity> Query()
        {
            return _dbSet;
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public TEntity GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public TEntity Add(TEntity item)
        {
            TEntity entity = _dbSet.Add(item);
            SaveChanges();
            return entity;
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> items)
        {
            IEnumerable<TEntity> entities = _dbSet.AddRange(items);
            SaveChanges();
            return entities;
        }

        public TEntity Delete(Guid id)
        {
            TEntity entity = _dbSet.Remove(_dbSet.Find(id));
            SaveChanges();
            return entity;
        }

        public IEnumerable<TEntity> DeleteRange(IEnumerable<TEntity> items)
        {
            IEnumerable<TEntity> entities = _dbSet.RemoveRange(items);
            SaveChanges();
            return entities;
        }
    }
}
