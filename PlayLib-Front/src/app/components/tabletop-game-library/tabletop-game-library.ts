import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Tabletop } from '../../model/tabletop';
import { RouterModule, Router } from '@angular/router';
@Component({
  selector: 'app-tabletop-game-library',
  imports: [
    CommonModule,
    FormsModule,
    MatToolbarModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    RouterModule
  ],
  templateUrl: './tabletop-game-library.html',
  styleUrl: './tabletop-game-library.css',
})
export class TabletopGameLibrary {
  searchText = '';
  filter = 'all';
  constructor(private router: Router) { }
  tabletops: Tabletop[] = [
    {
      id: '1',
      name: 'Unstable Unicorns',
      description: '',
      creator: 'Unstable Games',
      image_route: 'https://i.imgur.com/zJ6oPoP.jpg',
      release_date: new Date('2017-01-01'),
      min_player: 2,
      max_player: 8,
      average_duration: 30,
      plays: 5
    },
    {
      id: '2',
      name: 'Carcassonne',
      description: '',
      creator: 'Klaus-Jürgen Wrede',
      image_route: 'assets/img/carcassonne.jpg',
      release_date: new Date('2000-01-01'),
      min_player: 2,
      max_player: 5,
      average_duration: 45,
      plays: 5
    },
    {
      id: '3',
      name: 'Ticket to Ride',
      description: '',
      creator: 'Alan R. Moon',
      image_route: 'assets/img/ticket_to_ride.jpg',
      release_date: new Date('2004-01-01'),
      min_player: 2,
      max_player: 5,
      average_duration: 60,
      plays: 5
    },
    {
      id: '4',
      name: 'Pandemic',
      description: '',
      creator: 'Matt Leacock',
      image_route: 'assets/img/pandemic.jpg',
      release_date: new Date('2008-01-01'),
      min_player: 2,
      max_player: 4,
      average_duration: 45,
      plays: 5
    },
    {
      id: '5',
      name: '7 Wonders',
      description: '',
      creator: 'Antoine Bauza',
      image_route: 'assets/img/7wonders.jpg',
      release_date: new Date('2010-01-01'),
      min_player: 2,
      max_player: 7,
      average_duration: 30,
      plays: 5
    },
    {
      id: '6',
      name: 'Gloomhaven',
      description: '',
      creator: 'Isaac Childres',
      image_route: 'assets/img/gloomhaven.jpg',
      release_date: new Date('2017-01-01'),
      min_player: 1,
      max_player: 4,
      average_duration: 120,
      plays: 5
    },
    {
      id: '7',
      name: 'Azul',
      description: '',
      creator: 'Michael Kiesling',
      image_route: 'assets/img/azul.jpg',
      release_date: new Date('2017-01-01'),
      min_player: 2,
      max_player: 4,
      average_duration: 30,
      plays: 5
    },
    {
      id: '8',
      name: 'Dixit',
      description: '',
      creator: 'Jean-Louis Roubira',
      image_route: 'assets/img/dixit.jpg',
      release_date: new Date('2008-01-01'),
      min_player: 3,
      max_player: 6,
      average_duration: 30,
      plays: 5
    },
    {
      id: '9',
      name: 'Terraforming Mars',
      description: '',
      creator: 'Jacob Fryxelius',
      image_route: 'assets/img/terraforming_mars.jpg',
      release_date: new Date('2016-01-01'),
      min_player: 1,
      max_player: 5,
      average_duration: 120,
      plays: 5
    },
    {
      id: '10',
      name: 'Scythe',
      description: '',
      creator: 'Jamey Stegmaier',
      image_route: 'assets/img/scythe.jpg',
      release_date: new Date('2016-01-01'),
      min_player: 1,
      max_player: 5,
      average_duration: 115,
      plays: 5
    }
  ];

  get filteredGames(): Tabletop[] {
    return this.tabletops.filter(game =>
      game.name.toLowerCase().includes(this.searchText.toLowerCase())
    );
  }
  goToGame(game: Tabletop) {
    this.router.navigate(['/tabletop-game', game.name], {
      state: { game }
    });
  }
}
