using PlayLib.Data.Entities;

public class Review {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid? VideogameId { get; set; }
    public Guid? TabletopGameId { get; set; }
    public DateTime? ReviewDate { get; set; }
    public decimal Rating { get; set; }
    public string? Content { get; set; }
    public bool? Reported { get; set; }

    public User User { get; set; } = null!;
    public Videogame? Videogame { get; set; }
    public TabletopGame? TabletopGame { get; set; }
}