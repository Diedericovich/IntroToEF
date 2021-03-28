using IntroToEF.Data.Entities;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IntroToEF.Data.Repositories
{
    public class SamuraiRepo : ISamuraiRepo
    {
        private SamuraiContext context;

        public SamuraiRepo()
        {
            // Open connection to database
            context = new SamuraiContext();
        }

        public void AddSamurai(string name)
        {
            // Create a single object to be inserted
            var samurai = new Samurai
            {
                Name = name
            };

            // Add object(s) to be tracked by context
            // Specify target table and data to be added
            context.Samurais.Add(samurai);

            // Push changes to DB
            context.SaveChanges();
        }

        // HUISWERK: UPDATE OF ADD ??


        public void AddSamurai(Samurai samurai)
        {
            context.Samurais.Update(samurai);
            context.SaveChanges();
        }

        public void AddSamurais()
        {
            // Create a list of objects to be INSERTED
            List<Samurai> myList = new List<Samurai>
            {
                new Samurai
                {
                    Name = "Yamamoto Tsunetomo"
                },
                 new Samurai
                {
                    Name = "Miyamoto Musashi"
                },
                new Samurai
                {
                    Name = "ListSam3"
                },
            };

            // Add object(s) to be tracked by context
            // Specify target table and data to be added
            context.Samurais.AddRange(myList);

            // Push changes to DB
            context.SaveChanges();
        }

        //public Samurai GetSamurai(int id)
        //{
        //    return context.Samurais.Find(id);
        //}
        public Samurai GetSamurai(int id, bool fetchAllRelatedData = false)
        {
            Samurai samurai = null;
            if (fetchAllRelatedData)
            {
                samurai = context.Samurais.Include(x => x.Horses).Include(x => x.Quotes).Include(x => x.Battles).FirstOrDefault(x => x.Id == id);
            }
            else
            {
                samurai = context.Samurais.Find(id);
            }
            return samurai;
        }
        public Samurai GetSamuraiWithIncludedData(int id)
        {
            return context.Samurais
                .Include(x => x.Quotes) //search for specific text withing a DB 
                .Include(x => x.Horses)//includes all horses
                .Include(x => x.Battles)
                .FirstOrDefault(x => x.Id == id);
        }
        public Samurai GetSamuraiByName(string name)
        {
            return context.Samurais.FirstOrDefault(x => x.Name == name);
        }
        public List<Samurai> GetSamurais()
        {
            //return context.Samurais.ToList(); // vraagt alle samurai op, maar geen quotes of horses
            return context.Samurais
                .Include(x => x.Quotes)
                //.Include(x => x.Horses)
                //.Include(x => x.Battles)
                .ToList(); // vraagt alles op dat aan samurai hangt
        }

        public Samurai GetSamuraiWildCards(string text)
        {
            return context.Samurais
                .Where(x => x.Name.Contains(text))
                .OrderByDescending(x => x.Name)
                .FirstOrDefault();
        }

        public List<Samurai> GetSamuraisWildCards(string text)
        {
            return context.Samurais
                .Where(x => x.Name.Contains(text))
                .OrderByDescending(x => x.Name)
                .ToList();
        }

        public void UpdateSamurai(Samurai samurai)
        {
            context.SaveChanges(); // gewoon saven is voldoende 
        }

        public void UpdateSamurais()
        {
            List<Samurai> samurais = context.Samurais.Skip(4).Take(3).ToList();
            int i = 0;
            foreach (var samurai in samurais)
            {
                i++;
                samurai.Name = "I was changed in DB" + i;
                samurai.Dynasty = "Sengoku";
            }
            context.SaveChanges();
        }

        public void DeleteSamurai(int id)
        {
            Samurai samurai = GetSamurai(id);
            context.Samurais.Remove(samurai);
            context.SaveChanges();
        }

        public void AddDifferentObjectsToContext()
        {
            // Objects can be inserted in multiple tables in one statement
            var quote = new Quote
            {
                SamuraiId = 1,
                Text = "If the Bird does not sing, Kill it."
            };

            var horse = new Horse
            {
                SamuraiId = 1,
                Age = 5,
                IsWarHorse = true,
                Name = "Jolly jumper"
            };

            context.Add(quote);
            context.Add(horse);
            context.SaveChanges();
        }

        public List<Samurai> GetResultFromStoredProcedure(string text)
        {
            var samurais = context.Samurais.FromSqlRaw("EXEC dbo.SamuraisWhoSaidAWord {0}", text).ToList();
            return samurais;
        }
    }
}