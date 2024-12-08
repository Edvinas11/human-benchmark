import React from "react";

import styles from "./GameCard.module.css";

import { GameCardProps  } from "../../types/props"

import GameImage from "../../assets/descr.png"
import { useNavigate } from "react-router-dom";
import { GameType } from "../GameType/GameType";


const GameCard: React.FC<GameCardProps> = ({ game }) => {
  const navigate = useNavigate();

  const handleNavigate = () => {
    console.log("Game object:", game); // Debugging line
    switch (game.gameDescription.gameType) {
        case GameType.MovingTargets:
          navigate('/movingTargets');
          break;
        case GameType.ReflexTest:
          navigate('/reflex-test', { state: { difficulty: game.gameDifficulty } });
          break;
        case GameType.ReactionTimeChallenge:
          navigate('reaction-test');
          break;
        default:
          break;
      
    }
  }
  
  return (
    <div className={styles.card}>
      <img src={GameImage}
         alt={game.gameDescription.gameName}
         className={styles.image}
         onClick = {handleNavigate }
          />
      <h3>{game.gameDescription.gameName}</h3>
      <div>{game.gameDescription.gameDescr}</div>
      <div>{game.gameDifficulty}</div>
    </div>
  );
};

export default GameCard;
