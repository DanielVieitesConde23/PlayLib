using System.ComponentModel.DataAnnotations.Schema;

namespace PlayLib.Data.Entities;

public class Language {
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<LanguageTabletop> TabletopGames { get; set; } = new List<LanguageTabletop>();
    public ICollection<LanguageVideogame> Videogames { get; set; } = new List<LanguageVideogame>();
}

public class LanguageTabletop {
    [Column("Language_Id")]
    public Guid LanguageId { get; set; }
    [Column("Tabletop_Id")]
    public Guid TabletopId { get; set; }

    public Language Language { get; set; } = null!;
    public TabletopGame Tabletop { get; set; } = null!;
}

public class LanguageVideogame {
    [Column("Language_Id")]
    public Guid LanguageId { get; set; }
    [Column("Videogame_Id")]
    public Guid VideogameId { get; set; }

    public Language Language { get; set; } = null!;
    public Videogame Videogame { get; set; } = null!;
}