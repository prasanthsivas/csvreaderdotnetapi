using System.Collections.Generic;

namespace csvreaderdotnetapi.Models.Repository
{
    public interface IDataRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(long id);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entityRange);
        void Update(TEntity dbEntity, TEntity entity);
        void Delete(TEntity entity);
    }
}
