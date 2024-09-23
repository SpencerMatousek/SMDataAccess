using SMDataAccess.Models.DataAccessModels;

namespace SMDataAccess.Data.IData;
public interface IRepository<T> where T : class
{
    Task<RepositoryResponse<IEnumerable<T>>> GetPaginatedAsync(int pageNumber, int pageSize);
    Task<RepositoryResponse<T>> GetByIdAsync(int id);
    Task<RepositoryResponse<T>> AddAsync(T entity);
    Task<RepositoryResponse<T>> UpdateAsync(T entity);
    Task<RepositoryResponse<bool>> DeleteAsync(int id);
}
