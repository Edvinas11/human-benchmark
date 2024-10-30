import React, { useState } from "react";

import styles from "./ReactionTest.module.css";
import StartGame from "../../components/StartGame/StartGame";
import ReactionTestLogic from "../../components/ReactionTest/ReactionTestLogic";
import { useAuth } from "../../contexts/AuthContext";

const ReactionTest = () => {
  const { userId } = useAuth();
  const [sessionId, setSessionId] = useState(null);
  const [reactionTime, setReactionTime] = useState<number | null>(null);
  const [showReactionTest, setShowReactionTest] = useState(false);
  const [testStarted, setTestStarted] = useState(false);

  const apiUrl = import.meta.env.VITE_API_URL;

  const startGameSession = async (userId: number) => {
    try {
      const response = await fetch(
        `${apiUrl}/reactiontest/start?userId=${userId}`,
        {
          method: "POST",
        }
      );

      const session = await response.json();
      setSessionId(session.gameSessionId);
      setTestStarted(true);
      setShowReactionTest(true);
      setReactionTime(null);
    } catch (error) {
      console.error("Error starting game session:", error);
    }
  };

  const recordReactionTime = async (
    sessionId: number,
    reactionTime: number
  ) => {
    try {
      const response = await fetch(
        `https://localhost:7028/api/Reactiontest/recordScore`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            userId: userId, // In the future replace with actual userId
            reactionTimeInMilliseconds: reactionTime,
          }),
        }
      );

      const result = await response.json();
    } catch (error) {
      console.error("Error recording reaction time:", error);
    }
  };

  const goBackToStart = () => {
    setShowReactionTest(false);
    setTestStarted(false);
    setReactionTime(null);
    setSessionId(null);
  };


  const handleReactionTestComplete = (reactionTime: number) => {
    setReactionTime(reactionTime);
    setShowReactionTest(false);

    if (sessionId) {
      recordReactionTime(sessionId, reactionTime);
    }
  };

  const handleRestart = () => {
    if (userId) {
      startGameSession(Number(userId)); // Directly start the game session with the user ID
    }
  };

  return (
    <section className={styles.Reaction}>
      <div className={styles.wrapper}>
        <h2>Reaction Test Game</h2>

        {!testStarted && userId && (
          <StartGame startGameSession={startGameSession} />
        )}
        {showReactionTest && (
          <ReactionTestLogic
            onTestComplete={handleReactionTestComplete}
            sessionId={sessionId}
            goBackToStart={goBackToStart}
          />
        )}
        {reactionTime !== null && (
          <div className={styles.resultBox} onClick={handleRestart}>
            <div className={styles.reactionTimeText}>
              Your reaction time: {reactionTime} ms
            </div>
            
            <div className={styles.restartText}>
              Click to restart
            </div>
          </div>
        )}
      </div>
    </section>
  );
};

export default ReactionTest;
