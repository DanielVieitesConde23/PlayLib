using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlayLib.Data.Entities;

public class UserConfig : IEntityTypeConfiguration<User> {

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserName).HasMaxLength(30);
        builder.Property(x => x.Email).HasMaxLength(50);
    }
}
