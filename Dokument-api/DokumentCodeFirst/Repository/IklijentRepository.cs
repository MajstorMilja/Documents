using DokumentCodeFirst.Entity;

namespace DokumentCodeFirst.Repository
{
    public interface IklijentRepository
    {
        public IEnumerable<Klijent> GetAllKlijents();

        public void DeleteKlijent(int klijentId);

        public Klijent? GetById(int klijentId);

        public void AddKlijent(Klijent klijent);

        public void EditKlijent(int klijentId, Klijent klijent);

        public IEnumerable<Dokument>? DokumentiKlijenta(int klijentId);
    }
}
