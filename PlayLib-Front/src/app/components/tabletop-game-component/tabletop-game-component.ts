import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Tabletop } from '../../model/tabletop';
import { ActivatedRoute } from '@angular/router';
import { Details } from '../../services/details';

@Component({
  selector: 'app-tabletop-game-component',
  imports: [CommonModule],
  templateUrl: './tabletop-game-component.html',
  styleUrl: './tabletop-game-component.css',
})
export class TabletopGameComponent implements OnInit {
  tabletop!: Tabletop;

  constructor(private detailsSvc: Details, private cdr: ChangeDetectorRef) { }

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
        }))
      };
      this.cdr.detectChanges();
    });
  }
}
