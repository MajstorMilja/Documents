using AutoMapper;
using DokumentCodeFirst.Data;
using DokumentCodeFirst.Entity;
using DokumentCodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace DokumentCodeFirst.Repository.sql
{
    public class ProizvodRepository : IproizvodRepository
    {

        private readonly SqlContext _db;
        private readonly IMapper _mapper;

        public ProizvodRepository(SqlContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IEnumerable<Proizvod> GetAllProizvod() =>
            _db.Proizvod.ToList();

        public void DeleteProizvod(int ProizvodId)
        {
            var proizvod = _db.Proizvod
                .Include(p => p.StavkeDokumenta)
                .FirstOrDefault(p => p.ProizvodId == ProizvodId);
            if (proizvod != null && proizvod.StavkeDokumenta == null)
            {
                _db.Proizvod.Remove(proizvod);
                _db.SaveChanges();
            }
            else
            {
                string message = "Uf postoje stavke sa ovim proizvodom";
            }
        }

        public Proizvod? GetById(int ProizvodId) =>
          _db.Proizvod.FirstOrDefault(p => p.ProizvodId == ProizvodId);
        

        public void AddProizvod(Proizvod proizvod)
        {
            _db.Proizvod.Add(proizvod);
            _db.SaveChanges();
        }

        public void EditProizvod(Proizvod proizvod)
        {
            _db.Entry(proizvod).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }

}