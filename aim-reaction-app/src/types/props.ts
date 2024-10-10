export interface ButtonProps {
    label: string;
    variant: string;
    onClick?: () => void;
  }

export interface Game {
  gameId: number;
  gameName: string;
  gameDescr: string;
}

export interface GameCardProps {
  game: Game;
}

export interface Score{
    userId: number;
    userName: string;
    userEmail: string;
    score: number;
    dateAchieved: string;
    gameType: string;
}
