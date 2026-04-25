using PlayLib.Data.Entities;

public class TabletopLibrary {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid TabletopId { get; set; }
    public int? TimesPlayed { get; set; }

    public User User { get; set; } = null!;
    public TabletopGame TabletopGame { get; set; } = null!;
}

public class VideogameLibrary {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid VideogameId { get; set; }
    public string State { get; set; } = null!;

    public User User { get; set; } = null!;
    public Videogame Videogame { get; set; } = null!;
}