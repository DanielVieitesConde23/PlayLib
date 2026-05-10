using System.ComponentModel.DataAnnotations.Schema;
using PlayLib.Data.Entities;

public class Review {
    public Guid Id { get; set; }
    [Column("user_id")]
    public Guid UserId { get; set; }
    [Column("videogame_id")]
    public Guid? VideogameId { get; set; }
    [Column("tabletop_game_id")]
    public Guid? TabletopGameId { get; set; }
    [Column("review_date")]
    public DateTime? ReviewDate { get; set; }
    public decimal Rating { get; set; }
    public string? Content { get; set; }
    public bool? Reported { get; set; }

    public User User { get; set; } = null!;
    public Videogame? Videogame { get; set; }
    public TabletopGame? Tabletop { get; set; }
}