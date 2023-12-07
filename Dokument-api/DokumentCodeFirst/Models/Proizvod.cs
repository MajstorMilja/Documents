using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DokumentCodeFirst.Models
{
    public class ProizvodDto
    {
        [Required(ErrorMessage = "ProizvodId je obavezan")]
        public int ProizvodId { get; set; }
        [DisplayName("Sifra")]
        [Required(ErrorMessage = "Sifra je obavezana")]
        public int Sifra { get; set; }
        [DisplayName("Popust")]
        public string? Popust { get; set; }
        [DisplayName("Naziv proizvoda")]
        [Required(ErrorMessage = "Naziv je obavezan")]
        public string Naziv { get; set; }
        public List<StavkeDokumentaDto>? StavkeDokumenta { get; set; }
    }
}
