namespace PlayLib.Data.DTOs;

public class ReviewDTO {
    public Guid Id { get; set; }
    public string Username { get; set; }
    public Guid UserId { get; set; }
    public Guid GameId { get; set; }
    public DateTime? ReviewDate { get; set; }
    public decimal Rating { get; set; }
    public string? Content { get; set; }
}
