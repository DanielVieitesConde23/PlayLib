import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Tabletop } from '../../model/tabletop';
import { Review } from '../../model/review';
import { ActivatedRoute } from '@angular/router';
import { Details } from '../../services/details';
import { ReviewCard } from '../review-card/review-card';
import { CreateReview } from '../create-review/create-review';
import { ReviewService } from '../../services/review';

@Component({
  selector: 'app-tabletop-game-component',
  imports: [CommonModule, ReviewCard, CreateReview],
  templateUrl: './tabletop-game-component.html',
  styleUrl: './tabletop-game-component.css',
})
export class TabletopGameComponent implements OnInit {
  tabletop!: Tabletop;
  reviews: Review[] = [];
  showCreateReview: boolean = false;
  accentColor: string = '#b13fbb';

  constructor(private detailsSvc: Details, private cdr: ChangeDetectorRef, private reviewSvc: ReviewService) { }

  ngOnInit(): void {
    const id = history.state?.id;

    if (id) {
      this.loadGame(id);
    }
  }

  loadGame(id: string) {
    this.detailsSvc.getTabletopGameDetails(id).subscribe((data: any) => {
      console.log('Received tabletop game details:', data);
      this.tabletop = {
        average_duration: data.averageDuration,
        creator: data.developer,
        description: data.description,
        id: data.id,
        image_route: data.imageRoute,
        max_player: data.maxPlayerNumber,
        min_player: data.minPlayerNumber,
        name: data.name,
        plays: data.timesPlayed,
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
        title: '',
        content: r.content ?? '',
        rating: r.rating,
        review_date: new Date(r.reviewDate)
      }));
      this.cdr.detectChanges();
    });
  }

  toggleLibrary(): void {
    if (this.tabletop.isInLibrary) {
      this.detailsSvc.removeTabletopFromLibrary(this.tabletop.id).subscribe(() => {
        this.tabletop.isInLibrary = false;
        this.cdr.detectChanges();
      });
    } else {
      this.detailsSvc.addTabletopToLibrary(this.tabletop.id).subscribe(() => {
        this.tabletop.isInLibrary = true;
        this.cdr.detectChanges();
      });
    }
  }

  toggleFavourite(): void {
    if (this.tabletop.isFavourite) {
      this.detailsSvc.removeTabletopFromFavourites(this.tabletop.id).subscribe(() => {
        this.tabletop.isFavourite = false;
        this.cdr.detectChanges();
      });
    } else {
      this.detailsSvc.addTabletopToFavourites(this.tabletop.id).subscribe(() => {
        this.tabletop.isFavourite = true;
        this.cdr.detectChanges();
      });
    }
  }

  openCreateReview(): void {
    this.showCreateReview = true;
  }

  onReviewClosed(created: boolean): void {
    this.showCreateReview = false;
    if (created && this.tabletop) {
      this.loadGame(this.tabletop.id);
    }
  }

  onReviewDeleted(reviewId: string): void {
    const userId = localStorage.getItem('userId');
    if (userId) {
      this.reviewSvc.deleteReview(reviewId, userId).subscribe({
        next: () => this.loadGame(this.tabletop.id),
        error: (err) => console.error('Error deleting review', err)
      });
    }
  }
}


