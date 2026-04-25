namespace PlayLib.Data.DTOs;

public class ReviewDTO {
    public Guid Id { get; set; }
    public string Username { get; set; }
    public DateTime? ReviewDate { get; set; }
    public decimal Rating { get; set; }
    public string? Content { get; set; }

}
