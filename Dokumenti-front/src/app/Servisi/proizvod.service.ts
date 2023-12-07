import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProizvodService {

  constructor(private http: HttpClient) { }

  api = 'https://localhost:7083/Proizvodi'


  getAllProizvodi(){
    return this.http.get(this.api)
  }
}
