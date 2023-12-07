import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { KlijentComponent } from './klijent/klijent.component';
import { DokumentiComponent } from './dokumenti/dokumenti.component';
import { MaterialsModule } from './materials/materials.module';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatPaginatorModule } from '@angular/material/paginator';
import { AddNewDokumentComponent } from './add-new-dokument/add-new-dokument.component';
import { AddNewKlijentComponent } from './add-new-klijent/add-new-klijent/add-new-klijent.component';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    KlijentComponent,
    DokumentiComponent,
    AddNewDokumentComponent,
    AddNewKlijentComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,    
    MaterialsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatPaginatorModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
