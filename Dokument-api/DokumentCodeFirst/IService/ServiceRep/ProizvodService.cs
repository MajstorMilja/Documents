using DokumentCodeFirst.Entity;
using DokumentCodeFirst.Models;
using DokumentCodeFirst.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace DokumentCodeFirst.IService.sql
{
    public class ProizvodService : IProizvodService
    {
        private readonly IMemoryCache _cache;
        private readonly IproizvodRepository _proizvodRep;
        string cacheKey = new CacheClass().ProizvodKey;
        public ProizvodService(IMemoryCache cache, IproizvodRepository proizvod)
        {
            _cache = cache;
            _proizvodRep = proizvod;
        }
        public IEnumerable<Proizvod> GetAllProizvod()
        {
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<Proizvod> ProzivodData))
            {
                ProzivodData = _proizvodRep.GetAllProizvod();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(2));

                _cache.Set(cacheKey, ProzivodData, cacheEntryOptions);
            }
            return ProzivodData;
        }

        public void DeleteProizvod(int ProizvodId)
        {
            _proizvodRep.DeleteProizvod(ProizvodId);
            InvalidateCache();
        }
        public Proizvod? GetById(int ProizvodId) =>
            _proizvodRep.GetById(ProizvodId);



        public void AddProizvod(Proizvod proizvod)
        {
            _proizvodRep.AddProizvod(proizvod);
            InvalidateCache();
        }

        public void EditProizvod(Proizvod proizvod)
        {
            _proizvodRep.EditProizvod( proizvod);
            InvalidateCache();
        }

        private void InvalidateCache() =>
               _cache.Remove(cacheKey);
        
    }
}
