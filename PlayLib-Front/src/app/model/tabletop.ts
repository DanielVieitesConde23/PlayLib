import { Tag } from "./tag";
import { Language } from "./language";

export interface Tabletop {
  id: string;
  name: string;
  description: string;
  creator: string;
  image_route: string;
  release_date: Date;
  min_player: number;
  max_player: number;
  average_duration: number;
  plays: number;
  tags: Tag[];
  languages: Language[];
  isInLibrary: boolean;
  isFavourite: boolean;
}
