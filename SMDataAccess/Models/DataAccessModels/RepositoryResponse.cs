using System.Data.SqlClient;

namespace SMDataAccess.Models.DataAccessModels;
public class RepositoryResponse<T>
{
    public T? Data { get; set; }
    public PaginationInfo? PaginationInfo { get; set; }
    public List<string>? Errors;
    public ResponseMetadata? ResponseMetadata { get; set; }
    private SqlException? sqlException;
    public SqlException? SqlException
    {
        get
        {
            return sqlException;
        }
        set
        {
            sqlException = value;
            if (value != null)
            {
                var foundError = ExceptionHandler.SqlMessages.TryGetValue(value.Number, out var errorBuilder);
                if (foundError)
                {
                    Errors ??= [];
                    Errors.Add(errorBuilder?.Invoke(value.Message) ?? string.Empty);
                }
            }
        }
    }
    public string ErrorMessage
    {
        get
        {
            if (SqlException == null)
                return string.Empty;
            else if ((Errors == null || Errors?.Count < 1) && SqlException != null)
                return $"Error:\n{SqlException.Message}\nError# {SqlException.Number}";
            else if (Errors != null)
                return string.Join("\n", Errors);
            else return "Something went wrong";
        }
    }
    public bool IsSuccessful { get => SqlException == null; }
    public bool IsSuccessfulWithData { get => SqlException != null && Data != null; }
}
