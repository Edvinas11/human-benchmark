﻿// <auto-generated />
using AimReactionAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AimReactionAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240926104527_AddGameAndTargetModelsNoNav")]
    partial class AddGameAndTargetModelsNoNav
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AimReactionAPI.Models.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GameId"));

                    b.Property<string>("DifficultyLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GameDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GameDuration")
                        .HasColumnType("int");

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxTargets")
                        .HasColumnType("int");

                    b.Property<int>("TargetSpeed")
                        .HasColumnType("int");

                    b.HasKey("GameId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Target", b =>
                {
                    b.Property<int>("TargetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TargetId"));

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<int>("Speed")
                        .HasColumnType("int");

                    b.Property<int>("X")
                        .HasColumnType("int");

                    b.Property<int>("Y")
                        .HasColumnType("int");

                    b.HasKey("TargetId");

                    b.HasIndex("GameId");

                    b.ToTable("Targets");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Target", b =>
                {
                    b.HasOne("AimReactionAPI.Models.Game", null)
                        .WithMany("Targets")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AimReactionAPI.Models.Game", b =>
                {
                    b.Navigation("Targets");
                });
#pragma warning restore 612, 618
        }
    }
}
