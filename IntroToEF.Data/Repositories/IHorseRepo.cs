using IntroToEF.Data.Entities;
using System.Collections.Generic;

namespace IntroToEF.Data.Repositories
{
    public interface IHorseRepo
    {
        void AddHorse();
        void AddHorses();
        void DeleteHorse(int id);
        Horse GetHorse(int id);
        Horse GetHorseWithIncludedData(int id);
        List<Horse> GetHorses();
        void UpdateHorse(int id, Horse horse);
    }
}