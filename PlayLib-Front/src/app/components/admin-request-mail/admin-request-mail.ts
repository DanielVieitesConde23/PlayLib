import { ChangeDetectorRef, Component, OnInit, inject } from '@angular/core';
import { Request } from "../../model/request";
import { MatCardModule } from '@angular/material/card';
import { MatIcon } from "@angular/material/icon";
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { DialogRequestMail } from '../dialog-request-mail/dialog-request-mail';
import { RequestService } from '../../services/request';
import { ConfirmDialog } from '../confirm-dialog/confirm-dialog';

@Component({
  selector: 'app-admin-request-mail',
  imports: [MatCardModule, MatIcon, MatButtonModule, MatDialogModule],
  templateUrl: './admin-request-mail.html',
  styleUrl: './admin-request-mail.css',
})
export class AdminRequestMail implements OnInit {
  requests: Request[] = [];
  readonly dialog = inject(MatDialog);

  constructor(private requestSvc: RequestService, private cdr: ChangeDetectorRef) { }

  ngOnInit() {
    this.loadRequests();
  }

  loadRequests() {
    this.requestSvc.getAll().subscribe({
      next: (reqs) => {
        this.requests = reqs;
        this.cdr.detectChanges();
      },
      error: (err) => console.error('Error loading requests', err)
    });
  }

  approve(requestId: string) {
    const dialogRef = this.dialog.open(ConfirmDialog, {
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.requestSvc.approve(requestId).subscribe({
          next: () => this.loadRequests(),
          error: (err) => console.error('Error approving request', err)
        });
      }
    });
  }

  reject(requestId: string) {
    const dialogRef = this.dialog.open(ConfirmDialog, {
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.requestSvc.deny(requestId).subscribe({
          next: () => this.loadRequests(),
          error: (err) => console.error('Error denying request', err)
        });
      }
    });
  }
  
  openDialog() {
    this.dialog.open(DialogRequestMail, {
      height: '70vh',
      width: '70vw',
      maxWidth: '70vw',
      data: {
        requests: [... this.requests]
      }
    });
  }
}
