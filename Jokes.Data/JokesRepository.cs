using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Jokes.Data
{
    public class JokesRepository
    {
        private string _connectionString;

        public JokesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Joke GetRandomJoke()
        {
            var client = new HttpClient();
            string url = "https://official-joke-api.appspot.com/jokes/programming/random";
            string json = client.GetStringAsync(url).Result;
            var result = JsonConvert.DeserializeObject<List<Joke>>(json);
            var joke = result.FirstOrDefault();

            var existingJoke = GetJoke(joke.OriginalId);

            if(existingJoke == null)
            {
                joke.Id = SaveJoke(joke);
            }
            else
            {
                joke.Id = existingJoke.Id;
            }

            return joke;
        }

        public Joke GetJoke(int originalId)
        {
            using (var ctx = new JokesContext(_connectionString))
            {
                return ctx.Jokes.Include(j => j.Likes).FirstOrDefault(j => j.OriginalId == originalId);
            }
        }

        public int SaveJoke(Joke joke)
        {
            using(var ctx = new JokesContext(_connectionString))
            {
                ctx.Jokes.Add(joke);
                ctx.SaveChanges();
                return joke.Id;
            }
        }

        public void Like(int jokeId, int userId, bool liked)
        {
            using (var context = new JokesContext(_connectionString))
            {
                UserLikedJoke likedjoke = context.UserLikedJokes.FirstOrDefault(ulj => ulj.JokeId == jokeId
                && ulj.UserId == userId);
                if (likedjoke == null)
                {
                    AddLike(jokeId, userId, liked);
                }
                else
                {
                    UpdateLike(jokeId, userId, liked);
                }
            }
        }

        private void AddLike(int jokeId, int userId, bool liked)
        {
            using(var context = new JokesContext(_connectionString))
            {
                var like = new UserLikedJoke
                {
                    JokeId = jokeId,
                    UserId = userId,
                    Liked = liked,
                    DateLiked = DateTime.Now
                };
                context.UserLikedJokes.Add(like);
                context.SaveChanges();
            }
        }

        public void UpdateLike(int jokeId, int userId, bool liked)
        {
            using (var context = new JokesContext(_connectionString))
            {
                context.Database.ExecuteSqlCommand(@"UPDATE UserLikedJokes
                   SET Liked = @liked WHERE UserId = @userId AND JokeId = @jokeId",
                   new SqlParameter("@liked", liked),
                   new SqlParameter("@userId", userId),
                   new SqlParameter("@jokeId", jokeId));
            }
        }

        public List<Joke> GetJokes()
        {
            using (var context = new JokesContext(_connectionString))
            {
                return context.Jokes.Include(j => j.Likes).ToList();
            }
        }
    }
}
