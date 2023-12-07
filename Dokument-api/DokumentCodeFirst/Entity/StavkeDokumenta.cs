using System.ComponentModel.DataAnnotations;

namespace DokumentCodeFirst.Entity
{
    public class StavkeDokumenta
    {
        [Required(ErrorMessage = "StavkeDokumentaId je obavezan")]
        public int StavkeDokumentaId { get; set; }
        public int Kolicina { get; set; }
        public int Cena { get; set; }
        public int UkupnaCena { get; set; }
        public int Popust { get; set; }
        public int ProizvodId { get; set; }
        public int DokumentId { get; set; }
    }
}
