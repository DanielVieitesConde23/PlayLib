import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Videogame } from '../../model/videogame';

@Component({
  selector: 'app-videogame-component',
  imports: [CommonModule],
  templateUrl: './videogame-component.html',
  styleUrl: './videogame-component.css',
})
export class VideogameComponent {

    videogame: Videogame = {
        id: '1',
        name: 'Undertale',
        description: 'Undertale es un videojuego de rol en 2D de 2015 creado por el desarrollador independiente estadounidense Toby Fox. El jugador controla a un niño que ha caído al subsuelo: una gran región aislada bajo la superficie de la Tierra, separada por una barrera mágica. ',
        developer: 'Toby Fox',
        image_route: 'https://i.imgur.com/EEnkEEf.jpg',
        release_date: new Date('2015-09-15'),
        format: 'Digital',
        state: 'Played'  
    };
}
