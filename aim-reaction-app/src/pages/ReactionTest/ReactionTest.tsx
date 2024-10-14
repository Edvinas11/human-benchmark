import React, { useState } from 'react'

import styles from './ReactionTest.module.css'
import StartGame from '../../components/StartGame/StartGame';
import ReactionTestLogic from '../../components/ReactionTest/ReactionTestLogic';

const ReactionTest = () => {
  const [sessionId, setSessionId] = useState(null);
  const [reactionTime, setReactionTime] = useState<number | null>(null);
  const [showReactionTest, setShowReactionTest] = useState(false);
  const [testStarted, setTestStarted] = useState(false);

  const apiUrl = import.meta.env.VITE_API_URL;

  const startGameSession = async (userId: number) => {
    try {
      const response = await fetch(`${apiUrl}/reactiontest/start?userId=${userId}`, {
        method: 'POST'
      });

      const session = await response.json();
      setSessionId(session.gameSessionId);
      setTestStarted(true);
      setShowReactionTest(true);
    } catch (error) {
      console.error('Error starting game session:', error);
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
            userId: 1, // In the future replace with actual userId
            reactionTimeInMilliseconds: reactionTime,
          }),
        }
      );

      const result = await response.json();
    } catch (error) {
      console.error("Error recording reaction time:", error);
    }
  };

  const handleReactionTestComplete = (reactionTime: number) => {
    setReactionTime(reactionTime);
    setShowReactionTest(false);
    
    if (sessionId) {
      recordReactionTime(sessionId, reactionTime);
    }
  };

  return (
    <section className={styles.Reaction}>
      <div className={"wrapper"}>
        <h2>Reaction Test Game</h2>

        {!testStarted && <StartGame startGameSession={startGameSession}/>}
        {showReactionTest && <ReactionTestLogic onTestComplete={handleReactionTestComplete} sessionId={sessionId}/>}
        {reactionTime && <p>Your reaction time: {reactionTime} ms</p>}
      </div>
    </section>
  )
}

export default ReactionTest