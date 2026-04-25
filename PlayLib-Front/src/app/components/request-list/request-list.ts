import { Component } from '@angular/core';
import { MatIcon } from "@angular/material/icon";
import { Request } from "../../model/request";
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-request-list',
  imports: [MatIcon, MatCardModule],
  templateUrl: './request-list.html',
  styleUrl: './request-list.css',
})
export class RequestList {

  requests: Request[] = [
  {
    id: 'req-1',
    username: 'usuario_1',
    title: 'Solicitud de ejemplo 1',
    description: 'Descripción de prueba 1',
    isTabletop: true,
    approved: null
  },
  {
    id: 'req-2',
    username: 'usuario_2',
    title: 'Solicitud de ejemplo 2',
    description: 'Descripción de prueba 2',
    isTabletop: false,
    approved: true
  },
  {
    id: 'req-3',
    username: 'usuario_3',
    title: 'Solicitud de ejemplo 3',
    description: 'Descripción de prueba 3',
    isTabletop: true,
    approved: false
  },
  {
    id: 'req-4',
    username: 'usuario_4',
    title: 'Solicitud de ejemplo 4',
    description: 'Descripción de prueba 4',
    isTabletop: false,
    approved: false
  },
  {
    id: 'req-5',
    username: 'usuario_5',
    title: 'Solicitud de ejemplo 5',
    description: 'Descripción de prueba 5',
    isTabletop: true,
    approved: true
  },
  {
    id: 'req-6',
    username: 'usuario_6',
    title: 'Solicitud de ejemplo 6',
    description: 'Descripción de prueba 6',
    isTabletop: false,
    approved: false
  },
  {
    id: 'req-7',
    username: 'usuario_7',
    title: 'Solicitud de ejemplo 7',
    description: 'Descripción de prueba 7',
    isTabletop: true,
    approved: true
  },
  {
    id: 'req-8',
    username: 'usuario_8',
    title: 'Solicitud de ejemplo 8',
    description: 'Descripción de prueba 8',
    isTabletop: false,
    approved: false
  },
  {
    id: 'req-9',
    username: 'usuario_9',
    title: 'Solicitud de ejemplo 9',
    description: 'Descripción de prueba 9',
    isTabletop: true,
    approved: false
  },
  {
    id: 'req-10',
    username: 'usuario_10',
    title: 'Solicitud de ejemplo 10',
    description: 'Descripción de prueba 10',
    isTabletop: false,
    approved: true
  }
];
}
