using Microsoft.EntityFrameworkCore;

namespace PlayLib.Data.Entities;

public class PlayLibDContext : DbContext {

    public PlayLibDContext(DbContextOptions<PlayLibDContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfig());

        base.OnModelCreating(builder);
    }
}
