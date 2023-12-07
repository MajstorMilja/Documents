import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Klijent } from 'src/app/Models/klijent';
import { KlijentService } from 'src/app/Servisi/klijent.service';

@Component({
  selector: 'app-add-new-klijent',
  templateUrl: './add-new-klijent.component.html',
  styleUrls: ['./add-new-klijent.component.css']
})
export class AddNewKlijentComponent implements OnInit {
  klijent: FormGroup;
  constructor(private _klijent: KlijentService) { }

  FormMeAformForAFormToFormTheForm() {
    this.klijent = new FormGroup({
      klijentId: new FormControl(),
      sediste: new FormControl(),
      maticniBroj: new FormControl(),
      naziv: new FormControl(),
      poreskiIdentifikacioniBroj: new FormControl(),
      brojRacuna: new FormControl(),
    })
  }
  ngOnInit() {
    this.FormMeAformForAFormToFormTheForm()
  }
  AddKlijent(klijent: Klijent) {
    console.log(klijent)
    // this._klijent.postKlijent(klijent)
  }


}
