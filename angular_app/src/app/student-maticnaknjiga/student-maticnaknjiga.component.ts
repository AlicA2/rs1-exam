import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {MojConfig} from "../moj-config";
import {HttpClient} from "@angular/common/http";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-student-maticnaknjiga',
  templateUrl: './student-maticnaknjiga.component.html',
  styleUrls: ['./student-maticnaknjiga.component.css']
})
export class StudentMaticnaknjigaComponent implements OnInit {
  semestarsedodaje: boolean=false;
  student_id:any;
  studentPodaci:any;
  semestriPodaci:any;
  novi_semestar:any;
AkGodine:any;
  ovjeraSemestra:boolean=false;
  napomena:any;
  datumOvjere:string = new Date().toISOString().substring(0, 10);
  semestarID:number;


  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute) {}

  GetStudent()
  {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Student/GetById?id="+ this.student_id, MojConfig.http_opcije())
      .subscribe((x=>{
        this.studentPodaci=x;
      }));
  }
  noviSemestar()
  {
    this.novi_semestar=
      {
        datumUpisa: new Date(),
        godinaStudija:1,
        akademskaGodina_id:1,
        cijenaSkolarina:1800,
        obnova:false,
        student_id:this.student_id
      }
  }

  GetGodine()
  {
    this.httpKlijent.get(MojConfig.adresa_servera+"/AkademskeGodine/GetAll_ForCmb",MojConfig.http_opcije())
      .subscribe((x=>{
        this.AkGodine=x;
      }));
  }
  GetSemestri()
  {
    this.httpKlijent.get(MojConfig.adresa_servera+"/MaticnaKnjiga/GetById?student_id="+ this.student_id, MojConfig.http_opcije())
      .subscribe((x=>{
        this.semestriPodaci=x;
      }));
  }



  SaveChanges()
  {
    this.httpKlijent.post(MojConfig.adresa_servera+"/MaticnaKnjiga/Add",this.novi_semestar,MojConfig.http_opcije())
      .subscribe((x)=>{
        porukaSuccess("Uspjesno dodan student");
        this.GetSemestri();
      },(y)=>{
        porukaError("Greska");
      });
  }
  ovjeriLjetni(s:any) {

  }

  upisLjetni(s:any) {

  }


    ovjeriZimski() {
      var novaOvjera={
        Id:this.semestarID,
        datumOvjere:this.datumOvjere,
        Napomena:this.napomena
      };
      this.httpKlijent.put(MojConfig.adresa_servera+'/MaticnaKnjiga/Ovjeri',novaOvjera,MojConfig.http_opcije()).subscribe(x=>{
          porukaSuccess("Uspjesna operacija");
          this.GetSemestri();
          this.napomena
        },
        y=>{
          porukaError("Greska")
        });
    }

    SelectID(id:number)
    {
      this.semestarID=id;

    }


  ngOnInit(): void {
    this.route.params.subscribe((x:any)=>{
      this.student_id=+x["id"];
    });
    this.GetStudent();
    this.GetSemestri();
    this.GetGodine();
  }
}
