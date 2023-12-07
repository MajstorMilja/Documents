using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DokumentCodeFirst.Entity
{
    public class Proizvod
    {
        [Required(ErrorMessage = "ProizvodId je obavezan")]
        public int ProizvodId { get; set; }
        public int Sifra { get; set; }
        public string? Popust { get; set; }
        public string Naziv { get; set; }
        public List<StavkeDokumenta>? StavkeDokumenta { get; set; }
    }
}
