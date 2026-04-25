using Microsoft.EntityFrameworkCore;

namespace PlayLib.Data.Entities;

public class PlayLibDContext : DbContext {

    public PlayLibDContext(DbContextOptions<PlayLibDContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<Videogame> Videogames { get; set; }

    public DbSet<TabletopGame> TabletopGames { get; set; }

    public DbSet<VideogameLibrary> VideogameLibraries { get; set; }

    public DbSet<TabletopLibrary> TabletopLibraries { get; set; }

    public DbSet<Review> Reviews { get; set; }

    public DbSet<FavouriteTabletop> FavouriteTabletops { get; set; }

    public DbSet<FavouriteVideogame> FavouriteVideogames { get; set; }

    public DbSet<Language> Languages { get; set; }

    public DbSet<LanguageTabletop> LanguageTables { get; set; }

    public DbSet<LanguageVideogame> LanguageVideogames { get; set; }

    public DbSet<Request> Requests { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<TagTabletop> TagsTabletop { get; set; }

    public DbSet<TagVideogame> TagsVideogames { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<FavouriteTabletop>()
            .HasKey(f => new { f.UserId, f.TabletopId });

        builder.Entity<FavouriteVideogame>()
            .HasKey(f => new { f.UserId, f.VideogameId });

        builder.Entity<LanguageTabletop>()
            .HasKey(l => new { l.LanguageId, l.TabletopId });

        builder.Entity<LanguageVideogame>()
            .HasKey(l => new { l.LanguageId, l.VideogameId });

        builder.Entity<TagTabletop>()
            .HasKey(t => new { t.TagId, t.TabletopId });

        builder.Entity<TagVideogame>()
            .HasKey(t => new { t.TagId, t.VideogameId });

        builder.Entity<Review>()
            .ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Reviews_OnlyOneTarget",
                    "(videogame_id IS NOT NULL AND tabletop_game_id IS NULL) OR (videogame_id IS NULL AND tabletop_game_id IS NOT NULL)"
                );

                t.HasCheckConstraint(
                    "CK_Reviews_Rating",
                    "rating >= 0 AND rating <= 5"
                );
            });

        base.OnModelCreating(builder);
    }
}
