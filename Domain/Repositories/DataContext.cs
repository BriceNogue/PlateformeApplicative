﻿using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    public class DataContext : IdentityDbContext<Utilisateur, TypeE, int>
    {
        public DataContext() { }

        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-ALPHA;Database=ITInfrastructureDB;Trusted_Connection=true;TrustServerCertificate=true;")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        // Creation d'index et de contraintes
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Utilisateur>(utilisateur => {
                utilisateur.HasIndex(u => u.Email).IsUnique();
                utilisateur.HasIndex(u => u.Telephone).IsUnique();
            });*/

            modelBuilder.Entity<TypeE>(tp => {
                tp.HasIndex(t => t.Libelle).IsUnique();
            });

            modelBuilder.Entity<Utilisateur>()
                .HasOne(utilisateur => utilisateur.TypeE)
                .WithMany(typee => typee.Utilisateurs)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(utilisateur => utilisateur.IdType);

            modelBuilder.Entity<Etablissement>(etablissement => {
                etablissement.HasIndex(e => e.Nom).IsUnique();
                etablissement.HasIndex(e => e.Telephone).IsUnique();
                etablissement.HasIndex(e => e.Email).IsUnique();
            });

            // Classes par defaut de IdentityUser, pour specifier qu'elles n'ont pas de clé primaire
            modelBuilder.Entity<IdentityUserLogin<int>>().HasNoKey();
            modelBuilder.Entity<IdentityUserRole<int>>().HasNoKey();
            modelBuilder.Entity<IdentityUserToken<int>>().HasNoKey();


            modelBuilder.Entity<UtilisateurEtablissement>()
                .HasOne(utilisateurE => utilisateurE.Utilisateur)
                .WithMany(utilisateur => utilisateur.UtilisateurEtabs)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(utilisateurE => utilisateurE.IdUtilisateur);

            modelBuilder.Entity<UtilisateurEtablissement>()
                .HasOne(etabU => etabU.Etablissement)
                .WithMany(etab => etab.UtilisateurEtabs)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(etabU => etabU.IdEtablissement);

            modelBuilder.Entity<Salle>()
                .HasOne(salle => salle.TypeE)
                .WithMany(typee => typee.Salles)
                .HasForeignKey(salle => salle.IdType)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Salle>()
                .HasOne(salle => salle.Etablissement)
                .WithMany(etab => etab.Salles)
                .HasForeignKey(salle => salle.IdEtablissement)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Poste>()
                .HasOne(poste => poste.TypeE)
                .WithMany(typee => typee.Postes)
                .HasForeignKey(poste => poste.IdType)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Poste>()
                .HasOne(poste => poste.Salle)
                .WithMany(salle => salle.Postes)
                .HasForeignKey(poste => poste.IdSalle)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<TypeE> Types { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Etablissement> Etablissements { get; set; }
        public DbSet<UtilisateurEtablissement> UtilisateurEtablissements { get; set; }
        public DbSet<Salle> Salles { get; set; }
        public DbSet<Poste> Postes { get; set; }
        
    }
}
