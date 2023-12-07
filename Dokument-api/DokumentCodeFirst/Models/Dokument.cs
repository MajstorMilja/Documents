using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DokumentCodeFirst.Models
{
    public class DokumentDto
    {
        [Required(ErrorMessage = "DokumentId je obavezan")]
        public int DokumentId { get; set; }
        [DisplayName("Datum")]
        [Required(ErrorMessage = "Datum je obavezan")]
        public DateTime Datum { get; set; }
        [DisplayName("DatumDospeca")]
        [Required(ErrorMessage = "DatumDospeca je obavezan")]
        public DateTime DatumDospeca { get; set; }
        [DisplayName("UkupnaCena")]
        [Required(ErrorMessage = "UkupnaCena je obavezan")]
        public int UkupnaCena { get; set; }
        [DisplayName("MestoDospeca")]
        [Required(ErrorMessage = "MestoDospeca je obavezan")]
        public string MestoDospeca { get; set; }     
        public int KlijentId { get; set; }
        public int TipDokumentaId { get; set; }
        public DateTime DatumKreiranja { get; set; }
        [Required(ErrorMessage = "StavkeDokumenta su obavezane")]
        public List<StavkeDokumentaDto> StavkeDokumenta { get; set; }


    }
}
