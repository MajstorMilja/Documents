import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Klijent } from '../Models/klijent';
import { KlijentService } from '../Servisi/klijent.service';

@Component({
  selector: 'app-klijent',
  templateUrl: './klijent.component.html',
  styleUrls: ['./klijent.component.css']
})
export class KlijentComponent implements OnInit {
  klijents : any
  dataSource = new MatTableDataSource<Klijent>();
  
  
  constructor(private http: KlijentService) { }
  
  ngOnInit(): void {
    this.getAllDokuments()
  }
  displayedColumns: string[] = ['naziv','maticniBroj','sediste','poreskiIdentifikacioniBroj','brojRacuna',];
  getAllDokuments(){
    this.http.GetAllKlijents().subscribe((data)=>{
      this.klijents = data;
      this.dataSource.data = this.klijents;
    })
  }
}
