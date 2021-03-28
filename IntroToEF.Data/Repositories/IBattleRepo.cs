using IntroToEF.Data.Entities;
using System.Collections.Generic;

namespace IntroToEF.Data.Repositories
{
    public interface IBattleRepo
    {
        Battle GetBattle(int id);
        List<Battle> GetBattles();
        Battle GetBattleWithSamurai(int id);
    }
}