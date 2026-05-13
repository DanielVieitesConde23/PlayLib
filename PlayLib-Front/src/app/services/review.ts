import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../model/environment';

@Injectable({
    providedIn: 'root',
})
export class ReviewService {

    private url = environment.apiURL + 'Review';

    constructor(private http: HttpClient) { }

    createReview(review: { id: string; username: string; reviewDate: string; rating: number; content: string }): Observable<any> {
        return this.http.post(`${this.url}/create`, review);
    }

    deleteReview(reviewId: string, userId: string): Observable<any> {
        return this.http.delete(`${this.url}/delete/${reviewId}/${userId}`);
    }
}
