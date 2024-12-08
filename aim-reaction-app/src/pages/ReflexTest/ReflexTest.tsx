import React, { useState, useEffect } from "react";
import styles from "./ReflexTest.module.css";
import { useAuth } from "../../contexts/AuthContext";
import { useLocation } from "react-router-dom";

const DIFFICULTY_SETTINGS = {
  easy: { spawnInterval: 1200, expiryTime: 1200 },
  medium: { spawnInterval: 800, expiryTime: 800 },
  hard: { spawnInterval: 500, expiryTime: 500 },
};

const ReflexTest: React.FC = () => {
  const { userId } = useAuth();
  const [target, setTarget] = useState<number | null>(null);
  const [score, setScore] = useState(0);
  const [missedTargets, setMissedTargets] = useState(0);
  const [gameActive, setGameActive] = useState(false);
  const [sessionId, setSessionId] = useState<number | null>(null);
  const [expiryTimeout, setExpiryTimeout] = useState<NodeJS.Timeout | null>(null);

  const location = useLocation();
  const [difficulty, setDifficulty] = useState<"easy" | "medium" | "hard">("easy");

  const apiUrl = import.meta.env.VITE_API_URL;

  useEffect(() => {
    if (location.state?.difficulty) {
      setDifficulty(location.state.difficulty as "easy" | "medium" | "hard");
    }
  }, [location.state]);

  useEffect(() => {
    if (gameActive) {
      startGameSession();
    } else if (sessionId !== null) {
      endGameSession(sessionId);
    }
  }, [gameActive]);

  useEffect(() => {
    if (gameActive && target === null && missedTargets < 3) {
      const { spawnInterval, expiryTime } = DIFFICULTY_SETTINGS[difficulty];

      const spawnTimer = setTimeout(() => {
        setTarget(Date.now());

        const expiryTimer = setTimeout(() => {
          setTarget(null);
          setMissedTargets((prev) => prev + 1); // Increment missed targets
        }, expiryTime);

        setExpiryTimeout(expiryTimer);
      }, spawnInterval);

      return () => {
        clearTimeout(spawnTimer);
        if (expiryTimeout) clearTimeout(expiryTimeout);
      };
    } else if (missedTargets >= 3) {
      handleStopGame(); // Stop the game when 3 targets are missed
    }
  }, [gameActive, target, difficulty, missedTargets]);

  const startGameSession = async () => {
    if (!userId) {
      console.error("User ID is required to start a game session.");
      return;
    }
    console.log("UserID:", userId);
    try {
      const response = await fetch(
        `${apiUrl}/GenericGame/${userId}/start/1`, // 1 = reactiontest
        { method: "POST" }
      );
      if (!response.ok) {
        throw new Error("Failed to start game session");
      }

      const result = await response.json();
      setSessionId(result.gameSessionId);
    } catch (error) {
      console.error("Error starting game session:", error);
    }
  };

  const endGameSession = async (sessionId: number) => {
    try {
      const response = await fetch(`${apiUrl}/GenericGame/end/${sessionId}`, {
        method: "POST",
      });

      if (!response.ok) {
        throw new Error("Failed to end game session");
      }

      const result = await response.json();
      console.log("Session ended successfully:", result);
    } catch (error) {
      console.error("Error ending game session:", error);
    }
  };

  const handleHitTarget = () => {
    setScore((prev) => prev + 1);
    setTarget(null);

    if (expiryTimeout) {
      clearTimeout(expiryTimeout);
      setExpiryTimeout(null);
    }
  };

  const handleStartGame = () => {
    setScore(0);
    setMissedTargets(0);
    setGameActive(true);
    setTarget(null);
  };

  const handleStopGame = () => {
    setGameActive(false);
    setTarget(null);

    if (expiryTimeout) {
      clearTimeout(expiryTimeout);
      setExpiryTimeout(null);
    }
  };

  return (
    <div className={styles.container}>
      <h2>Reflex Test</h2>

      {!gameActive ? (
        <div>
          <button onClick={handleStartGame}>Start Game</button>
        </div>
      ) : (
        <div>
          <p>Score: {score}</p>
          <p>Missed Targets: {missedTargets} / 3</p>
          <button onClick={handleStopGame}>Stop Game</button>
        </div>
      )}

      <div className={styles.targetArea}>
        {target !== null && (
          <div
            className={styles.target}
            style={{
              top: `${Math.random() * 80}%`,
              left: `${Math.random() * 80}%`,
            }}
            onClick={handleHitTarget}
          />
        )}
      </div>
    </div>
  );
};

export default ReflexTest;
