import { GameType } from "../components/GameType/GameType";

export interface ButtonProps {
    label: string;
    variant: string;
    onClick?: () => void;
  }

export interface Game {
  gameId: number;
  gameName: string;
  gameDescr: string;
  gameType: GameType;
}

export interface GameCardProps {
  game: Game;
}

export interface Score{
    score: number;
    userEmail: string;
    userId: number;
    userName: string;
}
