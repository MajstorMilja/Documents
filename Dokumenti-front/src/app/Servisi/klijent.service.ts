import { Injectable } from '@angular/core';
import { Klijent } from '../Models/klijent';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class KlijentService {

  constructor(private http: HttpClient) { }

  api = 'https://localhost:7083/klijent'


  GetAllKlijents() {
    return this.http.get<Klijent[]>(this.api)
  }

  getSingleKlijent(klijentId: number) {
    return this.http.get<Klijent>(this.api + '/getById/' + klijentId)
  }
  deleteKlijent(klijentId: number) {
    this.http.delete(this.api + '/' + klijentId)
  }
  postKlijent(Klijent: Klijent) {
    this.http.post(this.api, Klijent)
  }
  putKlijent(klijent: Klijent, klijentId: number) {
    this.http.put(this.api + '/' + klijentId, klijent)
  }


}
