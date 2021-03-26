using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IntroToEF.Data.Entities
{
    public class Samurai
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Dynasty { get; set; }

        // One to many relationship -> A samurai can have 0,1 or more quotes
        public List<Quote> Quotes { get; set; } = new List<Quote>();

        public List<Horse> Horses { get; set; } = new List<Horse>();
        public List<Battle> Battles { get; set; } = new List<Battle>();
    }
}