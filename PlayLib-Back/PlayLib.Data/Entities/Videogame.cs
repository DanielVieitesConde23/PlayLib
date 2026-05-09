using PlayLib.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class Videogame {
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Developer { get; set; } = null!;
    [Column("Image_Route")]
    public string? ImageRoute { get; set; }
    [Column("Release_Date")]
    public DateTime? ReleaseDate { get; set; }

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<VideogameLibrary> Libraries { get; set; } = new List<VideogameLibrary>();
    public ICollection<FavouriteVideogame> Favourites { get; set; } = new List<FavouriteVideogame>();

    public ICollection<LanguageVideogame> Languages { get; set; } = new List<LanguageVideogame>();
    public ICollection<TagVideogame> Tags { get; set; } = new List<TagVideogame>();
}