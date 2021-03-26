using IntroToEF.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace IntroToEF.Data
{
    public class SamuraiContext : DbContext
    {
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Horse> Horses { get; set; }
        public DbSet<Battle> Battles { get; set; }




        private const string CONNECTION = @"Server=.\SQLEXPRESS;Database=SamuraiDB;Trusted_Connection=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CONNECTION);
                //.LogTo(Console.WriteLine);            
        }
    }
}