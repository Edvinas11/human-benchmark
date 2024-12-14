import React from "react";
import styles from "./startGame.module.css"

const StartGame: React.FC<any> = ({ startGameSession }) => {
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
