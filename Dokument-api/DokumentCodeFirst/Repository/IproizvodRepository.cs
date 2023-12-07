using DokumentCodeFirst.Entity;

namespace DokumentCodeFirst.Repository
{
    public interface IproizvodRepository
    {
        public IEnumerable<Proizvod> GetAllProizvod();
        public void DeleteProizvod(int ProizvodId);
        public Proizvod? GetById(int ProizvodId);
        public void AddProizvod(Proizvod proizvod);
        public void EditProizvod(Proizvod proizvod);
    }
}
