using PlayLib.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class TabletopLibrary {
    public Guid Id { get; set; }
    [Column("User_Id")]
    public Guid UserId { get; set; }
    [Column("Tabletop_Id")]
    public Guid TabletopId { get; set; }
    [Column("Times_Played")]
    public int? TimesPlayed { get; set; }

    public User User { get; set; } = null!;
    public TabletopGame Tabletop { get; set; } = null!;
}

public class VideogameLibrary {
    public Guid Id { get; set; }
    [Column("User_Id")]
    public Guid UserId { get; set; }
    [Column("Videogame_Id")]
    public Guid VideogameId { get; set; }
    public string State { get; set; } = null!;
    public string? Format { get; set; }

    public User User { get; set; } = null!;
    public Videogame Videogame { get; set; } = null!;
}