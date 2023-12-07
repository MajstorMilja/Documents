using AutoMapper;
using DokumentCodeFirst.Entity;
using DokumentCodeFirst.Models;
using DokumentCodeFirst.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DokumentCodeFirst.Controllers
{
    [Route("dokument")]
    [ApiController]
    public class DokumentController : ControllerBase
    {
        private readonly IdokumentRepository _dokument;
        private readonly IMapper _mapper;

        public class GetDokumentsModel
        {
            public int Page { get; set; }
            public int klijentId { get; set; }
            public int tipDokumentaId { get; set; }
            public int PageSize { get; set; }
            public DateTime? Datum { get; set; }
        }

        public DokumentController(IdokumentRepository Dokument, IMapper mapper)
        {
            _dokument = Dokument;
            _mapper = mapper;
        }
        // GET: ProizvodController
        [HttpGet]
        public ActionResult<IEnumerable<DokumentDto>> GetAllDokuments([FromQuery] GetDokumentsModel objekat)
        {
            var dokuments = _mapper.Map<IEnumerable<DokumentDto>>
            (_dokument.GetAllDokuments(objekat.Page, objekat.klijentId, objekat.tipDokumentaId,
            objekat.Datum, objekat.PageSize, out int totalDocuments));
            var response = new
            {
                TotalDocuments = totalDocuments,
                Dokuments = dokuments
            };
            return Ok(response);
        }
        [HttpGet("tip")]

        public IActionResult getTip()
        {
            return Ok(_mapper.Map<IEnumerable<TipDokumentaDto>>(_dokument.getTipDok()));
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult>? GetDokumentById(int id) =>
            Ok(_mapper.Map<DokumentDto>(_dokument.GetById(id)));

        [HttpPost]
        public void PostDokument(DokumentDto dokument) =>
            _dokument.AddDokument(_mapper.Map<Dokument>(dokument));

        [HttpDelete("{dokumentId}")]
        public void DeleteDokument(int dokumentId) =>
                _dokument.DeleteDokument(dokumentId);

    }
}
