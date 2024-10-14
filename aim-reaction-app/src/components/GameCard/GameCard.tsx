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
    switch (game.gameType) {
        case GameType.MovingTargets:
          navigate('/movingTargets');
          break;
        case GameType.ReflexTest:
          navigate('/reflex-test');
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
         alt={game.gameName}
         className={styles.image}
         onClick = {handleNavigate }
          />
      <h3>{game.gameName}</h3>
      <p>{game.gameDescr}</p>
    </div>
  );
};

export default GameCard;
