using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul0_Autentifikacija.Models;
using FIT_Api_Examples.Modul2.Models;
using FIT_Api_Examples.Modul2.ViewModels;
using FIT_Api_Examples.Modul3_MaticnaKnjiga.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIT_Api_Examples.Modul2.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

      

        [HttpGet]
        public ActionResult<List<Student>> GetAll(string ime_prezime,string opstina)
        {
            var data = _dbContext.Student
                .Include(s => s.opstina_rodjenja.drzava)
                .Where(x => (ime_prezime == null ||
                (x.ime + " " + x.prezime).ToLower().StartsWith(ime_prezime.ToLower()) || 
                (x.prezime + " " + x.ime).ToLower().StartsWith(ime_prezime.ToLower())) &&
                (opstina == null || x.opstina_rodjenja.description.ToLower().StartsWith(opstina.ToLower()))&& x.isDeleted==false)
                .OrderByDescending(s => s.id)
                .AsQueryable();
            return data.Take(100).ToList();
        }


        [HttpPost]
        public ActionResult<Student>Add(StudentAddVM x)
        {
            if(x.id==0)
            {
                var student = new Student
                {
                    ime = x.ime,
                    prezime = x.prezime,
                    opstina_rodjenja_id = x.opstina_rodjenja_id
                };
                _dbContext.Student.Add(student);
                _dbContext.SaveChanges();
                return Ok(student);
            }
            var studentUpdate = _dbContext.Student.Find(x.id);
            studentUpdate.ime = x.ime;
            studentUpdate.prezime = x.prezime;
            studentUpdate.opstina_rodjenja_id = x.opstina_rodjenja_id;
            _dbContext.Student.Update(studentUpdate);
            _dbContext.SaveChanges();
            return Ok(studentUpdate);
        }

        [HttpDelete]
        public ActionResult<Student>Delete(int id)
        {
            var student=_dbContext.Student.Find(id);
            student.isDeleted = true;
            _dbContext.SaveChanges();
            return Ok(student);
        }
        [HttpGet]
        public ActionResult<Student> GetById(int id)
        {
            var student = _dbContext.Student.Find(id);
            return Ok(student);
        }
    }
}
