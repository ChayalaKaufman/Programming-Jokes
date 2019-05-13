using System;
using System.Collections.Generic;
using System.Text;

namespace Jokes.Data
{
    public class UserLikedJoke
    {
        public int UserId { get; set; }
        public int JokeId { get; set; }
        public DateTime DateLiked { get; set; }
        public bool Liked { get; set; }
        public User User { get; set; }
        public Joke Joke { get; set; }
    }
}
