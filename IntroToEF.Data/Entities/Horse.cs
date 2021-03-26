using System;
using System.ComponentModel.DataAnnotations;

namespace IntroToEF.Data.Entities
{
    public class Horse
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Range(0, 35)]
        public int Age { get; set; }

        public bool IsWarHorse { get; set; }

        //horse belongs to one samurai
        public Samurai Samurai { get; set; }

        public int SamuraiId { get; set; }
    }
}