import React from "react";
import Button from "../Button/Button";
import { useAuth } from "../../contexts/AuthContext";

const StartGame: React.FC<any> = ({ startGameSession }) => {
  const { userId } = useAuth();

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
