using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMessenger.DataLayer.Entities;

namespace TextMessenger.DataAccessLayer.DbConfigurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> entity)
        {
            entity.ToTable("messages");

            entity.HasKey(m => m.MessageId);

            entity.HasIndex(m => new { m.ChatId, m.MessageId });

            entity.Property(m => m.MessageId).ValueGeneratedOnAdd();
            entity.Property(m => m.Text).HasMaxLength(200).IsRequired();
            entity.Property(m => m.IsSystem).HasDefaultValue(false);
            entity.Property(m => m.IsDeleted).HasDefaultValue(false);

            entity
                .HasOne(m => m.Chat)
                .WithMany(c => c.ChatMessages)
                .HasForeignKey(m => m.ChatId)
                .HasPrincipalKey(c => c.ChatId);

            entity
                .HasOne(m => m.MessageCreator)
                .WithMany(mc => mc.Messages)
                .HasForeignKey(m => m.MessageCreatorId)
                .HasPrincipalKey(mc => mc.UserId);

        }
    }
}
