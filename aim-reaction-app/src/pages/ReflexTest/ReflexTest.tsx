import React, { useState, useEffect } from "react";
import styles from "./ReflexTest.module.css";
import { useAuth } from "../../contexts/AuthContext";
import { useLocation } from "react-router-dom";
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
  const [target, setTarget] = useState<number | null>(null);
  const [score, setScore] = useState(0);
  const [missedTargets, setMissedTargets] = useState(0);
  const [gameActive, setGameActive] = useState(false);
  const [sessionId, setSessionId] = useState<number | null>(null);
  const [expiryTimeout, setExpiryTimeout] = useState<NodeJS.Timeout | null>(null);
  const [countdown, setCountdown] = useState<number | null>(null);

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
      if (gameActive) {
        handleStopGame();
      }
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
     const targetHit = new Audio(hitSound);
     targetHit.play();
    
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
    setTarget(null);
    setCountdown(3);

      let countdownValue = 4;
      const countdownInterval = setInterval(() => {
          if (countdownValue > 1) {
              const countdownSound = new Audio(countSound);
              countdownSound.play();
          }
          countdownValue -= 1;
          if (countdownValue === 0) {
              const startSound = new Audio(gameSound);
              startSound.play();
              setCountdown("Go!"); // Show "Go!" for 1 second
              setTimeout(() => {
                  setCountdown(null); // Clear countdown
                  setGameActive(true); // Start the game
              }, 1000);
              clearInterval(countdownInterval);
          } else {
              setCountdown(countdownValue);
          }
      }, 1000);
    };

  const handleStopGame = async () =>  { 
    setGameActive(false);
    setTarget(null);

    if (expiryTimeout) {
      clearTimeout(expiryTimeout);
      setExpiryTimeout(null);
    }

    const scoreData = {
      userId: userId, 
      value: score, 
      dateAchieved: new Date().toISOString(),
      gameId: 1,
      gameType: 1, 
    };
  
    try {
      await saveScore(scoreData);
    } catch (error) {
      console.error("Error saving score:", error);
    }
  };

  const saveScore = async (score: any) => {
    try {
      const response = await fetch(`https://localhost:8080/api/Game/${score.userId}/addscore?value=${score.value}&dateAchieved=${score.dateAchieved}&gameId=${score.gameId}&gameType=${score.gameType}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(score),  
      });
  
      if (!response.ok) {
        throw new Error(`Error saving score: ${response.statusText}`);
      }
  
      const data = await response.json();
      console.log('Score saved:', data);
    } catch (error) {
      console.error('Error saving score:', error);
    }
  };

    return (
    <div className={styles.container}>
        <h2>Reflex Test</h2>

        {!gameActive && countdown === null ? (
            <div>
                <button onClick={handleStartGame}>Start Game</button>
            </div>
        ) : (
            <div className={styles.scoreRow}>
                <span className={styles.score}> Score: {score} </span>
                <span className = {styles.missedTargets}>Missed Targets: {missedTargets} / 3</span>
                <button onClick={handleStopGame}>Stop Game</button>
            </div>
        )}

        <div className={styles.targetArea}>
            {countdown !== null && (
                <div className={styles.countdown}>{countdown}</div>
            )}

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

