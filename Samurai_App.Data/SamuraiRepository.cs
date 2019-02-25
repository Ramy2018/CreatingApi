using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
    public class SamuraiRepository : ISamuraiData
    {
        private readonly SamuraiContext context;

        public SamuraiRepository(SamuraiContext context)
        {
            this.context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<Samurai[]> GetAllSamuraisAsync(bool includeQuote = false,bool includeSamuraiBattles=false)
        {
            IQueryable<Samurai> query = context.Samurais.Include(x=> x.SecretIdentity);
            if (includeQuote)
            {
                query = query.Include(c => c.Quotes);
            }
            if (includeSamuraiBattles)
            {
                query = query.Include(c=> c.SamuraiBattles);
            }
            query = query.OrderBy(c => c.Name);
            return await query.ToArrayAsync();
        }

        public async Task<Samurai> GetSamuraiByIdAsync(int id, bool includeQuote = false, bool includeSamuraiBattles = false)
        {
            IQueryable<Samurai> query = context.Samurais.Include(c=> c.SecretIdentity);

            if (includeQuote)
            {
                query = query.Include(c => c.Quotes);
            }

            if (includeSamuraiBattles)
            {
                query = query.Include(c => c.SamuraiBattles);
            }
            query = query.Where(c => c.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Samurai> GetSamuraiByNameAsync(string name, bool includeQuote = false, bool includeSamuraiBattles = false)
        {
            IQueryable<Samurai> query = context.Samurais;
            if (includeQuote)
            {
                query = query.Include(c => c.Quotes);
            }
            if (includeSamuraiBattles)
            {
                query = query.Include(c => c.SamuraiBattles);
            }
            query = query.Where(c => c.Name == name);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Quote[]> GetAllQuoteAsync()
        {
            IQueryable<Quote> query = context.Quotes;
         
            query = query.OrderBy(c => c.Id);
            return await query.ToArrayAsync();

        }

        public async Task<Quote> GetSingelQutoeAsync(int quoteId)
        {
            IQueryable<Quote> query = context.Quotes;
         
            query = query.Where(x => x.Id == quoteId);
            return await query.FirstOrDefaultAsync();
        }

        public async  Task<Quote[]> GetRelatedAllQuoteByIdAsync(int id)
        {
            IQueryable<Quote> query = context.Quotes;
            query = query.Where(x => x.Samurai.Id == id);
            return await query.ToArrayAsync();
        }

        public async  Task<Quote> GetRelatedSingelQuoteByIdAsync(int id, int talkId)
        {
            IQueryable<Quote> query = context.Quotes;
            query = query.Where(x => x.Id ==talkId && x.Samurai.Id == id);
            return await query.FirstOrDefaultAsync();
        }
    }
}
