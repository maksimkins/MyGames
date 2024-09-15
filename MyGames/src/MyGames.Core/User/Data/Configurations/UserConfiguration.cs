using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyGames.Core.User.Data.Configurations;

using MyGames.Core.User.Models;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(c => c.Birthdate)
            .IsRequired();

        builder
            .Property(c => c.IsMuted)
            .IsRequired();

        builder
            .Property(c => c.IsBanned)
            .IsRequired();
    }
}
