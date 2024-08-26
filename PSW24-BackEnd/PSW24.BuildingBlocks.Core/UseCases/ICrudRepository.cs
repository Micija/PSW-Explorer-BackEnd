using PSW24.BuildingBlocks.Core.Domain;

namespace PSW24.BuildingBlocks.Core.UseCases;

public interface ICrudRepository<TEntity> where TEntity : Entity
{
    PagedResult<TEntity> GetPaged(int page, int pageSize);
    TEntity Get(long id);
    TEntity GetUntracked(long id);
    TEntity Create(TEntity entity);
    TEntity Update(TEntity entity);
    void Delete(long id);
}