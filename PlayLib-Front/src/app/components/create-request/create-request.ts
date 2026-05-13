import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RequestService } from '../../services/request';

@Component({
    selector: 'app-create-request',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './create-request.html',
    styleUrl: './create-request.css',
})
export class CreateRequest {
    @Input() accentColor: string = '#39D7FF';
    @Output() closed = new EventEmitter<boolean>();

    gameName: string = '';
    description: string = '';
    isTabletop: boolean = false;
    sending: boolean = false;
    error: string = '';

    constructor(private requestSvc: RequestService) { }

    send(): void {
        if (!this.gameName.trim()) {
            this.error = 'El título es obligatorio';
            return;
        }

        const userId = localStorage.getItem('userId');
        const request = {
            id: '00000000-0000-0000-0000-000000000000',
            userId: userId,
            username: '',
            gameName: this.gameName,
            description: this.description,
            isTabletop: this.isTabletop,
            approved: null
        };
        
        this.sending = true;
        this.error = '';
        this.requestSvc.create(request as any).subscribe({
            next: () => {
                this.sending = false;
                this.closed.emit(true);
            },
            error: () => {
                this.sending = false;
                this.error = 'No se pudo enviar la petición. Inténtalo de nuevo.';
            }
        });
    }

    cancel(): void {
        this.closed.emit(false);
    }
}
