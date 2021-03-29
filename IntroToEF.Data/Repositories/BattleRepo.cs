using IntroToEF.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IntroToEF.Data.Repositories
{
    public class BattleRepo : IBattleRepo
    {
        private SamuraiContext context;

        public BattleRepo()
        {
            context = new SamuraiContext();
        }

        public void AddBattles()
        {
            List<Battle> myBattleList = new List<Battle>
            {
                new Battle
                {
                    Name = "Goguryeo-Yamato War",
                    Location = "Japan",
                    Year = 404,
                },
                 new Battle
                {
                    Name = "Battle of Shigisan",
                    Location = "Japan",
                    Year = 587,
                },
                new Battle
                {
                    Name = "Isshi Incident",
                    Location = "Japan",
                    Year = 645,
                },
                new Battle
                {
                    Name = "Hayato Rebellion",
                    Location = "Japan",
                    Year = 721,
                },
                new Battle
                {
                    Name = "Fujiwara no Hirotsugu Rebellion",
                    Location = "Japan",
                    Year = 740,
                },
                new Battle
                {
                    Name = "Thirty-Eight Year War",
                    Location = "Japan",
                    Year = 811,
                },
            };

            context.Battles.AddRange(myBattleList);
            context.SaveChanges();
        }

        public List<Battle> GetBattles()
        {
            return context.Battles.ToList();
        }

        public Battle GetBattle(int id)
        {
            return context.Battles.Find(id);
        }

        public Battle GetBattleWithSamurai(int id)
        {
            return context.Battles
                 .Include(x => x.Samurai)
                 .Where(p => p.Id == id)
                 .FirstOrDefault();
        }
    }
}