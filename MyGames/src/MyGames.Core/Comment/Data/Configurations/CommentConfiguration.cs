using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyGames.Core.Comment.Data.Configurations;

using MyGames.Core.Comment.Models;

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
            .Property(c => c.Rate)
            .IsRequired();

        builder
            .Property(c => c.GameId)
            .IsRequired();
        
        builder
            .Property(c => c.UserId)
            .IsRequired();
    }
}
