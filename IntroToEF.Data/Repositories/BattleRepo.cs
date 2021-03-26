using IntroToEF.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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




    }
}
