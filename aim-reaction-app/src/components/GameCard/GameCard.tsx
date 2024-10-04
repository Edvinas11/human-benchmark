import React from "react";

import styles from "./GameCard.module.css";

import { GameCardProps  } from "../../types/props"

import GameImage from "../../assets/descr.png"

const GameCard: React.FC<GameCardProps> = ({ game }) => {
  return (
    <div className={styles.card}>
      <img src={GameImage} alt={game.gameName} className={styles.image} />
      <h3>{game.gameName}</h3>
      <p>{game.gameDescr}</p>
    </div>
  );
};

export default GameCard;
