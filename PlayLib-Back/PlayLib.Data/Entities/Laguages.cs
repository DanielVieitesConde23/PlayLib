namespace PlayLib.Data.Entities;

public class Language {
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<LanguageTabletop> TabletopGames { get; set; } = new List<LanguageTabletop>();
    public ICollection<LanguageVideogame> Videogames { get; set; } = new List<LanguageVideogame>();
}

public class LanguageTabletop {
    public Guid LanguageId { get; set; }
    public Guid TabletopId { get; set; }

    public Language Language { get; set; } = null!;
    public TabletopGame TabletopGame { get; set; } = null!;
}

public class LanguageVideogame {
    public Guid LanguageId { get; set; }
    public Guid VideogameId { get; set; }

    public Language Language { get; set; } = null!;
    public Videogame Videogame { get; set; } = null!;
}