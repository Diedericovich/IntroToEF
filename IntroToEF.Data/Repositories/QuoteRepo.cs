using IntroToEF.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToEF.Data.Repositories
{
    public class QuoteRepo : IQuoteRepo
    {
        private SamuraiContext context;
        public QuoteRepo()
        {
            context = new SamuraiContext();
        }

        public void AddQuote()
        {
            var quote = new Quote()
            {
                Text = "This is a quote!",
                SamuraiId = 5,
            };
            context.Quotes.Add(quote);
            context.SaveChanges();
        }

        public void AddQuotes()
        {
            List<Quote> quoteList = new List<Quote>
            {
                new Quote
                {
                    Text = "I dreamt of worldly success once.",
                    SamuraiId = 6,
                },
                new Quote
                {
                    Text = "A man who can't uphold his beliefs is pathetic dead or alive - Hajime Saito",
                    SamuraiId = 1,
                },
                new Quote
                {
                    Text = "Bushido is realized in the presence of death. This means choosing death whenever there is a choice between life and death. There is no other reasoning.",
                    SamuraiId = 7,
                },
            };

            context.Quotes.AddRange(quoteList);
            context.SaveChanges();
        }
        public Quote GetQuote(int id)
        {
            return context.Quotes.Find(id);
        }

        public Quote GetQuoteWithIncludedData(int id)//doet hetzelde als de methode hierboven
        {
            return context.Quotes.Include(x => x.Samurai).FirstOrDefault(x => x.Id == id);
        }

        public List<Quote> GetQuotes()
        {
           // return context.Quotes.ToList();
            return context.Quotes.Include(x => x.Samurai).ToList();
        }

        public void UpdateQuote(int id, Quote quote)
        {
            throw new NotImplementedException();
        }

        public void DeleteQuote(int id)
        {
            throw new NotImplementedException();
        }
    }
}
