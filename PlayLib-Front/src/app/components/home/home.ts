import { Component } from '@angular/core';
import { Videogame } from '../../model/videogame';
import { Tabletop } from '../../model/tabletop';
import { ScrollableListVg } from "../scrollable-list-vg/scrollable-list-vg";
import { ScrollableListTg } from "../scrollable-list-tg/scrollable-list-tg";

@Component({
  selector: 'app-home',
  imports: [
    ScrollableListVg,
    ScrollableListTg
  ],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {

  videogames: Videogame[] = [
    {
      id: '1',
      name: 'Undertale',
      description: '',
      developer: 'Toby Fox',
      image_route: 'https://i.imgur.com/EEnkEEf.jpg',
      release_date: new Date('2015-09-15'),
      format: 'Digital',
      state: 'Played'
    },
    {
      id: '2',
      name: 'Deltarune',
      description: '',
      developer: 'Toby Fox',
      image_route: 'https://i.imgur.com/Sfh0vjn.jpg',
      release_date: new Date('2018-10-31'),
      format: 'Digital',
      state: 'Played'
    },
    {
      id: '3',
      name: 'Red Dead Redemption 2',
      description: '',
      developer: 'Rockstar Games',
      image_route: 'assets/img/rdr2.jpg',
      release_date: new Date('2018-10-26'),
      format: 'PC',
        state: 'Played'
    },
    {
      id: '4',
      name: 'Elden Ring',
      description: '',
      developer: 'FromSoftware',
      image_route: 'assets/img/eldenring.jpg',
      release_date: new Date('2022-02-25'),
      format: 'PC',
        state: 'Played'
    },
    {
      id: '5',
      name: 'God of War',
      description: '',
      developer: 'Santa Monica Studio',
      image_route: 'assets/img/godofwar.jpg',
      release_date: new Date('2018-04-20'),
      format: 'PlayStation',
        state: 'Played'
    },
    {
      id: '6',
      name: 'Horizon Zero Dawn',
      description: '',
      developer: 'Guerrilla Games',
      image_route: 'assets/img/horizon.jpg',
      release_date: new Date('2017-02-28'),
      format: 'PC',
        state: 'Played'
    },
    {
      id: '7',
      name: 'The Last of Us Part II',
      description: '',
      developer: 'Naughty Dog',
      image_route: 'assets/img/tlou2.jpg',
      release_date: new Date('2020-06-19'),
      format: 'PlayStation',
        state: 'Played'
    },
    {
      id: '8',
      name: 'Assassin’s Creed Valhalla',
      description: '',
      developer: 'Ubisoft',
      image_route: 'assets/img/valhalla.jpg',
      release_date: new Date('2020-11-10'),
      format: 'PC',
        state: 'Played'
    },
    {
      id: '9',
      name: 'Minecraft',
      description: '',
      developer: 'Mojang',
      image_route: 'assets/img/minecraft.jpg',
      release_date: new Date('2011-11-18'),
      format: 'Multiplatform',
        state: 'Played'
    },
    {
      id: '10',
      name: 'Grand Theft Auto V',
      description: '',
      developer: 'Rockstar Games',
      image_route: 'assets/img/gtav.jpg',
      release_date: new Date('2013-09-17'),
      format: 'PC',
        state: 'Played'
    }
  ];

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

  scroll(container: HTMLElement, direction: number) {
    container.scrollBy({
      left: direction * 300,
      behavior: 'smooth'
    });
  }

}
