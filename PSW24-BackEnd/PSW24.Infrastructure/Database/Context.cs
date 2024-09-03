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
        public DbSet<Interest> Interests { get; set; }
        public DbSet<UserInterest> UserInterests { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<KeyPoint> KeyPoints{ get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ProblemLogger> ProblemLoggers { get; set; }
        public Context(DbContextOptions<Context> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("PSW24Schema");

            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Interest>().HasIndex(i => i.Type).IsUnique();


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

            modelBuilder.Entity<Tour>()
                 .HasOne<Interest>(t => t.Interest)
                 .WithMany(i => i.Tours)
                 .HasForeignKey(t => t.InterestId);

            modelBuilder.Entity<Tour>()
                 .HasOne<User>(t => t.Author)
                 .WithMany(u => u.Tours)
                 .HasForeignKey(t => t.AuthorId);

            modelBuilder.Entity<KeyPoint>()
                 .HasOne<Tour>(k => k.Tour)
                 .WithMany(t => t.KeyPoints)
                 .HasForeignKey(k => k.TourId);

            modelBuilder.Entity<Cart>()
                 .HasOne<Tour>(k => k.Tour)
                 .WithMany()
                 .HasForeignKey(k => k.TourId);

            modelBuilder.Entity<Cart>()
                 .HasOne<User>(k => k.Buyer)
                 .WithMany(u => u.Carts)
                 .HasForeignKey(k => k.BuyerId);

            modelBuilder.Entity<Problem>()
                 .HasOne<User>(k => k.User)
                 .WithMany(u => u.Problems)
                 .HasForeignKey(k => k.UserId);

            modelBuilder.Entity<Problem>()
                 .HasOne<Tour>(k => k.Tour)
                 .WithMany()
                 .HasForeignKey(k => k.TourId);

            modelBuilder.Entity<Report>()
                 .HasOne<User>(r => r.User)
                 .WithMany(u => u.Reports)
                 .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<ProblemLogger>()
                .HasOne<Problem>(r => r.Problem)
                .WithMany(u => u.Loggers)
                .HasForeignKey(r => r.ProblemId);
        }
    }
}
