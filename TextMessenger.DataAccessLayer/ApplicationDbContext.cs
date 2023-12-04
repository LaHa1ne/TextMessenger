using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMessenger.DataAccessLayer.DbConfigurations;
using TextMessenger.DataLayer.Entities;

namespace TextMessenger.DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<User> Users{ get; set; } = null!;        
        public virtual DbSet<Chat> Chats { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());

        }

    }
}
