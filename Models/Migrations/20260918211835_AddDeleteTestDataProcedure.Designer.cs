using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelApp.Models;

#nullable disable

namespace Models.Migrations
{
    [DbContext(typeof(TravelAppDbContext))]
    [Migration("20260918211835_AddDeleteTestDataProcedure")]
    partial class AddDeleteTestDataProcedure
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "10.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TravelApp.Models.Anvandare", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Epost")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Namn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Seeded")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Epost")
                        .IsUnique();

                    b.ToTable("Anvandare");
                });

            modelBuilder.Entity("TravelApp.Models.Kommentar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnvandareId")
                        .HasColumnType("int");

                    b.Property<int>("SevardhetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Skapad")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnvandareId");

                    b.HasIndex("SevardhetId");

                    b.ToTable("Kommentar");
                });

            modelBuilder.Entity("TravelApp.Models.Land", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Namn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Seeded")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Land");
                });

            modelBuilder.Entity("TravelApp.Models.Ort", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LandId")
                        .HasColumnType("int");

                    b.Property<string>("Namn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LandId");

                    b.ToTable("Ort");
                });

            modelBuilder.Entity("TravelApp.Models.Sevardhet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Beskrivning")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Kategori")
                        .HasColumnType("int");

                    b.Property<int>("OrtId")
                        .HasColumnType("int");

                    b.Property<string>("Rubrik")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Kategori");

                    b.HasIndex("OrtId");

                    b.ToTable("Sevardhet");
                });

            modelBuilder.Entity("TravelApp.Models.Kommentar", b =>
                {
                    b.HasOne("TravelApp.Models.Anvandare", "Anvandare")
                        .WithMany("Kommentarer")
                        .HasForeignKey("AnvandareId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TravelApp.Models.Sevardhet", "Sevardhet")
                        .WithMany("Kommentarer")
                        .HasForeignKey("SevardhetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anvandare");

                    b.Navigation("Sevardhet");
                });

            modelBuilder.Entity("TravelApp.Models.Ort", b =>
                {
                    b.HasOne("TravelApp.Models.Land", "Land")
                        .WithMany("Orter")
                        .HasForeignKey("LandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Land");
                });

            modelBuilder.Entity("TravelApp.Models.Sevardhet", b =>
                {
                    b.HasOne("TravelApp.Models.Ort", "Ort")
                        .WithMany("Sevardheter")
                        .HasForeignKey("OrtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ort");
                });

            modelBuilder.Entity("TravelApp.Models.Anvandare", b =>
                {
                    b.Navigation("Kommentarer");
                });

            modelBuilder.Entity("TravelApp.Models.Land", b =>
                {
                    b.Navigation("Orter");
                });

            modelBuilder.Entity("TravelApp.Models.Ort", b =>
                {
                    b.Navigation("Sevardheter");
                });

            modelBuilder.Entity("TravelApp.Models.Sevardhet", b =>
                {
                    b.Navigation("Kommentarer");
                });
#pragma warning restore 612, 618
        }
    }
}
