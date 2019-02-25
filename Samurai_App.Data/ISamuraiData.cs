using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiApp.Data
{
    public interface ISamuraiData
    {
        void Add<T>(T entity) where T:class;
        void Delete<T>(T entity) where T:class;
        Task<bool> SaveChangesAsync();

        Task<Samurai[]> GetAllSamuraisAsync(bool includeQuote = false, bool includeSamuraiBattles = false);
        Task<Samurai> GetSamuraiByNameAsync(string name, bool includeQuote = false, bool includeSamuraiBattles = false);
        Task<Samurai> GetSamuraiByIdAsync(int id, bool includeQuote = false, bool includeSamuraiBattles = false);

        Task<Quote[]> GetAllQuoteAsync();
        Task<Quote> GetSingelQutoeAsync(int quoteId);
        Task<Quote[]> GetRelatedAllQuoteByIdAsync(int id);
        Task<Quote> GetRelatedSingelQuoteByIdAsync(int id, int quoteId);




    }
}
