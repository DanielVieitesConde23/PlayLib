using PlayLib.Data.Entities;

public class TabletopGame {
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Creator { get; set; } = null!;
    public string? ImageRoute { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string Format { get; set; } = null!;
    public int MinPlayerNumber { get; set; }
    public int MaxPlayerNumber { get; set; }
    public int AverageDuration { get; set; }

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<TabletopLibrary> Libraries { get; set; } = new List<TabletopLibrary>();
    public ICollection<FavouriteTabletop> Favourites { get; set; } = new List<FavouriteTabletop>();

    public ICollection<LanguageTabletop> Languages { get; set; } = new List<LanguageTabletop>();
    public ICollection<TagTabletop> Tags { get; set; } = new List<TagTabletop>();
}