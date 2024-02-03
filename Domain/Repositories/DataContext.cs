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

        // Creation d'index et de contraintes
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user => {
                user.HasIndex(u => u.Email).IsUnique();
                user.HasIndex(u => u.PhoneNumber).IsUnique();
            });

            modelBuilder.Entity<TypeE>(tp => {
                tp.HasIndex(t => t.Nom).IsUnique();
            });

            modelBuilder.Entity<Etablissement>(etablissement => {
                etablissement.HasIndex(e => e.Nom).IsUnique();
            });

            modelBuilder.Entity<User>()
                .HasOne(user => user.TypeE)
                .WithMany(typee => typee.Users)
                .HasForeignKey(user => user.IdType);

            modelBuilder.Entity<Salle>()
                .HasOne(salle => salle.TypeE)
                .WithMany(typee => typee.Salles)
                .HasForeignKey(salle => salle.IdType);

            modelBuilder.Entity<Salle>()
                .HasOne(salle => salle.Etablissement)
                .WithMany(etab => etab.Salles)
                .HasForeignKey(salle => salle.IdEtablissement);

            modelBuilder.Entity<Poste>()
                .HasOne(poste => poste.TypeE)
                .WithMany(typee => typee.Postes)
                .HasForeignKey(poste => poste.IdType);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TypeE> Types { get; set; }
        public DbSet<Salle> Salles { get; set; }
        public DbSet<Poste> Postes { get; set; }
        public DbSet<Etablissement> Etablissements { get; set; }

    }
}
