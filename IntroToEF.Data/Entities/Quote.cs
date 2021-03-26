using System.ComponentModel.DataAnnotations;

namespace IntroToEF.Data.Entities
{
    public class Quote
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Text { get; set; }

        // One to many -> A quote can have only one samurai
        public Samurai Samurai { get; set; }

        // If we respect the naming [ClassName]Id, then EF will be 
        // smart enough to know that this is a Foreign Key
        public int SamuraiId { get; set; }
    }
}