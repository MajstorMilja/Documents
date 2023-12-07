import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Dokument } from '../Models/dokument';
import { tipDokumenta } from '../Models/TipDokumenta';


@Injectable({
  providedIn: 'root'
})
export class DokumentService {

  constructor(private http: HttpClient) { }
  api = 'https://localhost:7083/dokument'
  
  GetAllDokuments(page: number, klijent: number, type: number, pageSize: number, datum?: string) {
    let url = `${this.api}/?page=${page}&PageSize=${pageSize}`;
  
    if (klijent !== undefined && klijent !== 0) {
      url += `&klijentId=${klijent}`;
   } 
    if (type !== undefined && type !== 0) {
      url += `&tipDokumentaId=${type}`;
    }
    if (datum !== undefined) {
      url += `&Datum=${datum}`;
    }
  
    return this.http.get<Dokument>(url);
  }
  

  getSingleDokument(id: number) {
    return this.http.get<Dokument>(this.api + '/getById/' + id)
  }

  DeleteDokument(id: number) {
    this.http.delete(this.api + '/' + id).subscribe()
  }

  getTipDok() {
    return this.http.get<tipDokumenta[]>(this.api + "/tip")
  }

  postDokument(Dokument: Dokument) {
    console.log(Dokument)
    return this.http.post(this.api, Dokument)
  }
}
