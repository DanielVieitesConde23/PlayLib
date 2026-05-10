import { Component, inject, ViewEncapsulation } from '@angular/core';
import {MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogClose, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-dialog-request-mail',
  imports: [ MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogClose, MatButtonModule, FormsModule, MatSelectModule],
  templateUrl: './dialog-request-mail.html',
  styleUrl: './dialog-request-mail.css',
  encapsulation: ViewEncapsulation.None
})
export class DialogRequestMail {
  data = inject(MAT_DIALOG_DATA);

  requestSeleccionada: any = null;

}
