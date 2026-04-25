namespace PlayLib.Data.Responses;

public class AuthConfiguration {
    public string Key { get; set; }
    public int ExpirationMinutes { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string RefreshTokenKey { get; set; }
    public int RefreshTokenExpirationMinutes { get; set; }
}
