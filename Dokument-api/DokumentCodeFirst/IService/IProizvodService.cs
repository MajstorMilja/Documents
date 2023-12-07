using DokumentCodeFirst.Entity;

namespace DokumentCodeFirst.IService
{
    public interface IProizvodService
    {
        public IEnumerable<Proizvod> GetAllProizvod();
        public void DeleteProizvod(int ProizvodId);
        public Proizvod? GetById(int ProizvodId);
        public void AddProizvod(Proizvod proizvod);
        public void EditProizvod(Proizvod proizvod);
    }
}
