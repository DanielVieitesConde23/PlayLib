using System.ComponentModel.DataAnnotations.Schema;

namespace PlayLib.Data.Entities;

public class FavouriteTabletop {
    [Column("User_Id")]
    public Guid UserId { get; set; }
    [Column("Tabletop_Id")]
    public Guid TabletopId { get; set; }

    public User User { get; set; } = null!;
    public TabletopGame Tabletop { get; set; } = null!;
}

public class FavouriteVideogame {
    public Guid UserId { get; set; }
    public Guid VideogameId { get; set; }

    public User User { get; set; } = null!;
    public Videogame Videogame { get; set; } = null!;
}