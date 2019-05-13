using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jokes.Data
{
    public class UsersRepository
    {
        private string _connectionString;

        public UsersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(User user, string password)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = hash;

            using(JokesContext ctx = new JokesContext(_connectionString))
            {
                ctx.Users.Add(user);
                ctx.SaveChanges();
            }
        }

        public User GetByEmail(string email)
        {
            //return context.Questions
            //        .Include(q => q.User)
            //        .ThenInclude(q => q.LikedQuestions)
            //        .Include(q => q.Answers)
            //        .ThenInclude(a => a.User)
            //        .Include(q => q.Likes)
            //        .Include(u => u.QuestionsTags)
            //        .ThenInclude(qt => qt.Tag)
            //        .FirstOrDefault(q => q.Id == id);
            using (var ctx = new JokesContext(_connectionString))
            {
                return ctx.Users.Include(u => u.LikedJokes)
                    .FirstOrDefault(u => u.Email == email);
            }
        }

        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            bool correctPassword = PasswordHelper.PasswordMatch(password, user.PasswordHash);
            if (correctPassword)
            {
                return user;
            }

            return null;
        }
    }
}
