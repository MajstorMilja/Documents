import { Component, OnInit } from '@angular/core';
import { ProizvodService } from '../Servisi/proizvod.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Proizvod } from '../Models/proizvod';
import { Klijent } from '../Models/klijent';
import { tipDokumenta } from '../Models/TipDokumenta';
import { DokumentService } from '../Servisi/dokument.service';
import { KlijentService } from '../Servisi/klijent.service';
import { Dokument } from '../Models/dokument';
import { StavkeDokumenta } from '../Models/stavke-dokumenta';


@Component({
  selector: 'app-add-new-dokument',
  templateUrl: './add-new-dokument.component.html',
  styleUrls: ['./add-new-dokument.component.css']
})
export class AddNewDokumentComponent implements OnInit {
  stavkeForms: FormGroup[] = [];
  NekiBool: boolean = false;
  yesyes: any;
  proizvodi: Proizvod[];
  dokumentForm: FormGroup;
  klijenti: Klijent[];
  tip: tipDokumenta[];
  disabled: boolean = false;
  constructor(
    private Hproizvodi: ProizvodService, private formBuilder: FormBuilder,
    private dokument: DokumentService, private klijent: KlijentService) { }
  getAllProizvodi() {
    this.Hproizvodi.getAllProizvodi().subscribe((data) => {
      this.yesyes = data;
      this.proizvodi = this.yesyes;
      this.yesyes = '';
    })
  }

  getKlijentAndDokumentType() {
    this.dokument.getTipDok().subscribe((data) => {
      this.yesyes = data;
      this.tip = this.yesyes;
      this.yesyes = '';
    });
    this.klijent.GetAllKlijents().subscribe((data) => {
      this.yesyes = data;
      this.klijenti = this.yesyes;
      this.yesyes = '';
    })
  }
  ngOnInit(): void {
    this.DokumentForm();
    this.getKlijentAndDokumentType();
    this.getAllProizvodi();
  }

  DokumentForm() {
    const currentDate = new Date();
    this.dokumentForm = this.formBuilder.group({
      dokumentId: [0, Validators.required],
      datum: [currentDate, Validators.required],
      datumDospeca: [currentDate, Validators.required],
      ukupnaCena: [0, Validators.required],
      mestoDospeca: ['', Validators.required],
      klijentId: [0, Validators.required],
      tipDokumentaId: [1, Validators.required],
      datumKreiranja: [currentDate, Validators.required],
      stavkeDokumenta: StavkeDokumenta,
    });
  }
  openNewForm(): void {
    if (this.NekiBool = false) {
      this.NekiBool = true;
    }
    const newForm = this.formBuilder.group({
      stavkeDokumentaId: 0,
      kolicina: 1,
      cena: 0,
      ukupnaCena: 0,
      popust: 0,
      proizvodId: 1,
      dokumentId: 0
    });

    this.stavkeForms.push(newForm);
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
    this.calculateUkupnaCena();
  }
  hellodokumet() {
    const kljentControl = this.dokumentForm.get('klijentId');
    const klijentvalue = kljentControl.value
    const tipControl = this.dokumentForm.get('tipDokumentaId');
    const tiptvalue = tipControl.value
    const mestoDospecacontrol = this.dokumentForm.get('mestoDospeca');
    const mestoDospecavalue = mestoDospecacontrol.value
    console.log(this.dokumentForm)
  }


  helloworld(index: number): void {
    const form = this.stavkeForms[index];
    const kolicinaControl = form.get('kolicina');
    const kolicinaValue = kolicinaControl.value;
    const cenacontrol = form.get('cena');
    const cenaValue = cenacontrol.value;
    const popustControl = form.get('popust');
    const popustvalue = popustControl.value;
    const proizvodControl = form.get('proizvodId');``
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
  }

  calculateUkupnaCena(): void {
    let totalCena = 0;
    for (const form of this.stavkeForms) {
      const ukupnaCenaControl = form.get('ukupnaCena');
      const ukupnaCenaValue = ukupnaCenaControl.value;
      totalCena += ukupnaCenaValue ? parseFloat(ukupnaCenaValue) : 0;
    }
    this.dokumentForm.patchValue({ ukupnaCena: totalCena });
  }


  submitForms() {
    if (this.stavkeForms && this.dokumentForm != null) {
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
          stavkeDokumentaId: 0,
          kolicina: form.get('kolicina').value,
          cena: form.get('cena').value,
          ukupnaCena: form.get('ukupnaCena').value,
          popust: form.get('popust').value,
          proizvodId: form.get('proizvodId').value,
          dokumentId: 0
        };
        stavke.push(stavka);
      });

      const dokument: Dokument = {
        dokumentId: 0,
        datum: this.dokumentForm.get('datum').value,
        datumDospeca: this.dokumentForm.get('datumDospeca').value,
        ukupnaCena: this.dokumentForm.get('ukupnaCena').value,
        mestoDospeca: this.dokumentForm.get('mestoDospeca').value,
        klijentId: parseInt(this.dokumentForm.get('klijentId').value),
        tipDokumentaId: this.dokumentForm.get('tipDokumentaId').value,
        datumKreiranja: this.dokumentForm.get('datumKreiranja').value,
        stavkeDokumenta: stavke
      };

      this.dokument.postDokument(dokument).subscribe((data: any) => {
        console.log(data)
      })
      this.disabled = true;
    }
  }

}

