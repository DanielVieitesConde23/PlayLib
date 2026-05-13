import { Tag } from "./tag";
import { Language } from "./language";

export interface Videogame {
  id: string;
  name: string;
  description: string;
  developer: string;
  image_route: string;
  release_date: Date;
  format: string;
  state: string;
  tags: Tag[];
  languages: Language[];
  isInLibrary: boolean;
  isFavourite: boolean;
}