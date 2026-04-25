export interface Request {
    id: string;
    username: string;
    title: string;
    description: string;
    isTabletop: boolean;
    approved: boolean | null;
}