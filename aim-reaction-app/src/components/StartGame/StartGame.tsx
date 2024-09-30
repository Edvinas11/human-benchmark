import React from "react";
import Button from "../Button/Button";

const StartGame: React.FC<any> = ({ startGameSession }) => {
  const userId = 1;

  return (
    <>
      <Button
        label="Start Test"
        variant="primary"
        onClick={() => startGameSession(userId)}
      />
    </>
  );
};

export default StartGame;
