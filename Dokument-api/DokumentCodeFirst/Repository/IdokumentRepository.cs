using DokumentCodeFirst.Entity;
using System;

namespace DokumentCodeFirst.Repository
{
    public interface IdokumentRepository
    {

        public IEnumerable<Dokument> GetAllDokuments(int page, int KlijentId, int TipId, DateTime? Datum, int PageSize,out int totalDocuments);

        public string DeleteDokument(int DokumentId);

        public Dokument? GetById(int DokumentId);

        public void AddDokument(Dokument dokument);

        //public void EditDokuemnt(int? DokumentId, Dokument dokument);

        //public void AddStavke(IEnumerable<StavkeDokumenta> stavke, int dokumentId);

        public List<TipDokumenta> getTipDok();
    }
}
