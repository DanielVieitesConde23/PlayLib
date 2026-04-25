using Azure.Core;

namespace PlayLib.Data.Entities;

public class User {
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Request> Requests { get; set; } = new List<Request>();

    public ICollection<FavouriteTabletop> FavouriteTabletops { get; set; } = new List<FavouriteTabletop>();
    public ICollection<FavouriteVideogame> FavouriteVideogames { get; set; } = new List<FavouriteVideogame>();

    public ICollection<TabletopLibrary> TabletopLibrary { get; set; } = new List<TabletopLibrary>();
    public ICollection<VideogameLibrary> VideogameLibrary { get; set; } = new List<VideogameLibrary>();
}
