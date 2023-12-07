using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DokumentCodeFirst.Entity
{
    public class TipDokumenta
    {
        [Required(ErrorMessage = "TipDokumentaId je obavezan")]
        public int TipDokumentaId { get; set; }
        public string Naziv { get; set; }
    }
}
