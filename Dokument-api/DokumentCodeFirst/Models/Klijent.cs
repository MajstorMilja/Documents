using System.ComponentModel.DataAnnotations;

namespace DokumentCodeFirst.Models
{
    public class KlijentDto
    {

        public int KlijentId { get; set; }
        [Required(ErrorMessage = "Sediste je obavezan")]
        public string Sediste { get; set; }
        [Required(ErrorMessage = "MaticniBroj je obavezan")]
        public long MaticniBroj { get; set; }
        [Required(ErrorMessage = "Naziv je obavezan")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "PoreskiIdentifikacioniBroj je obavezan")]
        public long PoreskiIdentifikacioniBroj { get; set; }
        [Required(ErrorMessage = "BrojRacuna je obavezan")]
        public long BrojRacuna { get; set; }
        //public List<DokumentDto>? Dokument { get; set; }
    }
}
