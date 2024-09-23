using System.Text.RegularExpressions;

namespace SMDataAccess;
public static class ExceptionHandler
{
    public static Dictionary<int, Func<string, string>> SqlMessages = new()
    {
        { 2601, (string message) =>
            {
                Match match = Regex.Match(message, @"\((.*?)\)");
                return $"Cannot insert because record ({match.Groups[1].Value}) already exists".Replace("<NULL>", "Null");
            }
        }
    };
}
