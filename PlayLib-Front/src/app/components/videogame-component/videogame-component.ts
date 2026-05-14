import { ChangeDetectorRef, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Videogame } from '../../model/videogame';
import { Review } from '../../model/review';
import { Details } from '../../services/details';
import { ReviewCard } from '../review-card/review-card';
import { CreateReview } from '../create-review/create-review';
import { ReviewService } from '../../services/review';

import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-videogame-component',
  standalone: true,
  imports: [CommonModule, ReviewCard, CreateReview, MatSelectModule, MatFormFieldModule, FormsModule],
  templateUrl: './videogame-component.html',
  styleUrl: './videogame-component.css',
})
export class VideogameComponent {
  videogame!: Videogame;
  tags: string[] = [];
  reviews: Review[] = [];
  showCreateReview: boolean = false;

  constructor(private detailsSvc: Details, private cdr: ChangeDetectorRef, private reviewSvc: ReviewService) { }

  ngOnInit(): void {
    const id = history.state?.id;

    if (id) {
      this.loadGame(id);
    }
  }

  loadGame(id: string) {
    this.detailsSvc.getVideogameDetails(id).subscribe((data: any) => {
      console.log('Videogame details response:', data);
      this.videogame = {
        developer: data.developer,
        description: data.description,
        id: data.id,
        image_route: data.imageRoute,
        name: data.name,
        state: data.state,
        format: data.format,
        release_date: new Date(data.releaseDate),
        tags: data.tags.map((tag: any) => ({
          id: tag.id,
          name: tag.name,
          hex: tag.hex
        })),
        languages: data.languages.map((lang: any) => ({
          id: lang.id,
          name: lang.name
        })),
        isInLibrary: data.isInLibrary,
        isFavourite: data.isFavourite
      };
      this.reviews = (data.reviews ?? []).map((r: any) => ({
        id: r.id,
        username: r.username,
        userId: r.userId,
        userImage: r.userImage,
        title: '',
        content: r.content ?? '',
        rating: r.rating,
        review_date: new Date(r.reviewDate)
      }));
      this.cdr.detectChanges();
    });
  }

  toggleLibrary(): void {
    if (this.videogame.isInLibrary) {
      this.detailsSvc.removeVideogameFromLibrary(this.videogame.id).subscribe(() => {
        this.videogame.isInLibrary = false;
        this.cdr.detectChanges();
        this.loadGame(this.videogame.id);
      });
    } else {
      this.detailsSvc.addVideogameToLibrary(this.videogame.id).subscribe(() => {
        this.videogame.isInLibrary = true;
        this.cdr.detectChanges();
        this.loadGame(this.videogame.id);
      });
    }
  }

  toggleFavourite(): void {
    if (this.videogame.isFavourite) {
      this.detailsSvc.removeVideogameFromFavourites(this.videogame.id).subscribe(() => {
        this.videogame.isFavourite = false;
        this.cdr.detectChanges();
      });
    } else {
      this.detailsSvc.addVideogameToFavourites(this.videogame.id).subscribe(() => {
        this.videogame.isFavourite = true;
        this.cdr.detectChanges();
      });
    }
  }

  openCreateReview(): void {
    this.showCreateReview = true;
  }

  onReviewClosed(created: boolean): void {
    this.showCreateReview = false;
    if (created && this.videogame) {
      this.loadGame(this.videogame.id);
    }
  }

  onReviewDeleted(reviewId: string): void {
    const userId = localStorage.getItem('userId');
    if (userId) {
      this.reviewSvc.deleteReview(reviewId, userId).subscribe({
        next: () => this.loadGame(this.videogame.id),
        error: (err) => console.error('Error deleting review', err)
      });
    }
  }

  updateFormat(newFormat: string): void {
    this.detailsSvc.updateVideogameFormat(this.videogame.id, newFormat).subscribe({
      next: () => {
        this.videogame.format = newFormat;
        this.cdr.detectChanges();
      },
      error: (err) => console.error('Error updating format', err)
    });
  }

  updateState(newState: string): void {
    this.detailsSvc.updateVideogameState(this.videogame.id, newState).subscribe({
      next: () => {
        this.videogame.state = newState;
        this.cdr.detectChanges();
      },
      error: (err) => console.error('Error updating state', err)
    });
  }
}
