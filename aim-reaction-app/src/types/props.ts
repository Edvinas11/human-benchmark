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
    UserId: string;
    UserName: string;
    GameName: string;
    Score: int;
}
