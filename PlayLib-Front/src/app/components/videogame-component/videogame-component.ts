import { ChangeDetectorRef, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Videogame } from '../../model/videogame';
import { Review } from '../../model/review';
import { Details } from '../../services/details';

@Component({
  selector: 'app-videogame-component',
  imports: [CommonModule],
  templateUrl: './videogame-component.html',
  styleUrl: './videogame-component.css',
})
export class VideogameComponent {
  videogame!: Videogame;
  tags: string[] = [];
  reviews: Review[] = [];
  constructor(private detailsSvc: Details, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    const id = history.state?.id;

    if (id) {
      this.loadGame(id);
    }
  }

  loadGame(id: string) {
    this.detailsSvc.getVideogameDetails(id).subscribe((data: any) => {
      console.log('Received videogame details:', data);
      this.videogame = {
        developer: data.developer,
        description: data.description,
        id: data.id,
        image_route: data.imageRoute,
        name: data.name,
        state: data.timesPlayed,
        format: data.format,
        release_date: new Date(data.releaseDate),
        tags: data.tags.map((tag: any) => ({
          id: tag.id,
          name: tag.name,
          hex: tag.hex
        }))
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
}

