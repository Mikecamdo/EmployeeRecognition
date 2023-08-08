export interface CommentDto {
    kudoId: number;
    senderId: string;
    message: string;
}
  
export interface Comment {
    id: number;
    kudoId: number;
    senderId: string;
    senderName: string;
    senderAvatar: string;
    message: string;
}