import { NgModule } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatInputModule } from '@angular/material/input';
import { MatOptionModule, NativeDateModule } from '@angular/material/core';
import { MatPaginatorModule } from '@angular/material/paginator';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatAutocompleteModule} from '@angular/material/autocomplete';
import { MatSortModule } from '@angular/material/sort';
const materialsComponent =
  [MatTableModule, MatButtonModule, MatDatepickerModule, MatCheckboxModule,
    MatCardModule, MatInputModule, NativeDateModule, MatPaginatorModule,
    FormsModule, ReactiveFormsModule, MatNativeDateModule, MatFormFieldModule,
    MatOptionModule,MatSelectModule,MatAutocompleteModule,MatSortModule]

@NgModule({
  declarations: [],
  imports: [
    materialsComponent
  ],
  exports: [
    materialsComponent 
  ],
})
export class MaterialsModule { }
