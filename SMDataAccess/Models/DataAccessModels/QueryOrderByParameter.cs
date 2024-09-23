using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDataAccess.Models.DataAccessModels;
public class QueryOrderByParameter(string sqlFieldReference, bool isAscending)
{
    public string SqlFieldReference { get; set; } = sqlFieldReference;
    public bool IsAscending { get; set; } = isAscending;
    public string ToString(bool isFirstOrderBy)
    {
        var clause = isFirstOrderBy ? "ORDER BY" : ",";
        var orderDirection = IsAscending ? "ASC" : "DESC";
        return $"{clause} {SqlFieldReference} {orderDirection}";
    }
}
