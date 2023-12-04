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
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> entity)
        {
            entity.ToTable("chats");

            entity.HasKey(c => c.ChatId);

            entity.Property(c => c.ChatId).ValueGeneratedOnAdd();
            entity.Property(c => c.Name).HasMaxLength(50).IsRequired();
            entity.Property(c=>c.ChatCreatorId).IsRequired();

            entity
                .HasMany(c => c.ChatMembers)
                .WithMany(u => u.Chats)
                .UsingEntity(c => c.ToTable("chatMembers"));

            entity
                .HasOne(c => c.ChatCreator)
                .WithMany(u => u.CreatedChats)
                .HasForeignKey(c => c.ChatCreatorId)
                .HasPrincipalKey(u => u.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
