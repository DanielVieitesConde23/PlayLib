using System.ComponentModel.DataAnnotations.Schema;

namespace PlayLib.Data.Entities;

public class Tag {
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Hex { get; set; } = null!;

    public ICollection<TagTabletop> TabletopGames { get; set; } = new List<TagTabletop>();
    public ICollection<TagVideogame> Videogames { get; set; } = new List<TagVideogame>();
}

public class TagTabletop {
    [Column("Tag_Id")]
    public Guid TagId { get; set; }
    [Column("Tabletop_Id")]
    public Guid TabletopId { get; set; }

    public Tag Tag { get; set; } = null!;
    public TabletopGame Tabletop { get; set; } = null!;
}

public class TagVideogame {

    [Column("Tag_Id")]
    public Guid TagId { get; set; }

    [Column("Videogame_Id")]
    public Guid VideogameId { get; set; }

    public Tag Tag { get; set; } = null!;
    public Videogame Videogame { get; set; } = null!;
}