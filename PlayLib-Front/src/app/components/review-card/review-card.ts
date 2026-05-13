import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Review } from '../../model/review';
import { ProfileService } from '../../services/profile';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ConfirmDialog } from '../confirm-dialog/confirm-dialog';

@Component({
    selector: 'app-review-card',
    standalone: true,
    imports: [CommonModule, MatIconModule, MatDialogModule],
    templateUrl: './review-card.html',
    styleUrl: './review-card.css',
})
export class ReviewCard implements OnInit {
    @Input() review!: Review;
    @Input() accentColor: string = '#39D7FF';
    @Output() deleted = new EventEmitter<string>();

    currentUserId: string | null = null;
    isAdmin: boolean = false;

    constructor(private profileSvc: ProfileService, private dialog: MatDialog) {}

    ngOnInit() {
        this.currentUserId = localStorage.getItem('userId');
        if (this.currentUserId) {
            this.profileSvc.isAdmin(this.currentUserId).subscribe({
                next: (isAdmin) => this.isAdmin = isAdmin,
                error: () => this.isAdmin = false
            });
        }
    }

    get canDelete(): boolean {
        return this.isAdmin || (this.currentUserId === this.review.userId);
    }

    deleteReview() {
        const dialogRef = this.dialog.open(ConfirmDialog, {
            data: {}
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                this.deleted.emit(this.review.id);
            }
        });
    }

    get estrellas(): number[] {
        return Array.from({ length: 10 }, (_, i) => i + 1);
    }
}
