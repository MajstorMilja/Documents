using DokumentCodeFirst.Models;
using DokumentCodeFirst.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DokumentCodeFirst.Controllers.DokumentController;

namespace DokumentCodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StavkeController : ControllerBase
    {

        //private readonly IstavkeRepository _stavke;

        //public StavkeController(IstavkeRepository stavke)
        //{
        //    _stavke = stavke;
        //}
        //[HttpGet("GetAllStavke")]
        //public async Task<ActionResult> GetAllStavke()
        //{
        //    var stavke = _stavke.GetAllStavke();
        //    if (stavke != null)
        //    {
        //        return Ok(stavke);
        //    }
        //    else { return BadRequest(); }
        //}

        //[HttpGet("GetSingleStavkaById")]
        //public async Task<IActionResult> GetSingleStavkaById(int stavkaId)
        //{
        //    if (stavkaId != null && stavkaId != 0)
        //    {
        //        var dokument = _stavke.GetById(stavkaId);
        //        if (dokument != null)
        //        {
        //            return Ok(dokument);
        //        }
        //        else
        //        {
        //            return Ok("stavka ne postoji");
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpGet("GetStavkeById")]
        //public async Task<IActionResult> GetStavkeById(int stavkaId)
        //{
        //    if (stavkaId != null && stavkaId != 0)
        //    {
        //        var dokument = _stavke.GetStavkeDokumentaById(stavkaId);
        //        if (dokument != null && dokument.Any())
        //        {
        //            return Ok(dokument);
        //        }
        //        else
        //        {
        //            return Ok("Dokument sa stavkama ne postoji");
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        ////[HttpPost("PostDokument")]
        ////public ActionResult PostDokument(DokumentWithStavkeModel model)
        ////{
        ////    if (ModelState.IsValid)
        ////    {
        ////        List<StavkeDokumenta> stavkeList = model.Stavke?.ToList() ?? new List<StavkeDokumenta>();
        ////        _dokument.AddDokument(model.Dokument, stavkeList);
        ////        return Ok(model.Dokument);
        ////    }
        ////    else
        ////    {
        ////        return BadRequest();
        ////    }
        ////}

        //[HttpPost("EditSingleStavka")]
        //public async Task<ActionResult> EditSingleStavka(int stavkeId, StavkeDokumenta stavke)
        //{
        //    if (ModelState.IsValid && stavkeId != 0)
        //    {
        //        _stavke.EditSingleStavka(stavkeId, stavke);
        //        return Ok(stavke);
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
        //[HttpPost("EditMultipleStavka")]
        //public async Task<ActionResult> EditMultipleStavka(int stavkeId, IEnumerable<StavkeDokumenta> stavke)
        //{
        //    if (ModelState.IsValid && stavkeId != 0)
        //    {
        //        _stavke.EditMultipleStavke(stavkeId, stavke);
        //        return Ok(stavke);
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}


        //[HttpDelete("DeleteStavke")]
        //public IActionResult DeleteStavke(StavkeDokumenta stavke)
        //{
        //    if(ModelState.IsValid) 
        //    { 
        //    _stavke.DeleteStavke(stavke);
        //    return Ok(stavke);
        //    }
        //    else
        //    {
        //        return BadRequest();  
        //    }
        //}

        //[HttpDelete("DeleteSingleStavka")]
        //public void DeleteSingleStavka(int stavkaId)
        //{
        //    if (stavkaId != null && stavkaId != 0)
        //        _stavke.DeleteSingleStavka(stavkaId);
        //}
    }
}
