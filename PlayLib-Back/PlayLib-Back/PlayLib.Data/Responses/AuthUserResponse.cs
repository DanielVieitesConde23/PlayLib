namespace PlayLib.Data.Responses;
public class AuthUserResponse {
    public string AccessToken { get; set; }
    public Guid UserId { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
}
