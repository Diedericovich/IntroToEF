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