import React, { useState } from "react";

import styles from "./ReactionTest.module.css";
import StartGame from "../../components/StartGame/StartGame";
import ReactionTestLogic from "../../components/ReactionTest/ReactionTestLogic";
import { useAuth } from "../../contexts/AuthContext";
import { GameType } from "../../components/GameType/GameType";

const ReactionTest = () => {
  const { userId } = useAuth();
  const [sessionId, setSessionId] = useState<number | null>(null);
  const [reactionTime, setReactionTime] = useState<number | null>(null);
  const [showReactionTest, setShowReactionTest] = useState(false);
  const [testStarted, setTestStarted] = useState(false);

  const apiUrl = import.meta.env.VITE_API_URL;

  const startGameSession = async () => {
    if (!userId) {
      console.error("User ID is required to start a game session.");
      return;
    }

    try {
      const response = await fetch(`${apiUrl}/GenericGame/${userId}/start/2`, {
        method: "POST",
      });

      if (!response.ok) {
        throw new Error("Failed to start game session.");
      }

      const session = await response.json();
      setSessionId(session.gameSessionId);
      console.log("Session ID:", session.gameSessionId);

      setTestStarted(true);
      setShowReactionTest(true);
      setReactionTime(null);

      // Fetch active session count
      fetchActiveUserCount();
    } catch (error) {
      console.error("Error starting game session:", error);
    }
  };

  const recordReactionTime = async (sessionId: number, reactionTime: number) => {
    if (!userId) {
      console.error("User ID is required to record reaction time.");
      return;
    }

    try {
      const response = await fetch(`${apiUrl}/GenericGame/${userId}/addscore`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          userId: parseInt(userId),
          value: reactionTime,
          dateAchieved: "2024-12-09T14:30:00Z", // ISO 8601 format
          gameId: 1,
          gameType: GameType.ReactionTimeChallenge, // Assuming 2 is for reaction test
        }),
      });

      if (!response.ok) {
        throw new Error("Failed to record reaction time.");
      }

      const result = await response.json();
      console.log("Reaction time recorded:", result);
    } catch (error) {
      console.error("Error recording reaction time:", error);
    }
  };

  const fetchActiveUserCount = async () => {
    try {
      const response = await fetch(`${apiUrl}/GenericGame/active`, {
        method: "GET",
      });

      if (!response.ok) {
        throw new Error("Failed to fetch active user count.");
      }

      const data = await response.json();
      console.log("Active users:", data.activeSessions);
    } catch (error) {
      console.error("Error fetching active user count:", error);
    }
  };

  const endGameSession = async () => {
    if (!sessionId) {
      console.error("Session ID is required to end the session.");
      return;
    }

    try {
      const response = await fetch(`${apiUrl}/GenericGame/end/${sessionId}`, {
        method: "POST",
      });

      if (!response.ok) {
        throw new Error("Failed to end game session.");
      }

      console.log("Game session ended successfully.");
    } catch (error) {
      console.error("Error ending game session:", error);
    }
  };

  const goBackToStart = () => {
    endGameSession();
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
      endGameSession();
    }
  };

  const handleRestart = () => {
    if (userId) {
      startGameSession();
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
            <div className={styles.restartText}>Click to restart</div>
          </div>
        )}
      </div>
    </section>
  );
};

export default ReactionTest;
