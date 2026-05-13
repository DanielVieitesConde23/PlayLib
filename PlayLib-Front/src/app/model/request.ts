export interface Request {
    id: string;
    username: string;
    gameName: string;
    description: string;
    isTabletop: boolean;
    approved: boolean | null;
}