export interface KudoDto {
    senderId: string;
    receiverId: string;
    title: string;
    message: string;
    teamPlayer: boolean;
    oneOfAKind: boolean;
    creative: boolean;
    highEnergy: boolean;
    awesome: boolean;
    achiever: boolean;
    sweetness: boolean;
}
  
export interface Kudo {
    id: number;
    senderName: string;
    senderId: string;
    senderAvatarUrl: string;
    receiverName: string;
    receiverId: string;
    receiverAvatarUrl: string;
    title: string;
    message: string;
    teamPlayer: boolean;
    oneOfAKind: boolean;
    creative: boolean;
    highEnergy: boolean;
    awesome: boolean;
    achiever: boolean;
    sweetness: boolean;
    theDate: string;
}