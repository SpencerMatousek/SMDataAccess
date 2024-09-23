namespace SMDataAccess.Models.DataAccessModels;
public class ResponseMetadata 
{
    public ResponseMetadata(string? callName, string? onSuccessMessage, string? onFailureMessage)
    {
        CallName = callName;
        OnSuccessMessage = onSuccessMessage;
        OnFailureMessage = onFailureMessage;
    }
    public string? CallName { get; set; }
    public string? OnSuccessMessage { get; set; }
    public string? OnFailureMessage { get; set; }
}
