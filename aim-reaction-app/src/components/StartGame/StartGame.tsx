import React from "react";
import Button from "../Button/Button";
import styles from "./startGame.module.css"
import { useAuth } from "../../contexts/AuthContext";

const StartGame: React.FC<any> = ({ startGameSession }) => {
  const { userId } = useAuth();

  return (
    <div
      className={styles.startBox}
      onClick={startGameSession}
    >
      Press to Start
    </div>
  );
};

export default StartGame;
