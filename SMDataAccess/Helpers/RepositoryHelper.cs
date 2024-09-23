using SMDataAccess.Models.DataAccessModels;
using System.Data.SqlClient;

namespace SMDataAccess.Helpers;
public static class RepositoryHelper
{
    public static async Task<RepositoryResponse<IEnumerable<T>>> BuildEnumerableRepoResponse<T>(
        Task<(IEnumerable<T> data, int? totalCount)> queryTask,
        int? pageSize = null,
        int? pageNumber = null,
        ResponseMetadata? metadata = null)
    {
        RepositoryResponse<IEnumerable<T>> response = new();
        try
        {
            var (data, totalCount) = await queryTask;
            response.Data = data;
            
            if (totalCount is not null && pageNumber is not null && pageSize is not null)
                response.PaginationInfo = new PaginationInfo(pageNumber.Value, totalCount.Value, pageSize.Value);
        }
        catch (SqlException ex)
        {
            response.SqlException = ex;
        }
        catch (Exception) { throw; }

        if(metadata is not null)
            response.ResponseMetadata = metadata;

        return response;
    }
    public static async Task<RepositoryResponse<T>> BuildSingleRepoResponse<T>(
        Task<(IEnumerable<T> data, int? totalCount)> queryTask,
        ResponseMetadata? metadata = null)
    {
        RepositoryResponse<T> response = new();
        try
        {
            var (data, totalCount) = await queryTask;
            if (data.Any())
                response.Data = data.First();
        }
        catch (SqlException ex)
        {
            response.SqlException = ex;
        }
        catch (Exception) { throw; }

        if (metadata is not null)
            response.ResponseMetadata = metadata;

        return response;
    }
    public static async Task<RepositoryResponse<bool>> BuildDeleteRepoResponse(
        Task<int> deleteTask,
        ResponseMetadata? metadata = null)
    {
        RepositoryResponse<bool> response = new();
        try
        {
            int rowsAffected = await deleteTask;
            response.Data = rowsAffected > 0;
        }
        catch (SqlException ex)
        {
            response.SqlException = ex;
        }
        catch (Exception) { throw; }

        if (metadata is not null)
            response.ResponseMetadata = metadata;

        return response;
    }
}
