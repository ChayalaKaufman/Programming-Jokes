using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jokes.Data
{
    public class JokesContext : DbContext
    {
        private string _connectionString;

        public DbSet<Joke> Jokes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLikedJoke> UserLikedJokes { get; set; }

        public JokesContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //set up composite primary key
            modelBuilder.Entity<UserLikedJoke>()
                .HasKey(ul => new { ul.UserId, ul.JokeId });

            //set up foreign key from userlike to users
            modelBuilder.Entity<UserLikedJoke>()
                .HasOne(ul => ul.User)
                .WithMany(u => u.LikedJokes)
                .HasForeignKey(ul => ul.UserId);

            //set up foreign key from userlike to joke
            modelBuilder.Entity<UserLikedJoke>()
                .HasOne(ul => ul.Joke)
                .WithMany(j => j.Likes)
                .HasForeignKey(ul => ul.JokeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
