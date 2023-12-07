using System.ComponentModel.DataAnnotations;

namespace DokumentCodeFirst.Entity
{
    public class Klijent
    {
        [Required(ErrorMessage = "KlijentId je obavezan")]
        public int KlijentId { get; set; }
        public string Sediste { get; set; }
        public long MaticniBroj { get; set; }
        public string Naziv { get; set; }
        public long PoreskiIdentifikacioniBroj { get; set; }
        public long BrojRacuna { get; set; }
        public List<Dokument>? Dokument { get; set; }
    }
}
