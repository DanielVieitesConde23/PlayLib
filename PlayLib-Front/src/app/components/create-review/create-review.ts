import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReviewService } from '../../services/review';

@Component({
    selector: 'app-create-review',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './create-review.html',
    styleUrl: './create-review.css',
})
export class CreateReview {
    @Input() gameId!: string;
    @Input() accentColor: string = '#39D7FF';
    @Output() closed = new EventEmitter<boolean>();

    rating: number = 5;
    content: string = '';
    sending: boolean = false;
    error: string = '';

    constructor(private reviewSvc: ReviewService) { }

    send(): void {
        const userId = localStorage.getItem('userId');
        const review = {
            id: '00000000-0000-0000-0000-000000000000',
            gameId: this.gameId,
            userId: userId,
            username: '',
            reviewDate: new Date().toISOString(),
            rating: this.rating,
            content: this.content
        };
        this.sending = true;
        this.error = '';
        this.reviewSvc.createReview(review).subscribe({
            next: () => {
                this.sending = false;
                this.closed.emit(true);
            },
            error: () => {
                this.sending = false;
                this.error = 'No se pudo enviar la reseña. Inténtalo de nuevo.';
            }
        });
    }

    cancel(): void {
        this.closed.emit(false);
    }
}
