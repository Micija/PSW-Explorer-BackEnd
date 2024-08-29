using PSW24.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Infrastructure.Database
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserInterest> UserInterests { get; set; }
        public DbSet<Interest> Interests { get; set; }

        public Context(DbContextOptions<Context> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("PSW24Schema");

            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

            Configure(modelBuilder);
        }

        private static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInterest>()
                .HasOne<User>()
                .WithMany(u => u.Interests)
                .HasForeignKey(ui => ui.UserId);

            modelBuilder.Entity<UserInterest>()
                .HasOne<Interest>()
                .WithMany(i => i.Users)
                .HasForeignKey(ui => ui.InterestId);
        }
    }
}
