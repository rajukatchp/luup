import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateWorkFlowComponent } from './create-workflow.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { TranslateModule } from '@ngx-translate/core';
import { MatChipsModule } from '@angular/material/chips';
import { MatButtonModule } from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatDialogModule} from '@angular/material/dialog';
import { HttpClientModule } from '@angular/common/http';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

@NgModule({
  declarations: [CreateWorkFlowComponent],
  imports: [
     CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    MatIconModule,
    HttpClientModule,
    TranslateModule,
    MatChipsModule,
    MatButtonModule,
    MatCardModule,
    MatAutocompleteModule,
    MatDialogModule,
    MatSlideToggleModule,
    MatIconModule
  ],
  exports: [CreateWorkFlowComponent]
})
export class CreateWorkFlowModule { }
