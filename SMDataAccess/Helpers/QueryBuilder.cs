using SMDataAccess.Models.DataAccessModels;
using System.Text;

namespace SMDataAccess.Helpers;

/// <summary>
/// Utility for adding filters and order by parameters to a query.
/// </summary>
public class QueryBuilder
{
    /// <summary>
    /// Initializes the QueryBuilder with a SQL scripts and default parameters
    /// </summary>
    /// <param name="initialQuery">Query that needs to be built</param>
    /// <param name="initialParameters">Parameters before query has been built</param>
    public QueryBuilder(string initialQuery, Dictionary<string, object> initialParameters)
    {
        Query = initialQuery;
        Parameters = initialParameters ?? new();
    }

    public string Query { get; set; }
    public Dictionary<string, object> Parameters { get; set; } = new();
    private void AddFilterParameters(List<QueryFilterParameter> filterParameters, bool isFirstFilter)
    {
        StringBuilder queryStringBuilder = new();
        bool isFirst = isFirstFilter;
        foreach (var filter in filterParameters)
        {
            queryStringBuilder.Append(filter.ToString(isFirst));
            isFirst = false;

            if (filter.UsesValue)
            {
                if (filter.Value == null) throw new ArgumentException($"{nameof(filter.Value)} can only be null for IS_NULL or IS_NOT_NULL");
                if (filter.ParameterName == null) throw new InvalidCastException();
                Parameters.Add(filter.ParameterName, filter.Value);
            }
        }
        Query = Query.Replace("/*@QueryFilters@*/", queryStringBuilder.ToString());
    }
    private void AddOrderByParameters(List<QueryOrderByParameter> orderByParameters)
    {
        StringBuilder queryStringBuilder = new();
        bool isFirst = true;

        foreach (var orderBy in orderByParameters)
        {
            queryStringBuilder.Append(orderBy.ToString(isFirst));
            isFirst = false;
        }

        Query = Query.Replace("/*@OrderParams@*/", queryStringBuilder.ToString());
    }

    /// <summary>
    /// Build a query with filter parameters.
    /// </summary>
    /// <param name="filterParameters">Parameters to filter the query by</param>
    /// <param name="isFirstFilter">If the query already has a WHERE clause</param>
    public void BuildQuery(List<QueryFilterParameter> filterParameters, bool isFirstFilter) => AddFilterParameters(filterParameters, isFirstFilter);
    
    /// <summary>
    /// Build a query with order by parameters
    /// </summary>
    /// <param name="orderByParameters">Parameters to order the query by</param>
    public void BuildQuery(List<QueryOrderByParameter> orderByParameters) => AddOrderByParameters(orderByParameters);
    
    /// <summary>
    /// Build a query with filter and order by parameters
    /// </summary>
    /// <param name="filterParameters">Parameters to filter the query by</param>
    /// <param name="isFirstFilter">If the query already has a WHERE clause</param>
    /// <param name="orderByParameters">Parameters to order the query by</param>
    public void BuildQuery(List<QueryFilterParameter> filterParameters, bool isFirstFilter, List<QueryOrderByParameter> orderByParameters)
    {
        AddFilterParameters(filterParameters, isFirstFilter);
        AddOrderByParameters(orderByParameters);
    }
}
