import React, { useState, useEffect} from "react";

import styles from "./ReactionTest.module.css";
import StartGame from "../../components/StartGame/StartGame";
import ReactionTestLogic from "../../components/ReactionTest/ReactionTestLogic";
import { useAuth } from "../../contexts/AuthContext";

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
    console.log("UserID:",userId);
    try {

      const response = await fetch(
        `${apiUrl}/GenericGame/${userId}/start/2`, // 2 = reactiontest
        {
          method: "POST",
        }
      );

      const session = await response.json();
      setSessionId(session.gameSessionId);
      console.log("Session id: ",session.gameSessionId);
      setTestStarted(true);
      setShowReactionTest(true);
      setReactionTime(null);

    // Fetch active session count
    const activeCountResponse = await fetch(`${apiUrl}/GenericGame/active`);
    const activeCountData = await activeCountResponse.json();
    console.log("Active users:", activeCountData.activeSessions);
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

  const fetchActiveUserCount = async () => {
    try {
        const response = await fetch(`${apiUrl}/GenericGame/active/count`, {
            method: "GET",
        });

        if (!response.ok) {
            throw new Error("Failed to fetch active user count.");
        }

        const data = await response.json();
        console.log("Active users: from fetch", data);
    } catch (error) {
        console.error("Error fetching active user count:", error);
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
      //recordReactionTime(sessionId, reactionTime);
      endGameSession();
    }
  };

  const handleRestart = () => {
    if (userId) {
      startGameSession(); // Directly start the game session with the user ID
    }
  };

  const endGameSession = async () => {
    if (!sessionId) {
      console.error("Session ID is required to end the session.");
      return;
    }
    try {
      console.log("end" ,sessionId);
      const response = await fetch(
        `${apiUrl}/GenericGame/end/${sessionId}`,
        {
          method: "POST",
        }
      );
      if (response.ok) {
        console.log("Game session ended successfully.");
      } else {
        console.error("Failed to end game session.");
      }
    } catch (error) {
      console.error("Error ending game session:", error);
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
