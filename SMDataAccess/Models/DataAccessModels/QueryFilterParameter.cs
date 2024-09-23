namespace SMDataAccess.Models.DataAccessModels;
public class QueryFilterParameter
{
    public QueryFilterParameter(string sqlFieldReference, string? parameterName, QueryParameterType type, object? value)
    {
        SqlFieldReference = sqlFieldReference;
        ParameterName = parameterName;
        Type = type;
        Value = value;
    }
    public enum QueryParameterType
    {
        EQUALS,
        LESS_THAN,
        GREATER_THAN,
        STARTS_WITH,
        ENDS_WITH,
        CONTAINS,
        IS_NULL,
        IS_NOT_NULL
    };
    public string SqlFieldReference { get; set; } = string.Empty;
    public string? ParameterName { get; set; } = string.Empty;
    public QueryParameterType Type { get; set; } = QueryParameterType.EQUALS;
    public object? Value { get; set; }
    public bool UsesValue
    {
        get
        {
            return Type switch
            {
                QueryParameterType.IS_NULL => false,
                QueryParameterType.IS_NOT_NULL => false,
                _ => true
            };
        }
    }
    public string ToString(bool isFirstFilter)
    {
        var clause = isFirstFilter ? "WHERE" : "AND";
        var conditional = Type switch
        {
            QueryParameterType.EQUALS => "=",
            QueryParameterType.GREATER_THAN => ">",
            QueryParameterType.LESS_THAN => "<",
            QueryParameterType.STARTS_WITH => "LIKE",
            QueryParameterType.CONTAINS => "LIKE",
            QueryParameterType.ENDS_WITH => "LIKE",
            QueryParameterType.IS_NULL => "IS NULL",
            QueryParameterType.IS_NOT_NULL => "IS NOT NULL",
            _ => throw new NotSupportedException()
        };

        var value = Type switch
        {
            QueryParameterType.STARTS_WITH => $"CONCAT(@{ParameterName}, '%')",
            QueryParameterType.CONTAINS => $"CONCAT('%', @{ParameterName}, '%')",
            QueryParameterType.ENDS_WITH => $"CONCAT(@{ParameterName}, '%')",
            QueryParameterType.IS_NULL => string.Empty,
            QueryParameterType.IS_NOT_NULL => string.Empty,
            _ => $"@{ParameterName}" // Equal, Greater than, Less than
        };

        return $"\n{clause} {SqlFieldReference} {conditional} {value}";
    }
}
