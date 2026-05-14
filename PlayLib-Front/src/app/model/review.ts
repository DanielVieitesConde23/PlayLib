export interface Review {
    id: string;
    username: string;
    userId: string;
    userImage: string;
    gameId: string;
    review_date: Date;
    content: string;
    rating: number;
}