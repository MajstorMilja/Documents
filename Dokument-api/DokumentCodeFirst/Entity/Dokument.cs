using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DokumentCodeFirst.Entity
{
    public class Dokument
    {
        [Required(ErrorMessage = "DokumentId je obavezan")]
        public int DokumentId { get; set; }
        public DateTime Datum { get; set; }
        public DateTime DatumDospeca { get; set; }
        public int UkupnaCena { get; set; }
        public string MestoDospeca { get; set; }
        
        public DateTime DatumKreiranja { get; set; }
        public List<StavkeDokumenta> StavkeDokumenta { get; set; }
        public int KlijentId { get; set; }
        public int TipDokumentaId { get; set; }
        //public Klijent Klijent { get; set; }

        //public TipDokumenta TipDokumenta { get; set; }
    }
}
