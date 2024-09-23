using SMDataAccess.Data.IData;
using SMDataAccess.Models;
using SMDataAccess.Models.DataAccessModels;
using SMDataAccess.Services;
using Dapper;
using SMDataAccess.Scripts;
using SMDataAccess.Helpers;

namespace SMDataAccess.Data;
public class PartRepository : IPartRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    public PartRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    private async Task<(IEnumerable<TbPart>, int?)> getPartsHelper(string sql, object parameters, bool isPaginatedResponse = false)
    {
        Dictionary<int, TbPart> partLookup = new();
        var connection = _connectionFactory.CreateConnection();
        var grid = await connection.QueryMultipleAsync(sql: sql, param: parameters);

        int? totalCount = null;
        if(isPaginatedResponse)
            totalCount = grid.ReadFirst<int>();

        //Read parts
        var parts = grid.Read<TbPart>();
        //Read inventory
        var inventory = grid.Read<TbInventory>();
        
        foreach(var part in parts)
        {
            partLookup.Add(part.PartId, part);
        }

        foreach(var inv in inventory)
        {
            partLookup.TryGetValue(inv.PartId, out TbPart? partEntry);
            if(partEntry != null)
            {
                partEntry.Inventory ??= [];
                partEntry.Inventory.Add(inv);
            }
        }
        
        return (partLookup.Values, totalCount);
    }
    public async Task<RepositoryResponse<TbPart>> AddAsync(TbPart entity)
    {
        ResponseMetadata metaData = new("Add Part", "Part Added", "Failed to Add Part");
        var queryParams = new
        {
            entity.PartName,
            entity.Price,
            entity.Notes
        };
        var queryTask = getPartsHelper(sql: SqlScripts.PartAdd, parameters: queryParams);
        return await RepositoryHelper.BuildSingleRepoResponse(queryTask, metaData);
    }
    public async Task<RepositoryResponse<bool>> DeleteAsync(int id)
    {
        List<string> strings = new();
        var dt = new Helpers.DataTableHelper<string>();
        dt.ConvertToDt(strings);

        ResponseMetadata metaData = new("Delete Part", "Part Deleted", "Failed to Delete Part");
        var connection = _connectionFactory.CreateConnection();
        var deleteTask = connection.ExecuteAsync(sql: SqlScripts.PartDelete, param: new { PartId = id });
        return await RepositoryHelper.BuildDeleteRepoResponse(deleteTask, metaData);
    }
    public async Task<RepositoryResponse<TbPart>> GetByIdAsync(int id)
    {
        ResponseMetadata metaData = new("Get Part By Id", "Got Part by ID", "Failed to Get Part by ID");
        var queryTask = getPartsHelper(sql: SqlScripts.PartUpdate, parameters: new {PartId = id});
        return await RepositoryHelper.BuildSingleRepoResponse(queryTask, metaData);
    }
    public async Task<RepositoryResponse<TbPart>> UpdateAsync(TbPart entity)
    {
        ResponseMetadata metaData = new("Update Part", "Part Updated", "Failed to Update Part");
        var queryParams = new
        {
            entity.PartId,
            entity.PartName,
            entity.Price,
            entity.Notes
        };
        var queryTask = getPartsHelper(sql: SqlScripts.PartUpdate, parameters: queryParams);
        return await RepositoryHelper.BuildSingleRepoResponse(queryTask, metaData);
    }
    public async Task<RepositoryResponse<IEnumerable<TbPart>>> GetPaginatedAsync(int pageNumber, int pageSize)
    {
        ResponseMetadata metaData = new("Get Parts Paginated", "Got Parts", "Failed to Get Parts");
        var queryParams = new Dictionary<string, object>
        {
            {"PageSize", pageSize },
            {"PageNumber", pageNumber }
        };
        
        var qb = new QueryBuilder(SqlScripts.PartGetPaginated, queryParams);
        var orderByParams = new List<QueryOrderByParameter>()
        {
            new("p.PartId", true)
        };

        qb.BuildQuery(orderByParams);

        var queryTask = getPartsHelper(sql: qb.Query,
            parameters: qb.Parameters,
            isPaginatedResponse: true);
        return await RepositoryHelper.BuildEnumerableRepoResponse(queryTask, pageSize, pageNumber, metaData);
    }
}
