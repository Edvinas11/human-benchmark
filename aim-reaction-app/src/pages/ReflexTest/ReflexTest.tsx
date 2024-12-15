import React, { useState, useEffect } from "react";
import styles from "./ReflexTest.module.css";
import { useAuth } from "../../contexts/AuthContext";
import { useSearchParams, useNavigate } from "react-router-dom";
import hitSound from "../../assets/Target-sound.mp3";
import countSound from "../../assets/CountDown.mp3";
import gameSound from "../../assets/GameStart-Sound.mp3";

const DIFFICULTY_SETTINGS = {
  easy: { spawnInterval: 1200, expiryTime: 1200 },
  medium: { spawnInterval: 800, expiryTime: 800 },
  hard: { spawnInterval: 500, expiryTime: 500 },
};

const ReflexTest: React.FC = () => {
  const { userId } = useAuth();
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();

  const gameId = parseInt(searchParams.get("gameId") || "0", 10); // Default to 0 if invalid
  const difficultyParam = searchParams.get("difficulty") || "easy";

  const [difficulty, setDifficulty] = useState<"easy" | "medium" | "hard">("easy");
  const [sessionId, setSessionId] = useState<number | null>(null);
  const [target, setTarget] = useState<number | null>(null);
  const [score, setScore] = useState(0);
  const [missedTargets, setMissedTargets] = useState(0);
  const [gameActive, setGameActive] = useState(false);
  const [expiryTimeout, setExpiryTimeout] = useState<NodeJS.Timeout | null>(null);
  const [countdown, setCountdown] = useState<number | string | null>(null);

  const sanitizedUserId = userId ? parseInt(userId, 10) : null;
  const apiUrl = import.meta.env.VITE_API_URL;

  useEffect(() => {
    if (!gameId || isNaN(gameId)) {
      console.error("Invalid or missing gameId in URL query.");
      navigate("/"); // Redirect to a fallback route
    }

    if (["easy", "medium", "hard"].includes(difficultyParam)) {
      setDifficulty(difficultyParam as "easy" | "medium" | "hard");
    } else {
      console.error("Invalid difficulty level in query parameters.");
    }
  }, [gameId, difficultyParam, navigate]);

  const startGameSession = async () => {
    if (!sanitizedUserId || !gameId) {
      console.error("User ID and gameId are required to start a game session.");
      return;
    }

    try {
      const response = await fetch(
        `${apiUrl}/GenericGame/${sanitizedUserId}/start/1`,
        { method: "POST" }
      );

      if (!response.ok) {
        throw new Error(`Failed to start game session: ${response.statusText}`);
      }

      const session = await response.json();
      setSessionId(session.gameSessionId);
      setGameActive(true);
      console.log("Session ID:", session.gameSessionId);
    } catch (error) {
      console.error("Error starting game session:", error);
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
        throw new Error(`Failed to end game session: ${response.statusText}`);
      }

      const result = await response.json();
      console.log("Session ended successfully:", result);
    } catch (error) {
      console.error("Error ending game session:", error);
    }
  };

  const saveScore = async (reactionScore: number) => {
    if (!sanitizedUserId || !gameId) {
      console.error("User ID and gameId are required to save the score.");
      return;
    }

    const scoreData = {
      userId: sanitizedUserId,
      value: reactionScore,
      dateAchieved: new Date().toISOString(),
      gameId,
      gameType: 1, // Assuming gameType 1 for Reflex Test
    };

    try {
      const response = await fetch(`${apiUrl}/GenericGame/${sanitizedUserId}/addscore`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(scoreData),
      });

      if (!response.ok) {
        throw new Error(`Error saving score: ${response.statusText}`);
      }

      const data = await response.json();
      console.log("Score saved:", data);
    } catch (error) {
      console.error("Error saving score:", error);
    }
  };

  const handleStartGame = () => {
    setScore(0);
    setMissedTargets(0);
    setCountdown(3);

    let countdownValue = 3;
    const countdownInterval = setInterval(() => {
      if (countdownValue > 1) {
        const countdownSound = new Audio(countSound);
        countdownSound.play();
      }
      countdownValue -= 1;
      if (countdownValue === 0) {
        const startSound = new Audio(gameSound);
        startSound.play();
        setCountdown("Go!");
        setTimeout(() => {
          setCountdown(null);
          startGameSession(); // Start the session when countdown ends
        }, 1000);
        clearInterval(countdownInterval);
      } else {
        setCountdown(countdownValue);
      }
    }, 1000);
  };

  const handleHitTarget = () => {
    const targetHit = new Audio(hitSound);
    targetHit.play();

    setScore((prev) => prev + 1);
    setTarget(null);

    if (expiryTimeout) {
      clearTimeout(expiryTimeout);
      setExpiryTimeout(null);
    }
  };

  const handleStopGame = async () => {
    setGameActive(false);
    if (expiryTimeout) {
      clearTimeout(expiryTimeout);
    }

    try {
      await saveScore(score);
      await endGameSession();
    } catch (error) {
      console.error("Error handling game stop:", error);
    }
  };

  useEffect(() => {
    if (gameActive && target === null && missedTargets < 3) {
      const { spawnInterval, expiryTime } = DIFFICULTY_SETTINGS[difficulty];
      const spawnTimer = setTimeout(() => {
        setTarget(Date.now());
        const expiryTimer = setTimeout(() => {
          setTarget(null);
          setMissedTargets((prev) => prev + 1);
        }, expiryTime);
        setExpiryTimeout(expiryTimer);
      }, spawnInterval);

      return () => {
        clearTimeout(spawnTimer);
        if (expiryTimeout) clearTimeout(expiryTimeout);
      };
    } else if (missedTargets >= 3 && gameActive) {
      handleStopGame();
    }
  }, [gameActive, target, missedTargets, difficulty]);

  return (
    <div className={styles.container}>
      <h2>Reflex Test</h2>
      {!gameActive && countdown === null ? (
        <button onClick={handleStartGame}>Start Game</button>
      ) : (
        <div className={styles.scoreRow}>
          <span className={styles.score}>Score: {score}</span>
          <span className={styles.missedTargets}>Missed: {missedTargets} / 3</span>
          <button onClick={handleStopGame}>Stop Game</button>
        </div>
      )}
      <div className={styles.targetArea}>
        {countdown !== null && <div className={styles.countdown}>{countdown}</div>}
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
