using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMessenger.DataLayer.Entities;

namespace TextMessenger.DataAccessLayer.DbConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("users");

            entity.HasKey(u => u.UserId);

            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.Nickname).IsUnique();

            entity.Property(u => u.Email).HasMaxLength(40).IsRequired();
            entity.Property(u => u.Nickname).HasMaxLength(16).IsRequired();
            entity.Property(u => u.Password).HasMaxLength(150).IsRequired();

            entity
                .HasMany(u => u.Friends)
                .WithMany(f => f.FriendsNavigation)
                .UsingEntity(u => u.ToTable("friends"));

            entity
                .HasMany(u=>u.FriendshipSenders)
                .WithMany(fs=>fs.FriendshipSendersNavigation)
                .UsingEntity(u => u.ToTable("friendshipSenders"));
        }
    }
}
