﻿// <auto-generated />
using System;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Domain.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240315134822_Migration_15")]
    partial class Migration_15
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Etablissement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodePostal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LibelleRue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("NumeroRue")
                        .HasColumnType("int");

                    b.Property<string>("Pays")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Ville")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Nom")
                        .IsUnique();

                    b.HasIndex("Telephone")
                        .IsUnique();

                    b.ToTable("Etablissements");
                });

            modelBuilder.Entity("Domain.Entities.Poste", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdresseIP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdresseMAC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdSalle")
                        .HasColumnType("int");

                    b.Property<int>("IdType")
                        .HasColumnType("int");

                    b.Property<string>("Marque")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<double>("RAM")
                        .HasColumnType("float");

                    b.Property<double>("ROM")
                        .HasColumnType("float");

                    b.Property<string>("SE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Statut")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("IdSalle");

                    b.HasIndex("IdType");

                    b.ToTable("Postes");
                });

            modelBuilder.Entity("Domain.Entities.Salle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacite")
                        .HasColumnType("int");

                    b.Property<int>("IdEtablissement")
                        .HasColumnType("int");

                    b.Property<int>("IdType")
                        .HasColumnType("int");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("IdEtablissement");

                    b.HasIndex("IdType");

                    b.ToTable("Salles");
                });

            modelBuilder.Entity("Domain.Entities.TypeE", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Objet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Libelle")
                        .IsUnique();

                    b.ToTable("Types");
                });

            modelBuilder.Entity("Domain.Entities.Utilisateur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateInscription")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateNaissance")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("IdType")
                        .HasColumnType("int");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("IdType");

                    b.HasIndex("Telephone")
                        .IsUnique();

                    b.ToTable("Utilisateurs");
                });

            modelBuilder.Entity("Domain.Entities.UtilisateurEtablissement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<int>("EtablissementId")
                        .HasColumnType("int");

                    b.Property<int>("IdEtablissement")
                        .HasColumnType("int");

                    b.Property<int>("IdUtilisateur")
                        .HasColumnType("int");

                    b.Property<int>("UtilisateurId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EtablissementId");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("UtilisateurEtablissements");
                });

            modelBuilder.Entity("Domain.Entities.Poste", b =>
                {
                    b.HasOne("Domain.Entities.Salle", "Salle")
                        .WithMany("Postes")
                        .HasForeignKey("IdSalle")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.TypeE", "TypeE")
                        .WithMany("Postes")
                        .HasForeignKey("IdType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Salle");

                    b.Navigation("TypeE");
                });

            modelBuilder.Entity("Domain.Entities.Salle", b =>
                {
                    b.HasOne("Domain.Entities.Etablissement", "Etablissement")
                        .WithMany("Salles")
                        .HasForeignKey("IdEtablissement")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.TypeE", "TypeE")
                        .WithMany("Salles")
                        .HasForeignKey("IdType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Etablissement");

                    b.Navigation("TypeE");
                });

            modelBuilder.Entity("Domain.Entities.Utilisateur", b =>
                {
                    b.HasOne("Domain.Entities.TypeE", "TypeE")
                        .WithMany("Utilisateurs")
                        .HasForeignKey("IdType")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TypeE");
                });

            modelBuilder.Entity("Domain.Entities.UtilisateurEtablissement", b =>
                {
                    b.HasOne("Domain.Entities.Etablissement", "Etablissement")
                        .WithMany()
                        .HasForeignKey("EtablissementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Utilisateur", "Utilisateur")
                        .WithMany()
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Etablissement");

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("Domain.Entities.Etablissement", b =>
                {
                    b.Navigation("Salles");
                });

            modelBuilder.Entity("Domain.Entities.Salle", b =>
                {
                    b.Navigation("Postes");
                });

            modelBuilder.Entity("Domain.Entities.TypeE", b =>
                {
                    b.Navigation("Postes");

                    b.Navigation("Salles");

                    b.Navigation("Utilisateurs");
                });
#pragma warning restore 612, 618
        }
    }
}
