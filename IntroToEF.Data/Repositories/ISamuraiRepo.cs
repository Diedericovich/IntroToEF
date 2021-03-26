using IntroToEF.Data.Entities;
using System.Collections.Generic;

namespace IntroToEF.Data.Repositories
{
    public interface ISamuraiRepo
    {
        void AddDifferentObjectsToContext();
        void AddSamurai(string name);
        void AddSamurai(Samurai samurai);
        void AddSamurais();
        void DeleteSamurai(int id);
        Samurai GetSamurai(int id, bool fetchAllRelatedData = false);
        Samurai GetSamuraiWithIncludedData(int id);
        Samurai GetSamuraiByName(string name);
        List<Samurai> GetSamurais();
        Samurai GetSamuraiWildCards(string text);
        List<Samurai> GetSamuraisWildCards(string text);
        void UpdateSamurai(Samurai samurai);
        void UpdateSamurais();
        List<Samurai> GetResultFromStoredProcedure(string text);
    }
}