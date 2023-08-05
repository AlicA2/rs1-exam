using FIT_Api_Examples.Modul2.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FIT_Api_Examples.Modul3_MaticnaKnjiga.Models
{
    public class UpisAkGodine
    {
        public int Id { get; set; }
        public DateTime datumUpisa { get; set; }
        public int godinaStudija { get; set; }
        [ForeignKey(nameof(akademskaGodina))]
        public int? akademskaGodina_id { get; set; }
        public AkademskaGodina akademskaGodina { get; set; }
        public float cijenaSkolarine { get; set; }
        public bool obnova { get; set; }
        public DateTime datumOvjere { get; set; }
        public string Napomena { get; set; }
        [ForeignKey(nameof(student))]
        public int? student_id { get; set; }
        public Student student { get; set; }
    }
}
