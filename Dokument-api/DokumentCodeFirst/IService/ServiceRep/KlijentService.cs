using DokumentCodeFirst.Entity;
using DokumentCodeFirst.Models;
using DokumentCodeFirst.Repository;
using Microsoft.Extensions.Caching.Memory;


namespace DokumentCodeFirst.IService.sql
{
    public class KlijentService : IKlijentService
    {  
        private readonly IMemoryCache _cache;
        private readonly IklijentRepository _klijentRep;
        string cacheKey = new CacheClass().KlijentKey;
        public KlijentService( IMemoryCache cache , IklijentRepository klijent)
        {
            _cache = cache;
            _klijentRep = klijent;
        }
        public IEnumerable<Klijent> GetAllKlijents()
        {
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<Klijent> klijentData))
            {
                klijentData = _klijentRep.GetAllKlijents();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(2));
                
                _cache.Set(cacheKey, klijentData, cacheEntryOptions);
            }
            return klijentData;
        }

        public void DeleteKlijent(int klijentId) { 
            _klijentRep.DeleteKlijent(klijentId);
            InvalidateCache();
        }
        public Klijent? GetById(int klijentId) =>
            _klijentRep.GetById(klijentId);

     
        public void AddKlijent(Klijent klijent)
        {
            _klijentRep.AddKlijent(klijent);
            InvalidateCache();
        }
        
        public void EditKlijent(int klijentId, Klijent klijent) {
            _klijentRep.EditKlijent(klijentId, klijent);
            InvalidateCache();
        }

        private void InvalidateCache() =>
            _cache.Remove(cacheKey);



        public IEnumerable<Dokument>? DokumentiKlijenta(int klijentId) =>
            _klijentRep.DokumentiKlijenta(klijentId);
    }
}

