import { Component, inject, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogClose, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule, provideNativeDateAdapter } from '@angular/material/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Details } from '../../services/details';
import { ConfirmDialog } from '../confirm-dialog/confirm-dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { TagLanguageService } from '../../services/tag-language';
import { Tag } from '../../model/tag';
import { Language } from '../../model/language';

@Component({
  selector: 'app-dialog-request-mail',
  standalone: true,
  imports: [MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogClose, MatButtonModule, FormsModule, MatSelectModule, MatInputModule, MatRadioModule, MatDatepickerModule, MatNativeDateModule, MatSnackBarModule, MatCheckboxModule],
  providers: [provideNativeDateAdapter()],
  templateUrl: './dialog-request-mail.html',
  styleUrl: './dialog-request-mail.css',
  encapsulation: ViewEncapsulation.None
})
export class DialogRequestMail implements OnInit {
  data = inject(MAT_DIALOG_DATA);
  requestSeleccionada: any = null;
  gameType: 'videogame' | 'tabletop' = 'videogame';

  allTags: Tag[] = [];
  allLanguages: Language[] = [];
  selectedTags: string[] = [];
  selectedLanguages: string[] = [];

  gameData: any = {
    name: '',
    description: '',
    developer: '',
    creator: '',
    image_Route: '',
    release_Date: new Date(),
    minPlayerNumber: 1,
    maxPlayerNumber: 4,
    averageDuration: 60
  };

  constructor(private detailsSvc: Details, private dialog: MatDialog, private snackBar: MatSnackBar, private tagLanguageSvc: TagLanguageService) { }

  ngOnInit() {
    this.tagLanguageSvc.getAllTags().subscribe({
      next: (tags) => this.allTags = tags,
      error: () => this.snackBar.open('Error cargando etiquetas', 'Cerrar', { duration: 3000 })
    });
    this.tagLanguageSvc.getAllLanguages().subscribe({
      next: (langs) => this.allLanguages = langs,
      error: () => this.snackBar.open('Error cargando idiomas', 'Cerrar', { duration: 3000 })
    });
  }

  onSelectionChange() {
    if (this.requestSeleccionada) {
      this.gameData.name = this.requestSeleccionada.gameName;
      this.gameData.description = this.requestSeleccionada.description;
      this.gameType = this.requestSeleccionada.isTabletop ? 'tabletop' : 'videogame';
    }
  }

  toggleTag(id: string) {
    const idx = this.selectedTags.indexOf(id);
    if (idx >= 0) this.selectedTags.splice(idx, 1);
    else this.selectedTags.push(id);
  }

  toggleLanguage(id: string) {
    const idx = this.selectedLanguages.indexOf(id);
    if (idx >= 0) this.selectedLanguages.splice(idx, 1);
    else this.selectedLanguages.push(id);
  }

  isTagSelected(id: string) { return this.selectedTags.includes(id); }
  isLanguageSelected(id: string) { return this.selectedLanguages.includes(id); }

  addGame() {
    if (!this.gameData.name || !this.gameData.description || !this.gameData.image_Route || !this.gameData.release_Date) {
      this.snackBar.open('Por favor, rellena todos los campos obligatorios', 'Cerrar', { duration: 3000 });
      return;
    }

    if (this.gameType === 'videogame' && !this.gameData.developer) {
      this.snackBar.open('Desarrollador es obligatorio', 'Cerrar', { duration: 3000 });
      return;
    }

    if (this.gameType === 'tabletop' && (!this.gameData.creator || !this.gameData.minPlayerNumber || !this.gameData.maxPlayerNumber || !this.gameData.averageDuration)) {
      this.snackBar.open('Creador, jugadores y duración son obligatorios', 'Cerrar', { duration: 3000 });
      return;
    }

    const lowerImage = this.gameData.image_Route.toLowerCase();
    if (!lowerImage.endsWith('.png') && !lowerImage.endsWith('.jpg') && !lowerImage.endsWith('.jpeg')) {
      this.snackBar.open('El enlace de la imagen debe terminar en .png, .jpg o .jpeg', 'Cerrar', { duration: 3000 });
      return;
    }

    const confirmRef = this.dialog.open(ConfirmDialog, {
      data: { title: 'Confirmar acción', message: '¿Estás seguro de que quieres agregar este juego?' }
    });

    confirmRef.afterClosed().subscribe(confirm => {
      if (confirm) {
        if (this.gameType === 'videogame') {
          const dto = {
            Name: this.gameData.name,
            Description: this.gameData.description,
            Developer: this.gameData.developer,
            Image_Route: this.gameData.image_Route,
            Release_Date: this.gameData.release_Date,
            Tags: this.selectedTags,
            Languages: this.selectedLanguages
          };
          this.detailsSvc.createVideogame(dto).subscribe({
            next: () => {
              this.snackBar.open('Videojuego creado con éxito', 'Cerrar', { duration: 3000 });
            },
            error: () => {
              this.snackBar.open('Error creando el videojuego', 'Cerrar', { duration: 3000 });
            }
          });
        } else {
          const dto = {
            Name: this.gameData.name,
            Description: this.gameData.description,
            Creator: this.gameData.creator,
            Image_Route: this.gameData.image_Route,
            Release_Date: this.gameData.release_Date,
            MinPlayerNumber: this.gameData.minPlayerNumber,
            MaxPlayerNumber: this.gameData.maxPlayerNumber,
            AverageDuration: this.gameData.averageDuration,
            Tags: this.selectedTags,
            Languages: this.selectedLanguages
          };
          this.detailsSvc.createTabletopGame(dto).subscribe({
            next: () => {
              this.snackBar.open('Juego de mesa creado correctamente', 'Cerrar', { duration: 3000 });
            },
            error: () => {
              this.snackBar.open('Error creando el juego de mesa', 'Cerrar', { duration: 3000 });
            }
          });
        }
      }
    });
  }
}
