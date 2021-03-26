using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToEF.Data.Entities
{
    public class Battle
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int Year { get; set; }

        [MaxLength(100)]
        public string Location { get; set; }
        public List<Samurai> Samurai { get; set; } = new List<Samurai>();
    }
}
