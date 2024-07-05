using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGames.Models;

namespace MyGames.Repositories.EF_Core.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Text)
                .IsRequired();

            builder
                .Property(c => c.Title)
                .IsRequired();

            builder
                .Property(c => c.Rate)
                .IsRequired();

            builder
                .Property(c => c.GameId)
                .IsRequired();
        }
    }
}