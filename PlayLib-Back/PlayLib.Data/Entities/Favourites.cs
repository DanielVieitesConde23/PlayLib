namespace PlayLib.Data.Entities;

public class FavouriteTabletop {
    public Guid UserId { get; set; }
    public Guid TabletopId { get; set; }

    public User User { get; set; } = null!;
    public TabletopGame TabletopGame { get; set; } = null!;
}

public class FavouriteVideogame {
    public Guid UserId { get; set; }
    public Guid VideogameId { get; set; }

    public User User { get; set; } = null!;
    public Videogame Videogame { get; set; } = null!;
}