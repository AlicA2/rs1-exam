using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul2.Controllers;
using FIT_Api_Examples.Modul2.ViewModels;
using FIT_Api_Examples.Modul3_MaticnaKnjiga.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;



namespace FIT_Api_Examples.Modul3_MaticnaKnjiga.Controller
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MaticnaKnjigaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;



        public MaticnaKnjigaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }



        [HttpGet]
        public ActionResult<UpisAkGodine> GetById(int student_id)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");



            var student = _dbContext.Student.Find(student_id);
            var upisi = _dbContext.UpisAkGodine.Where(s => s.student_id == student_id).ToList();
            var akGodina = _dbContext.AkademskaGodina.ToList();



            return Ok(new
            {
                student,
                upisi,
                akGodina
            });
        }





        [HttpPost]
        public ActionResult<UpisAkGodine> Add([FromBody] UpisAkGodine x)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");



            var semestri = _dbContext.UpisAkGodine.Where(s => s.student_id == x.student_id).Select(g => g.godinaStudija).ToList();



            foreach (var s in semestri)
            {
                if (s == x.godinaStudija && x.obnova == false)
                    return BadRequest("Nije obnova!");
            }



            var upis = new UpisAkGodine
            {
                datumUpisa = x.datumUpisa,
                godinaStudija = x.godinaStudija,
                akademskaGodina_id = x.akademskaGodina_id,
                cijenaSkolarine = x.cijenaSkolarine,
                obnova = x.obnova,
                student_id = x.student_id
            };



            _dbContext.Add(upis);
            _dbContext.SaveChanges();
            return upis;
        }



        [HttpPut]
        public ActionResult<UpisAkGodine> Ovjeri([FromBody] UpisAkGodine x)
        {
            var thisGodina = _dbContext.UpisAkGodine.Find(x.Id);
            if (thisGodina == null)
                return BadRequest("Greska !");
            thisGodina.datumOvjere = x.datumOvjere;
            thisGodina.Napomena = x.Napomena;
            _dbContext.UpisAkGodine.Update(thisGodina);
            _dbContext.SaveChanges();
            return Ok(thisGodina);
        }
    }
}