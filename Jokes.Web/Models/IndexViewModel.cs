using Jokes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jokes.Web.Models
{
    public class IndexViewModel
    {
        public Joke Joke { get; set; }
        public User User { get; set; }
    }
}
