using DokumentCodeFirst.Data;
using DokumentCodeFirst.Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace DokumentCodeFirst.Repository.sql
{
    public class StavkeRepository : IstavkeRepository
    {
        //private readonly SqlContext _db;
        //public StavkeRepository(SqlContext db)
        //{
        //    _db = db;
        //}
        //public IEnumerable<StavkeDokumenta> GetAllStavke()
        //{
        //    return _db.StavkeDokumenta;
        //}
        //public StavkeDokumenta DeleteStavke(StavkeDokumenta stavke)
        //{
        //    var deletedStavke = _db.StavkeDokumenta.Remove(stavke).Entity;
        //    _db.SaveChanges();
        //    return deletedStavke; 
        //}
        //public StavkeDokumenta? GetById(int stavkaId)
        //{
        //    return _db.StavkeDokumenta.FirstOrDefault(p => p.StavkeDokumentaId == stavkaId);
        //}
        //public IEnumerable<StavkeDokumenta>? GetStavkeDokumentaById(int DokumentId)
        //{
        //    return _db.StavkeDokumenta.Where(p => p.DokumentId == DokumentId).ToList();
        //}
        //public void AddStavke(IEnumerable<StavkeDokumenta> stavke,int dokumentId)
        //{
        //    if(dokumentId == 0) 
        //    { 
        //        var DonutId = _db.Dokument.Max(p => p.DokumentId);
        //        foreach (var stavka in stavke)
        //    {
        //         stavka.DokumentId = DonutId;
        //        _db.StavkeDokumenta.Add(stavka);
        //    }
        //    _db.SaveChanges();
        //    }
        //    else if (dokumentId > 0)
        //    {
        //        var existingStavke = _db.StavkeDokumenta.Where(s => s.DokumentId == dokumentId).ToList();
        //        foreach (var stavka in existingStavke)
        //        {
        //            stavka.DokumentId = dokumentId;
        //            _db.Entry(stavka).State = EntityState.Modified;
        //        }
        //        foreach (var stavka in stavke)
        //        {
        //            if (!_db.StavkeDokumenta.Any(s => s.StavkeDokumentaId == stavka.StavkeDokumentaId))
        //            {
        //                stavka.DokumentId = dokumentId;
        //                _db.StavkeDokumenta.Add(stavka);
        //            }
        //        }
        //        _db.SaveChanges();
        //    }
        //}
        //public void EditSingleStavka(int ProizvodId, StavkeDokumenta stavka)
        //{
        //    _db.Entry(stavka).State = EntityState.Modified;
        //    _db.SaveChanges();
        //}
        //public List<StavkeDokumenta> EditMultipleStavke(int ProizvodId, IEnumerable<StavkeDokumenta> stavke)
        //{
        //    List<StavkeDokumenta> modifiedStavke = new List<StavkeDokumenta>();

        //    foreach (var stavka in stavke)
        //    {
        //        stavka.ProizvodId = ProizvodId;
        //        _db.Entry(stavka).State = EntityState.Modified;
        //        modifiedStavke.Add(stavka);
        //    }

        //    _db.SaveChanges();

        //    return modifiedStavke;
        //}
        //public void DeleteSingleStavka(int stavkeId)
        //{
        //    var stavka = _db.StavkeDokumenta.FirstOrDefault(d => d.StavkeDokumentaId == stavkeId);
        //    if (stavka != null)
        //    {
        //        _db.StavkeDokumenta.Remove(stavka);
        //        _db.SaveChanges();
        //    }
        //}
    }
}

