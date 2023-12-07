using AutoMapper;
using DokumentCodeFirst.Data;
using DokumentCodeFirst.Entity;
using DokumentCodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace DokumentCodeFirst.Repository.sql
{
    public class KlijentRepository : IklijentRepository
    {
        private readonly SqlContext _db;
        private readonly IMapper _mapper;

        public KlijentRepository(SqlContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public IEnumerable<Klijent> GetAllKlijents() =>
                                          _db.Klijent.ToList();
      
        public void DeleteKlijent(int klijentId)
        {
            var Klijent = _db.Klijent
                .Include(x => x.Dokument)
                .FirstOrDefault(k => k.KlijentId == klijentId);
            if (Klijent != null && Klijent.Dokument == null)
            {
                _db.Klijent.Remove(Klijent);
                _db.SaveChanges();
            }
            else
            {
                string message = "Uf klijent poseduje dokument";
            }
        }

        public Klijent? GetById(int klijentId)
        {
            var klijent = _db.Klijent
                .Include(d => d.Dokument)
                .FirstOrDefault(k => k.KlijentId == klijentId);
            return klijent;
        }

        public void AddKlijent(Klijent klijent)
        {
            _db.Klijent.Add(klijent);
            _db.SaveChanges();
        }
        public void EditKlijent(int klijentId, Klijent klijent)
        {
            _db.Entry(klijent).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public IEnumerable<Dokument>? DokumentiKlijenta(int klijentId) => 
            _db.Dokument.Where(p => p.KlijentId == klijentId);
    }
}

