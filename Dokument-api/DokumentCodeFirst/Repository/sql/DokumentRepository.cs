using DokumentCodeFirst.Data;
using DokumentCodeFirst.Entity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using AutoMapper;
using DokumentCodeFirst.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DokumentCodeFirst.Repository.sql
{
    public class DokumentRepository : IdokumentRepository
    {
        private readonly IMemoryCache _cache;
        private readonly SqlContext _db;
        string cacheKey = new CacheClass().TipDockKey;
        public DokumentRepository(SqlContext db,IMapper mapper,IMemoryCache cache)
        {
            _db = db;
            _cache = cache;
        }

        public IEnumerable<Dokument> GetAllDokuments
        (int page,int KlijentId ,int tipDokumentaId, DateTime? Datum, int PageSize, out int totalDocuments) 
        {
            IQueryable<Dokument> query = _db.Dokument;
            if (Datum.HasValue && tipDokumentaId > 0 && KlijentId > 0)
            {
                DateTime dateToCompare = Datum.Value.Date;
                query = query.Where(d => d.DatumKreiranja.Day == dateToCompare.Day &&
                                         d.DatumKreiranja.Month == dateToCompare.Month &&
                                         d.DatumKreiranja.Year == dateToCompare.Year &&
                                         d.TipDokumentaId == tipDokumentaId &&
                                         d.KlijentId == KlijentId);
            }
            if (Datum.HasValue && tipDokumentaId > 0 && KlijentId == 0)
            {
                DateTime dateToCompare = Datum.Value.Date;
                query = query.Where(d => d.DatumKreiranja.Day == dateToCompare.Day &&
                                         d.DatumKreiranja.Month == dateToCompare.Month &&
                                         d.DatumKreiranja.Year == dateToCompare.Year &&
                                         d.TipDokumentaId == tipDokumentaId);
            }
            if (Datum.HasValue && KlijentId > 0 && tipDokumentaId == 0)
            {
                DateTime dateToCompare = Datum.Value.Date;
                query = query.Where(d => d.DatumKreiranja.Day == dateToCompare.Day &&
                                         d.DatumKreiranja.Month == dateToCompare.Month &&
                                         d.DatumKreiranja.Year == dateToCompare.Year &&
                                         d.KlijentId == KlijentId);
            }
            if (!Datum.HasValue && KlijentId > 0 && tipDokumentaId > 0)
            {
                query = query.Where(d => d.TipDokumentaId == tipDokumentaId &&
                                         d.KlijentId == KlijentId);
            }
            if (tipDokumentaId != 0 && !Datum.HasValue && KlijentId == 0)
            {
                query = query.Where(d => d.TipDokumentaId == tipDokumentaId);
            }
            if (KlijentId != 0 && !Datum.HasValue && tipDokumentaId == 0)
            {
                query = query.Where(d => d.KlijentId == KlijentId);
            }
            if (Datum.HasValue && tipDokumentaId == 0 && KlijentId == 0)
            {
                DateTime dateToCompare = Datum.Value.Date;
                query = query.Where(d => d.DatumKreiranja.Day == dateToCompare.Day &&
                                         d.DatumKreiranja.Month == dateToCompare.Month &&
                                         d.DatumKreiranja.Year == dateToCompare.Year);
            }
            int filteredDokumentsAmount = query.Count();

            var dokuments = query
                .OrderByDescending(p => p.DatumKreiranja)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            totalDocuments = filteredDokumentsAmount;
             
            return dokuments;
        }

        public string DeleteDokument(int DokumentId)
        {
            var dokument = _db.Dokument
                .FirstOrDefault(d => d.DokumentId == DokumentId);
            if (dokument != null)
            {
                _db.Remove(dokument);
                _db.SaveChanges();
                return "Uspesno ste obrisali dokument";
            }
            else
            {
               return "Dokument ne postoji";
            }
        }
        public Dokument? GetById(int DokumentId)
        {
            var MiljanDokument = _db.Dokument
                .Include(d => d.StavkeDokumenta)
                .FirstOrDefault(p => p.DokumentId == DokumentId);
            return MiljanDokument;
        }
        public void AddDokument(Dokument dokument)
        { 
            if (dokument.DokumentId == 0)
            {
                _db.Add(dokument);
            }
            else
            {
                _db.Dokument.Update(dokument);
                var existingStavke =
                         _db.StavkeDokumenta
                         .AsNoTracking()
                         .Where(s => s.DokumentId == dokument.DokumentId)
                         .ToList();

                foreach (var s in dokument.StavkeDokumenta)
                {
                    s.DokumentId = dokument.DokumentId;

                    if (s.StavkeDokumentaId == 0)
                    {
                        _db.StavkeDokumenta.Add(s);
                    }
                    else
                    {
                        _db.StavkeDokumenta.Update(s);
                    }
                }
                var removeItems = existingStavke
                            .Where(p => !dokument.StavkeDokumenta
                            .Any(q => q.StavkeDokumentaId == p.StavkeDokumentaId));
                if(removeItems != null)
                {
                    _db.RemoveRange(removeItems);
                }
        }
            _db.SaveChanges();
        }

        public List<TipDokumenta> getTipDok()
        {
            if(!_cache.TryGetValue(cacheKey, out List<TipDokumenta> tipovi))
            {
                tipovi = _db.TipDokumenta.ToList();

                _cache.Set(cacheKey, tipovi); 
            }
            return tipovi;
        }
                          

    }
}

