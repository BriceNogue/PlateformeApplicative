﻿// <auto-generated />
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
    [Migration("20240203120353_Migration_9")]
    partial class Migration_9
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

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodePostal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Pays")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ville")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Nom")
                        .IsUnique();

                    b.ToTable("Etablissements");
                });

            modelBuilder.Entity("Domain.Entities.Poste", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdresseIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdresseMAC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdSalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdType")
                        .HasColumnType("int");

                    b.Property<string>("LibellePoste")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Marque")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Statut")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("IdType");

                    b.ToTable("Postes");
                });

            modelBuilder.Entity("Domain.Entities.Salle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Capacite")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Emplacement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdEtablissement")
                        .HasColumnType("int");

                    b.Property<int>("IdType")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ObjetConcerne")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Nom")
                        .IsUnique();

                    b.ToTable("Types");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdType")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("IdType");

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.Poste", b =>
                {
                    b.HasOne("Domain.Entities.TypeE", "TypeE")
                        .WithMany("Postes")
                        .HasForeignKey("IdType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.HasOne("Domain.Entities.TypeE", "TypeE")
                        .WithMany("Users")
                        .HasForeignKey("IdType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeE");
                });

            modelBuilder.Entity("Domain.Entities.Etablissement", b =>
                {
                    b.Navigation("Salles");
                });

            modelBuilder.Entity("Domain.Entities.TypeE", b =>
                {
                    b.Navigation("Postes");

                    b.Navigation("Salles");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
