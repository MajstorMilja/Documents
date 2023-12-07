using AutoMapper;
using DokumentCodeFirst.Entity;
using DokumentCodeFirst.IService;
using DokumentCodeFirst.Models;
using DokumentCodeFirst.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DokumentCodeFirst.Controllers
{
    [Route("Proizvodi")]
    [ApiController]
    public class ProizvodController : ControllerBase
    {
        private readonly IProizvodService _proizvod;
        private readonly IMapper _mapper;
        public ProizvodController(IProizvodService proizvod, IMapper mapper)
        {
            _proizvod = proizvod;
            _mapper = mapper;
        }
        // GET: ProizvodController
        [HttpGet]
        public async Task<ActionResult> GetAllProizvodAsync() =>
           Ok(_mapper.Map<IEnumerable<ProizvodDto>>(_proizvod.GetAllProizvod()));
        

        [HttpPost]
        public void PostProizvod(ProizvodDto proizvod) =>
                _proizvod.AddProizvod(_mapper.Map<Proizvod>(proizvod));
          
        [HttpGet("getById/{ProizvodId}")]
        public async Task<IActionResult> GetProizvodById(int ProizvodId) 
        {
            if(ProizvodId != 0)
            {
                var proizvod = _proizvod.GetById(ProizvodId);
                if(proizvod != null) 
                {
                    return Ok(_mapper.Map<ProizvodDto>(proizvod));
                }
                else
                {
                    return Ok("Proizvod ne postoji");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public void EditProizvod(ProizvodDto proizvod,int ProizvodId) =>
            _proizvod.EditProizvod(_mapper.Map<Proizvod>(proizvod));
      



        [HttpDelete]
        public void DeleteProizvod(int proizvodId) =>
            _proizvod.DeleteProizvod(proizvodId);


    }
}
