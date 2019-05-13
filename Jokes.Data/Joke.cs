using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Jokes.Data
{
    public class Joke
    {
        public int Id { get; set; }
        [JsonProperty ("id")]
        public int OriginalId { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string SetUp { get; set; }
        [Required]
        public string Punchline { get; set; }
        public List<UserLikedJoke> Likes { get; set; }
   }
}
