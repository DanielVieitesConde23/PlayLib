import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { MatIcon } from "@angular/material/icon";
import { Request } from "../../model/request";
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { RequestService } from '../../services/request';
import { CreateRequest } from '../create-request/create-request';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ConfirmDialog } from '../confirm-dialog/confirm-dialog';

@Component({
  selector: 'app-request-list',
  standalone: true,
  imports: [MatIcon, MatCardModule, CommonModule, CreateRequest, MatDialogModule],
  templateUrl: './request-list.html',
  styleUrl: './request-list.css',
})
export class RequestList implements OnInit {

  requests: Request[] = [];
  showCreateRequest: boolean = false;

  constructor(private requestSvc: RequestService, private cdr: ChangeDetectorRef, private dialog: MatDialog) { }

  ngOnInit() {
    this.loadRequests();
  }

  loadRequests() {
    const userId = localStorage.getItem('userId');
    if (userId) {
      this.requestSvc.getForUser(userId).subscribe({
        next: (reqs) => {
          this.requests = reqs;
          this.cdr.detectChanges();
        },
        error: (err) => console.error('Error loading requests', err)
      });
    }
  }

  remove(requestId: string) {
    const dialogRef = this.dialog.open(ConfirmDialog, {
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.requestSvc.remove(requestId).subscribe({
          next: () => this.loadRequests(),
          error: (err) => console.error('Error removing request', err)
        });
      }
    });
  }

  openCreateRequest() {
    this.showCreateRequest = true;
  }

  onCreateRequestClosed(success: boolean) {
    this.showCreateRequest = false;
    if (success) {
      this.loadRequests();
    }
  }
}
