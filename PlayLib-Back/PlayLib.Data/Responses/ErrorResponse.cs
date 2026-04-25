namespace PlayLib.Data.Responses;
public class ErrorResponse {
    public IEnumerable<string> ErrorMessages { get; set; }

    public ErrorResponse(string errorResponse) : this(new List<string> { errorResponse })
    {

    }

    public ErrorResponse(IEnumerable<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}
