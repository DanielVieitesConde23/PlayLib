import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Tabletop } from '../../model/tabletop';
@Component({
  selector: 'app-tabletop-game-component',
  imports: [CommonModule],
  templateUrl: './tabletop-game-component.html',
  styleUrl: './tabletop-game-component.css',
})
export class TabletopGameComponent {
    tabletop: Tabletop = {
      id: '1',
      name: 'Unstable Unicorns',
      description: 'Unstable Unicorns es un juego de cartas estratégico por turnos sobre tus dos cosas favoritas: los unicornios y la destrucción. ¡El primer jugador que consiga tener 7 Unicornios de cualquier tipo en su Establo será el ganador!',
      creator: 'Unstable Games',
      image_route: 'https://i.imgur.com/zJ6oPoP.jpg',
      release_date: new Date('2017-01-01'),
      min_player: 2,
      max_player: 8,
      average_duration: 30,
      plays: 5
    };
}
