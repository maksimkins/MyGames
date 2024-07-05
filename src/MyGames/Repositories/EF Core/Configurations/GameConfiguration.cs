using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGames.Models;

namespace MyGames.Repositories.EF_Core.Configurations
{
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
}