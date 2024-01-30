using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-ALPHA;Database=PlateformeApplicative;Trusted_Connection=true;TrustServerCertificate=true;")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user => {
                user.HasIndex(u => u.Email).IsUnique();
                user.HasIndex(u => u.PhoneNumber).IsUnique();
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Etablissement> Etablissements { get; set; } 
        public DbSet<TypeE> Types { get; set; } 

    }
}
