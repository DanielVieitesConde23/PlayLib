using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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
        builder.Entity<Videogame>().ToTable("Videogames");

        builder.Entity<TabletopGame>().ToTable("Tabletop_Games");

        builder.Entity<VideogameLibrary>()
            .ToTable("Videogame_Library")
            .HasKey(l => new { l.UserId, l.VideogameId });

        builder.Entity<TabletopLibrary>()
            .ToTable("Tabletop_Library")
            .HasKey(l => new { l.UserId, l.TabletopId });

        builder.Entity<FavouriteTabletop>()
            .ToTable("Favourites_Tabletop")
            .HasKey(f => new { f.UserId, f.TabletopId });

        builder.Entity<FavouriteVideogame>()
            .ToTable("Favourites_Videogame")
            .HasKey(f => new { f.UserId, f.VideogameId });

        builder.Entity<LanguageTabletop>()
            .ToTable("Language_Tabletop")
            .HasKey(l => new { l.LanguageId, l.TabletopId });

        builder.Entity<LanguageVideogame>()
            .ToTable("Language_Videogames")
            .HasKey(l => new { l.LanguageId, l.VideogameId });

        builder.Entity<TagTabletop>()
            .ToTable("Tags_Tabletop")
            .HasKey(t => new { t.TagId, t.TabletopId });

        builder.Entity<TagVideogame>()
            .ToTable("Tags_Videogames")
            .HasKey(t => new { t.TagId, t.VideogameId });

        builder.Entity<Review>(entity =>
        {
            entity.ToTable("Reviews");

            entity.Property(r => r.TabletopGameId)
                .HasColumnName("tabletop_game_id");

            entity.Property(r => r.VideogameId)
                .HasColumnName("videogame_id");

            entity.HasOne(r => r.TabletopGame)
                .WithMany(t => t.Reviews)
                .HasForeignKey(r => r.TabletopGameId)
                .HasPrincipalKey(t => t.Id)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.Videogame)
                .WithMany(v => v.Reviews)
                .HasForeignKey(r => r.VideogameId)
                .HasPrincipalKey(v => v.Id)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Request>()
            .ToTable("Requests");

        builder.Entity<Tag>()
            .ToTable("Tags");

        foreach (var entity in builder.Model.GetEntityTypes())
        {
            Console.WriteLine($"ENTITY: {entity.Name}");

            foreach (var property in entity.GetProperties())
            {
                Console.WriteLine($"  PROPERTY: {property.Name} -> COLUMN: {property.GetColumnName()}");
            }
        }

        base.OnModelCreating(builder);
    }
}
