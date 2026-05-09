using PlayLib.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class TabletopGame {
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Creator { get; set; } = null!;
    [Column("Image_Route")]
    public string? ImageRoute { get; set; }
    [Column("Release_Date")]
    public DateTime? ReleaseDate { get; set; }
    [Column("Min_Player_Number")]
    public int MinPlayerNumber { get; set; }
    [Column("Max_Player_Number")]
    public int MaxPlayerNumber { get; set; }
    [Column("Average_Duration")]
    public int AverageDuration { get; set; }

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<TabletopLibrary> Libraries { get; set; } = new List<TabletopLibrary>();
    public ICollection<FavouriteTabletop> Favourites { get; set; } = new List<FavouriteTabletop>();

    public ICollection<LanguageTabletop> Languages { get; set; } = new List<LanguageTabletop>();
    public ICollection<TagTabletop> Tags { get; set; } = new List<TagTabletop>();
}