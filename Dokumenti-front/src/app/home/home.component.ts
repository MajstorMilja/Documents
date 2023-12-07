import { Component, OnInit, ViewChild } from '@angular/core';
import { tipDokumenta } from '../Models/TipDokumenta';
import { DokumentService } from '../Servisi/dokument.service';
import { FormBuilder, FormControl, FormGroup, UntypedFormArray } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { Klijent } from '../Models/klijent';
import { KlijentService } from '../Servisi/klijent.service';
import { MatAutocompleteTrigger } from '@angular/material/autocomplete';
import {  Observable, isEmpty, map, startWith } from 'rxjs';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  ngOnInit(): void {
    this.PopulateKlijent();
    this.filterKlijent();
  }
  constructor(private http: DokumentService, private _klijent: KlijentService, private formBuilder: FormBuilder) {
  }
  @ViewChild(MatAutocompleteTrigger, { static: false }) autocomplete!: MatAutocompleteTrigger;
  tip: tipDokumenta[] = [];
  options: any;
  Zero: number = 0;
  selectedKlijent: number;
  tipId: number = 0;
  datum: string;
  klijent: Klijent[] = [];
  formKlijent: FormGroup[] = [];
  klijentBool: boolean = false;
  dokumentBool: boolean = false;
  defaultBool: boolean = true;
  NewDockBool: boolean = false;
  NewKlijent: boolean = false;
  filteredOptions: Observable<{ klijentId: number, naziv: string }[]>;
  currentDate = new Date();
  myControl = new FormControl('');
  inputValue: string = '';

  klijentButton() {
    this.klijentBool = true;
    this.dokumentBool = false;
    this.defaultBool = false;
    this.NewDockBool = false;
    this.NewKlijent = false;
  }

  dokumentButton() {
    this.klijentBool = false;
    this.dokumentBool = true;
    this.defaultBool = false;
    this.NewDockBool = false;
    this.NewKlijent = false;
    this.PopulateTipAndDate();
  }

  NewDokumentButton() {
    this.NewDockBool = true;
    this.klijentBool = false;
    this.dokumentBool = false;
    this.defaultBool = false;
    this.NewKlijent = false;
  }

  NewKlijentButton() {
    this.klijentBool = false;
    this.dokumentBool = false;
    this.defaultBool = false;
    this.NewDockBool = false;
    this.NewKlijent = true;
  }
  PopulateTipAndDate() {
    this.http.getTipDok().subscribe((data: tipDokumenta[]) => {
      this.tip = data;
    });
    this._klijent.GetAllKlijents().subscribe((data) => {
      this.options = data;
      for (const o of this.options) {
        const group = this.formBuilder.group({
          klijentId: o.klijentId,
          naziv: o.naziv
        })
        this.formKlijent.push(group)
      }
    })
  }
  onSelectedProduct(id: number) {
    this.tipId = id;
  }

  onDateSelected(event: MatDatepickerInputEvent<Date>): void {
    const date: Date = event.value;
    const year: number = date.getFullYear();
    const month: number = date.getMonth() + 1;
    const day: number = date.getDate();
    const formattedDate: string = `${year}-${month.toString().padStart(2, '0')}-${day.toString().padStart(2, '0')}`;

    this.datum = formattedDate;
  }
  PopulateKlijent() {
    this._klijent.GetAllKlijents().subscribe((data) => {
      this.klijent = data;
    })
  }
  private _filter(value: string): { klijentId: number, naziv: string }[] {
    const filterValue = value.toLowerCase();
    return this.klijent.filter(klijent => klijent.naziv.toLowerCase().includes(filterValue))
      .map(klijent => ({ klijentId: klijent.klijentId, naziv: klijent.naziv }));
  }

  filterKlijent() {
    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value || '')),
    );
  }
  onKlijentSelected(option: { klijentId: number, naziv: string }) {
    if (option && option.klijentId !== undefined && option.klijentId !== 0) {
      this.selectedKlijent = option.klijentId;
    } else {
      this.selectedKlijent = 0;
    }
  }
CheckInput(ImeKlijenta: string){
  if(ImeKlijenta == '' || ImeKlijenta == undefined){
    this.selectedKlijent = 0;
  }
}
}

