using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Query();

        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);

        TEntity GetById(Guid id);

        IEnumerable<TEntity> GetAll();

        TEntity Add(TEntity item);

        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> items);

        TEntity Delete(Guid id);

        IEnumerable<TEntity> DeleteRange(IEnumerable<TEntity> items);
    }
}
