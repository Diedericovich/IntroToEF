using IntroToEF.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToEF.Data.Repositories
{
    public class HorseRepo : IHorseRepo
    {
        private SamuraiContext context;
        public HorseRepo()
        {
            context = new SamuraiContext();
        }

        public void AddHorse()
        {
            var horse = new Horse()
            {
                Name = "Sam",
                Age = 5,
                IsWarHorse = true,
                SamuraiId = 1,
            };
            context.Horses.Add(horse);
            context.SaveChanges();
        }

        public void AddHorses()
        {
            List<Horse> horseList = new List<Horse>
            {
                new Horse
                {
                    Name = "Joni",
                Age = 1,
                IsWarHorse = false,
                SamuraiId = 3,
                },
                new Horse
                {
                    Name = "Greg",
                Age = 10,
                IsWarHorse = true,
                SamuraiId = 2,
                },
                new Horse
                {
                    Name = "Fluffy",
                Age = 3,
                IsWarHorse = true,
                SamuraiId = 2,
                },
            };

            context.Horses.AddRange(horseList);
            context.SaveChanges();
        }

        public Horse GetHorse(int id)
        {
            return context.Horses.Find(id);
        }

        public Horse GetHorseWithIncludedData(int id)
        {
            return context.Horses.Include(x => x.Samurai).FirstOrDefault(x => x.Id == id);
        }

        public List<Horse> GetHorses()
        {
            //return context.Horses.ToList();
            return context.Horses.Include(x => x.Samurai).ToList();
        }

        public void UpdateHorse(int id, Horse horse)
        {
            throw new NotImplementedException();
        }

        public void DeleteHorse(int id)
        {
            throw new NotImplementedException();
        }
    }
}
