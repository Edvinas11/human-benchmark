import React, { useEffect, useState } from "react";
import Button from "../Button/Button";

const ReactionTest: React.FC<any> = ({ onTestComplete, sessionId }) => {
  const [waitingForClick, setWaitingForClick] = useState(false);
  const [startTime, setStartTime] = useState<number | null>(null);
  const [reactionTime, setReactionTime] = useState<number | null>(null);

  const apiUrl = import.meta.env.VITE_API_URL;

  useEffect(() => {
    const randomDelay = Math.random() * 5000 + 2000; // 2-7s delay

    const timeout = setTimeout(() => {
      setWaitingForClick(true);
      setStartTime(Date.now());
    }, randomDelay);

    return () => clearTimeout(timeout);
  }, []);

  const handleClick = async () => {
    if (startTime !== null) {
      const reactionTime = Date.now() - startTime;
      setReactionTime(reactionTime);
      setWaitingForClick(false);

      await endGameSession(sessionId);

      onTestComplete(reactionTime);
    }
  };

  const endGameSession = async (sessionId: number) => {
    

    try {
      const response = await fetch(
        `${apiUrl}/reflexTest/end/${sessionId}`,
        {
          method: "POST",
        }
      );

      if (!response.ok) {
        throw new Error("Failed to end the session");
      }

      const result = await response.json();
      console.log("Session ended successfully:", result);
    } catch (error) {
      console.error("Error ending the session:", error);
    }
  };

  return (
    <div>
      {waitingForClick ? (
        <Button label="Click Now!" variant="secondary" onClick={handleClick} />
      ) : (
        <p>Wait for it...</p>
      )}
    </div>
  );
};

export default ReactionTest;
