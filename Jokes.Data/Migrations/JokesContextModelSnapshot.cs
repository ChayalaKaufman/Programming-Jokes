﻿// <auto-generated />
using System;
using Jokes.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Jokes.Data.Migrations
{
    [DbContext(typeof(JokesContext))]
    partial class JokesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Jokes.Data.Joke", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OriginalId");

                    b.Property<string>("Punchline")
                        .IsRequired();

                    b.Property<string>("SetUp")
                        .IsRequired();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Jokes");
                });

            modelBuilder.Entity("Jokes.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Jokes.Data.UserLikedJoke", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("JokeId");

                    b.Property<DateTime>("DateLiked");

                    b.Property<bool>("Liked");

                    b.HasKey("UserId", "JokeId");

                    b.HasIndex("JokeId");

                    b.ToTable("UserLikedJokes");
                });

            modelBuilder.Entity("Jokes.Data.UserLikedJoke", b =>
                {
                    b.HasOne("Jokes.Data.Joke", "Joke")
                        .WithMany("Likes")
                        .HasForeignKey("JokeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Jokes.Data.User", "User")
                        .WithMany("LikedJokes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
