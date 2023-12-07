using AutoMapper;
using DokumentCodeFirst.Entity;
using DokumentCodeFirst.IService;
using DokumentCodeFirst.Models;
using DokumentCodeFirst.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DokumentCodeFirst.Controllers
{
    [Route("klijent")]
    [ApiController]
    public class KlijentController : ControllerBase
    {
        private readonly IKlijentService _klijent;
        private readonly IMapper _mapper;

        public KlijentController(IKlijentService klijent,IMapper mapper)
        {
            _klijent = klijent;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllKlijents()
        {
            var klijenti = _mapper.Map<IEnumerable<KlijentDto>>(_klijent.GetAllKlijents());
            if (klijenti != null)
            {
                return Ok(klijenti);
            }
            else { return BadRequest(); }
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetKlijentById(int id) =>
                    Ok(_mapper.Map<KlijentDto>(_klijent.GetById(id)));



        [HttpPost]
        public void PostProizvod(KlijentDto klijent) =>
                _klijent.AddKlijent(_mapper.Map<Klijent>(klijent));



        [HttpPut]
        public void EditProizvod(int klijentId, KlijentDto klijent) =>
                _klijent.EditKlijent(klijentId, _mapper.Map<Klijent>(klijent));




        [HttpDelete]
        public void DeleteProizvod(int klijentId) =>
               _klijent.DeleteKlijent(klijentId);

        [HttpGet("klijentDokuments")]
        public async Task<IActionResult> klijentDokuments(int KlijentId)
        {
            if (KlijentId != 0)
            {
                var dokumenti = _klijent.DokumentiKlijenta(KlijentId);
                if (dokumenti != null)
                {
                    return Ok(_mapper.Map<KlijentDto>(dokumenti));
                }
                else
                {
                    return Ok("Klijent nema dokumenata");
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
