import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {Router} from "@angular/router";
declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-studenti',
  templateUrl: './studenti.component.html',
  styleUrls: ['./studenti.component.css']
})
export class StudentiComponent implements OnInit {

  title:string = 'angularFIT2';
  ime_prezime:string = '';
  opstina: string = '';
  studentPodaci: any;
  filter_ime_prezime: boolean;
  filter_opstina: boolean;
  def_opstina_id: number;
  naslov: string;
  open: boolean=false;
  student: any;
  opstine: any;
  openClose()
  {
    this.open=!this.open;
  }
  constructor(private httpKlijent: HttpClient, private router: Router) {}
  filtriraj()
  {
    const params= new HttpParams()
      .set("ime_prezime",this.ime_prezime)
      .set("opstina",this.opstina);
    this.testirajWebApi(params);
  }

 odaberiStudenta()
 {
   this.naslov="Dodaj student";
   this.openClose();
   this.student={
     ime:this.ime_prezime,
     prezime: '',
     opstina_rodjenja_id: this.def_opstina_id
   };
 }
 Obrisi(id:number)
 {
    this.httpKlijent.delete(MojConfig.adresa_servera+'/Student/Delete?id='+ id)
      .subscribe((res)=> {
        if(!!res)
        {
          porukaSuccess("Uspjesno izbrisan student");
          this.testirajWebApi();
        }
      });
 }
 toMaticna(id:number)
 {
   this.router.navigate(['student-maticnaknjiga',id]);
 }
 urediStudenta()
 {
   this.naslov="Edit student";
   this.openClose();
 }
 SaveChanges()
 {
   this.httpKlijent.post(MojConfig.adresa_servera+'/Student/Add',this.student)
     .subscribe((res)=>{
       if(!!res)
     {
       this.openClose();
       porukaSuccess("Uspjesno dodan student");
       this.testirajWebApi();
     }});
 }
 loadOpstine()
 {
   this.httpKlijent.get(MojConfig.adresa_servera+'/Opstina/GetByAll')
     .subscribe((res)=>{if(!!res)
     {
       this.opstine=res;
     }});
 }
  testirajWebApi(params?:HttpParams) :void
  {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Student/GetAll", {params})
      .subscribe((x)=> {
        this.studentPodaci=x;
        if(Array.isArray(x))
          this.def_opstina_id=x[6].opstina_rodjenja_id;
      });
  }

  ngOnInit(): void {
    this.testirajWebApi();
    this.loadOpstine();
  }

}
