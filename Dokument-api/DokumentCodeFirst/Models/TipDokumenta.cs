using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DokumentCodeFirst.Models
{
    public class TipDokumentaDto
    {
        [Required(ErrorMessage = "TipDokumentaId je obavezan")]
        public int TipDokumentaId { get; set; }
        [DisplayName("Naziv proizvoda")]
        [Required(ErrorMessage = "Naziv je obavezan")]
        public string Naziv { get; set; }
    }
}
