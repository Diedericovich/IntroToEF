using IntroToEF.Data.Entities;
using System.Collections.Generic;

namespace IntroToEF.Data.Repositories
{
    public interface IQuoteRepo
    {
        void AddQuote();
        void AddQuotes();
        void DeleteQuote(int id);
        Quote GetQuote(int id);
        List<Quote> GetQuotes();
        void UpdateQuote(int id, Quote quote);
    }
}