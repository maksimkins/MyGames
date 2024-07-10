using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyGames.Core.UserGame.Data.Configurations;

using MyGames.Core.UserGame.Models;

public class UserGameConfiguration : IEntityTypeConfiguration<UserGame>
{
    public void Configure(EntityTypeBuilder<UserGame> builder)
    {
        builder
            .HasKey(ug => ug.Id);

        builder
            .HasOne(ug => ug.Game)
            .WithMany(ug => ug.Users)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(ug => ug.User)
            .WithMany(ug => ug.Games)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasIndex(ug => new {ug.UserId , ug.GameId})
            .IsUnique();
    }
}
