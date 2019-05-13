using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Jokes.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Jokes.Data;
using Microsoft.Extensions.Configuration;

namespace Jokes.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var repo = new JokesRepository(_connectionString);
            IndexViewModel vm = new IndexViewModel();
            vm.Joke = repo.GetRandomJoke();


            if (User.Identity.IsAuthenticated)
            {
                var usersRepo = new UsersRepository(_connectionString);
                vm.User = usersRepo.GetByEmail(User.Identity.Name);
                
            }
            return View(vm);
        }

        public IActionResult ViewAllJokes()
        {
            var repo = new JokesRepository(_connectionString);
            List<Joke> jokes = repo.GetJokes();
            return View(jokes);
        }

        [HttpPost]
        public void Like(int jokeId, int userId, bool liked)
        {
            var repo = new JokesRepository(_connectionString);
            repo.Like(jokeId, userId, liked);
        }

        //public bool? GetLikeStatus(int jokeId, int userId)
        //{
        //    var repo = new JokesRepository(_connectionString);
        //    return repo.GetLikeStatus(jokeId, userId);
        //}
    }
}
