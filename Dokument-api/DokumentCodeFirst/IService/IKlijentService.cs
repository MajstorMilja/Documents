using DokumentCodeFirst.Entity;

namespace DokumentCodeFirst.IService
{
    public interface IKlijentService
    {
        public IEnumerable<Klijent> GetAllKlijents();

        public void DeleteKlijent(int klijentId);

        public Klijent? GetById(int klijentId);

        public void AddKlijent(Klijent klijent);

        public void EditKlijent(int klijentId, Klijent klijent);

        public IEnumerable<Dokument>? DokumentiKlijenta(int klijentId);
    }
}
