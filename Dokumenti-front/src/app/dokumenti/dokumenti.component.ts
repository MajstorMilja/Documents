import { AfterViewInit, Component, Input, OnInit, ViewChild, OnChanges, SimpleChanges } from '@angular/core';
import { Dokument } from '../Models/dokument';
import { MatTableDataSource } from '@angular/material/table';
import { DokumentService } from '../Servisi/dokument.service';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Klijent } from '../Models/klijent';
import { KlijentService } from '../Servisi/klijent.service';
import { ProizvodService } from '../Servisi/proizvod.service';
import { Proizvod } from '../Models/proizvod';
import { tipDokumenta } from '../Models/TipDokumenta';
import { StavkeDokumenta } from '../Models/stavke-dokumenta';
import { MatSort, Sort } from '@angular/material/sort';


@Component({
  selector: 'app-dokumenti',
  templateUrl: './dokumenti.component.html',
  styleUrls: ['./dokumenti.component.css']
})
export class DokumentiComponent implements OnInit, AfterViewInit, OnChanges {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort:MatSort;
  @Input() receivedTipId: number;
  @Input() receivedDate: string;
  @Input() receivedKlijent: number;
  dokuments: any
  SendTipId: number;
  FreeTypeAny: any;
  FormDokument: FormGroup;
  dataSource = new MatTableDataSource<Dokument>();
  klijenti: Klijent[]
  postojeceStavke = new MatTableDataSource<StavkeDokumenta>();
  stavkeForms: FormGroup[] = [];
  ExistingStavkeForm: FormGroup[] = [];
  proizvodi: Proizvod[]
  tip: tipDokumenta[];
  stavkeCounter: number = 0;
  NekiBool: boolean = false;
  pageSize: number = 10;
  pageSizeOptions = [5, 10, 25, 100];
  numberOfPages: number;
  currentPage: number = 1;
  locked: boolean = true;
  loadPrice: boolean = true;
  DokumentAmount: number;
  DokId: number;
  SortedData: Dokument[];
  currentSort: Sort;
  // stavke:StavkeDokumenta[] = [];
  // dataSource: MatTableDataSource<any>;
  constructor(private http: DokumentService, private httpklijent: KlijentService, private formBuilder: FormBuilder, private proizvodHttp: ProizvodService) {
   }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['receivedTipId'] && !changes['receivedTipId'].firstChange) {
      this.getSortedDokuments();
    }
    if (changes['receivedDate'] && !changes['receivedDate'].firstChange) {
      this.getSortedDokuments();
    }
    if (changes['receivedKlijent'] && !changes['receivedKlijent'].firstChange) {
      this.getSortedDokuments();
    }
  }
  ngOnInit(): void {
    this.SetForms();
    this.getAllDokuments()
    this.getAllProizvodiAndType();
  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator
    this.dataSource.sort = this.sort
  }
  displayedColumns: string[] = ['rowNumber', 'klijent', 'tip', 'ukupnaCena', 'mestoDospeca', 'datum', 'datumDospeca', 'datumKreiranja'];
  StavkeColumns: string[] = ['proizvodId', 'kolicina', 'cena', 'popust', 'ukupnaCena', 'dugmici'];
  getAllDokuments() {
    this.http.GetAllDokuments(this.currentPage, this.receivedKlijent, this.receivedTipId, this.pageSize).subscribe((data) => {
      this.dokuments = data;
      this.dataSource.data = this.dokuments.dokuments;
      this.dataSource.paginator = this.paginator
      this.numberOfPages = this.dokuments.totalPages;
      this.DokumentAmount = this.dokuments.totalDocuments
      this.dokuments = '';
      // this.dataSource.data = this.dokuments;
    });
    this.httpklijent.GetAllKlijents().subscribe((data: Klijent[]) => {
      this.klijenti = data;
      this.populateKlijentiMap(data);
    });
    this.http.getTipDok().subscribe((data: tipDokumenta[]) => {
      this.tip = data;
      this.PopulateTip(data);
    })
  }

  getSortedDokuments() {this.dokuments.totalPages;
    this.http.GetAllDokuments(this.currentPage, this.receivedKlijent, this.receivedTipId, this.pageSize, this.receivedDate).subscribe((data) => {
      this.dokuments = data;
      this.dataSource.data = this.dokuments.dokuments;
      this.dataSource.paginator = this.paginator
      this.DokumentAmount = this.dokuments.totalDocuments
      this.dokuments = '';
      this.paginator.firstPage();
    })
  }
  onPageChange(page: PageEvent): void {
    this.currentPage = page.pageIndex + 1;
    this.pageSize = page.pageSize;
    this.http.GetAllDokuments(this.currentPage, this.receivedKlijent, this.receivedTipId, this.pageSize).subscribe((data) => {
      this.dokuments = data;
      this.dataSource = this.dokuments.dokuments;
      this.dataSource.paginator = this.paginator;
      this.DokumentAmount = this.dokuments.totalDocuments
      console.log(this.DokumentAmount);
      this.dokuments = '';
      // this.dataSource.data = this.dokuments;
    });
  }

  sortData(sort:Sort){
    const data = this.dataSource.data.slice();
    if(!sort.active || sort.direction === '')
      {
        this.SortedData = data;
        return;
  } 
  this.SortedData = data.sort((a, b) =>{
    const isAsc= sort.direction === 'asc';
    switch(sort.active) {
      case 'ukupnaCena' : return compare(a.ukupnaCena,b.ukupnaCena,isAsc);
      default: return 0;
    }
  });
}

  getAllProizvodiAndType() {
    this.proizvodHttp.getAllProizvodi().subscribe((data) => {
      this.FreeTypeAny = data;
      this.proizvodi = this.FreeTypeAny;
      this.FreeTypeAny = '';
    })
  }

  TipMap: Map<number, tipDokumenta> = new Map<number, tipDokumenta>();
  PopulateTip(tip: tipDokumenta[]): void {
    for (const t of tip) {
      this.TipMap.set(t.tipDokumentaId, t);
    }
  }

  GetTipName(tipId: number): string {
    const tipNaming = this.tip.find(k => k.tipDokumentaId === tipId);
    return this.tip ? tipNaming.naziv : '';
  }

  klijentiMap: Map<number, Klijent> = new Map<number, Klijent>();
  populateKlijentiMap(klijenti: Klijent[]): void {
    for (const klijent of klijenti) {
      this.klijentiMap.set(klijent.klijentId, klijent);
    }
  }

  getKlijentName(klijentId: number): string {
    const klijent = this.klijenti.find(k => k.klijentId === klijentId);
    return klijent ? klijent.naziv : '';
  }
  SetForms() {
    this.FormDokument = new FormGroup({
      dokumentId: new FormControl(),
      datum: new FormControl(),
      datumDospeca: new FormControl(),
      ukupnaCena: new FormControl(),
      mestoDospeca: new FormControl(),
      klijentId: new FormControl(),
      tipDokumentaId: new FormControl(),
      datumKreiranja: new FormControl(),
      stavkeDokumenta: new FormControl()
    })
  }
  changePage(pageNumber: number) {
    this.currentPage = pageNumber;
    this.getAllDokuments();
    console.log(this.SendTipId)
  }
  onKeyPress(event: any) {
    const keyCode = event.which ? event.which : event.keyCode;
    const keyValue = String.fromCharCode(keyCode);
    const input = (event.target as HTMLInputElement).value;
    let value = input + keyValue;
    if (value !== "0") {
      value = value.replace(/^0+/, "");
    }
    const isNumeric = /^(\d+(\.\d*)?|\.\d+)$/.test(value);
    if (!isNumeric) {
      event.preventDefault();
    }
  }
  removeForm(index: number): void {
    this.stavkeForms.splice(index, 1);
    this.stavkeCounter -= 1;
    this.calculateUkupnaCena();
  }
  helloworld(index: number): void {
    const form = this.stavkeForms[index];
    const kolicinaControl = form.get('kolicina');
    const kolicinaValue = kolicinaControl.value;
    const cenacontrol = form.get('cena');
    const cenaValue = cenacontrol.value;
    const popustControl = form.get('popust');
    const popustvalue = popustControl.value;
    const proizvodControl = form.get('proizvodId');
    const proizvodValue = proizvodControl.value;
    if (isNaN(popustvalue) || popustvalue < 0) {
      const nekiConst = 0;
      popustControl.setValue(nekiConst);
    }
    else if (isNaN(popustvalue) || popustvalue > 100) {
      const nekiConst = 100;
      popustControl.setValue(nekiConst);
    }

    setTimeout(() => {
      const updatedPopustValue = popustControl.value;
      const totalPrice = (cenaValue * kolicinaValue) - (cenaValue * kolicinaValue * updatedPopustValue / 100);
      const ukupnaCenaControl = form.get('ukupnaCena');
      ukupnaCenaControl.setValue(totalPrice);
    }, 10);
    setTimeout(() => {
      this.calculateUkupnaCena();
    }, 30);
    console.log(this.stavkeForms)
  }

  onInputChange() {
  }

  showMessage = false;
  openMessage(id: number) {
    this.showMessage = true;
    this.http.getSingleDokument(id).subscribe((data) => {
      this.FreeTypeAny = data;
      this.DokId = this.FreeTypeAny.dokumentId
      console.log(this.DokId)
      if (this.FreeTypeAny.stavkeDokumenta != null && this.FreeTypeAny.stavkeDokumenta != undefined) {
        this.stavkeForms = this.FreeTypeAny.stavkeDokumenta.map((stavka: any) =>
          this.formBuilder.group({
            stavkeDokumentaId: [stavka.stavkeDokumentaId],
            kolicina: [stavka.kolicina, Validators.max(1000)],
            cena: [stavka.cena],
            ukupnaCena: [stavka.ukupnaCena],
            popust: [stavka.popust],
            proizvodId: [stavka.proizvodId],
            dokumentId: [stavka.dokumentId]
          })
        );
      }
      this.FormDokument.setValue(data);
      this.getKlijentName(data.klijentId);
    });
    if (this.loadPrice) {
      this.calculateUkupnaCena();
      this.loadPrice = false;
    }
  }

  closeMessage() {
    this.showMessage = false;
    this.stavkeForms.splice(0, this.stavkeForms.length);
    this.stavkeCounter = 0;
  }
  // obrisi(index: number): void {
  //   this.postojeceStavke.data.splice(index, 1);
  //   this.postojeceStavke._updateChangeSubscription();
  //   this.calculateUkupnaCena();
  // }
  openNewForm(): void {
    if (this.NekiBool = false) {
      this.NekiBool = true;
    }
    this.stavkeCounter += 1;
    const newForm = this.formBuilder.group({
      stavkeDokumentaId: 0,
      kolicina: [1, Validators.max(1000)],
      cena: 0,
      ukupnaCena: 0,
      popust: 0,
      proizvodId: 1,
      dokumentId: this.FormDokument.get('dokumentId').value
    });
    this.stavkeForms.push(newForm);
  }

  calculateUkupnaCena(): void {
    let totalCena = 0;
    for (const form of this.stavkeForms) {
      const ukupnaCenaControl = form.get('ukupnaCena');
      const ukupnaCenaValue = ukupnaCenaControl.value;
      totalCena += ukupnaCenaValue ? parseFloat(ukupnaCenaValue) : 0;
    }
    this.FormDokument.patchValue({ ukupnaCena: totalCena });
  }

  obrisiDokument() {
    let response = confirm('da li zelite da obrisete ovaj dokument?')
    if (response) {
      this.http.DeleteDokument(this.DokId)
      // const data = this.dataSource.data;
      // data.splice(this.DokId, 1);
      // this.dataSource.data = data;
      location.reload()
      this.paginator.firstPage();
    }
   
  }

  submitForms() {
    if (this.stavkeForms && this.FormDokument != null) {
      let hasInvalidStavke = false;
      this.stavkeForms.forEach(form => {
        const kolicina = form.get('kolicina').value;
        const cena = form.get('cena').value;
        if (kolicina === 0 || cena === 0) {
          hasInvalidStavke = true;
        }
      });
      if (hasInvalidStavke) {
        console.log('Greska u matrixu');
        return;
      }
      const stavke: StavkeDokumenta[] = [];
      this.stavkeForms.forEach(form => {
        const stavka: StavkeDokumenta = {
          stavkeDokumentaId: form.get('stavkeDokumentaId').value,
          kolicina: form.get('kolicina').value,
          cena: form.get('cena').value,
          ukupnaCena: form.get('ukupnaCena').value,
          popust: form.get('popust').value,
          proizvodId: form.get('proizvodId').value,
          dokumentId: this.FormDokument.get('dokumentId').value,
        };
        stavke.push(stavka);
      });
      // this.postojeceStavke.data.forEach(stavka => {
      //   const HelloSir: StavkeDokumenta = {
      //     stavkeDokumentaId: stavka.stavkeDokumentaId,
      //     kolicina: stavka.kolicina,
      //     cena: stavka.cena,
      //     ukupnaCena: stavka.ukupnaCena,
      //     popust: stavka.popust,
      //     proizvodId: stavka.proizvodId,
      //     dokumentId: this.FormDokument.get('tipDokumentaId').value
      //   };
      //   stavke.push(HelloSir)
      // })

      const dokument: Dokument = {
        dokumentId: this.FormDokument.get('dokumentId').value,
        datum: this.FormDokument.get('datum').value,
        datumDospeca: this.FormDokument.get('datumDospeca').value,
        ukupnaCena: this.FormDokument.get('ukupnaCena').value,
        mestoDospeca: this.FormDokument.get('mestoDospeca').value,
        klijentId: parseInt(this.FormDokument.get('klijentId').value),
        tipDokumentaId: this.FormDokument.get('tipDokumentaId').value,
        datumKreiranja: this.FormDokument.get('datumKreiranja').value,
        stavkeDokumenta: stavke
      };
      this.http.postDokument(dokument).subscribe((data: any) => {
        console.log(data)
      })
    }
    setTimeout(() => {
      this.closeMessage()
      window.location.reload()
    }, 1000);
  }
}

function compare(a: number | string, b: number | string, isAsc: boolean) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}