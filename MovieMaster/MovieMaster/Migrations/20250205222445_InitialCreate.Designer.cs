﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieMaster.Data;

#nullable disable

namespace MovieMaster.Migrations
{
    [DbContext(typeof(MovieDbContext))]
    [Migration("20250205222445_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Actor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name_Actor")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("ActorMovie", b =>
                {
                    b.Property<int>("Actor_ID")
                        .HasColumnType("int");

                    b.Property<int>("Movie_ID")
                        .HasColumnType("int");

                    b.HasKey("Actor_ID", "Movie_ID");

                    b.HasIndex("Movie_ID");

                    b.ToTable("ActorsMovies");
                });

            modelBuilder.Entity("Director", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name_Director")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.ToTable("Directors");
                });

            modelBuilder.Entity("DirectorMovie", b =>
                {
                    b.Property<int>("Director_ID")
                        .HasColumnType("int");

                    b.Property<int>("Movie_ID")
                        .HasColumnType("int");

                    b.HasKey("Director_ID", "Movie_ID");

                    b.HasIndex("Movie_ID");

                    b.ToTable("DirectorsMovies");
                });

            modelBuilder.Entity("MovieMaster.Models.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("Created_At");

                    b.Property<int>("Movie_ID")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("User_ID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Movie_ID");

                    b.HasIndex("User_ID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("MovieMaster.Models.Genre", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Genre_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.ToTable("Genres", (string)null);
                });

            modelBuilder.Entity("MovieMaster.Models.Movie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Genre_ID")
                        .HasColumnType("int");

                    b.Property<int>("Release_Year")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Txt_Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.HasIndex("Genre_ID");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieMaster.Models.Rating", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Movie_ID")
                        .HasColumnType("int");

                    b.Property<int>("RatingValue")
                        .HasColumnType("int");

                    b.Property<int>("User_ID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("MovieMaster.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name_User")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Occupation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("User_Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ActorMovie", b =>
                {
                    b.HasOne("Actor", "Actor")
                        .WithMany("ActorsMovies")
                        .HasForeignKey("Actor_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieMaster.Models.Movie", "Movie")
                        .WithMany("ActorsMovies")
                        .HasForeignKey("Movie_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("DirectorMovie", b =>
                {
                    b.HasOne("Director", "Director")
                        .WithMany("DirectorsMovies")
                        .HasForeignKey("Director_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieMaster.Models.Movie", "Movie")
                        .WithMany("DirectorsMovies")
                        .HasForeignKey("Movie_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Director");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieMaster.Models.Comment", b =>
                {
                    b.HasOne("MovieMaster.Models.Movie", "Movie")
                        .WithMany("Comments")
                        .HasForeignKey("Movie_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieMaster.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MovieMaster.Models.Movie", b =>
                {
                    b.HasOne("MovieMaster.Models.Genre", "GenreInfo")
                        .WithMany("Movies")
                        .HasForeignKey("Genre_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GenreInfo");
                });

            modelBuilder.Entity("Actor", b =>
                {
                    b.Navigation("ActorsMovies");
                });

            modelBuilder.Entity("Director", b =>
                {
                    b.Navigation("DirectorsMovies");
                });

            modelBuilder.Entity("MovieMaster.Models.Genre", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("MovieMaster.Models.Movie", b =>
                {
                    b.Navigation("ActorsMovies");

                    b.Navigation("Comments");

                    b.Navigation("DirectorsMovies");
                });
#pragma warning restore 612, 618
        }
    }
}
