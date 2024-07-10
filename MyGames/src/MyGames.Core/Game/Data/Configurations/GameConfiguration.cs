using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyGames.Core.Game.Data.Configurations;

using MyGames.Core.Game.Models;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {

        builder
            .HasKey(g => g.Id);

        builder
            .Property(g => g.Name)
            .IsRequired();

        builder
            .Property(g => g.Price)
            .IsRequired();

        builder
            .Property(g => g.Rate)
            .HasDefaultValue(0)
            .IsRequired();
    }
}
