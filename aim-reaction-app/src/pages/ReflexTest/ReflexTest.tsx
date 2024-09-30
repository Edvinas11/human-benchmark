import React, { useState } from 'react'

import styles from './ReflexTest.module.css'
import StartGame from '../../components/StartGame/StartGame';
import ReactionTest from '../../components/ReactionTest/ReactionTest';

const ReflexTest = () => {
  const [sessionId, setSessionId] = useState(null);
  const [reactionTime, setReactionTime] = useState<number | null>(null);
  const [showReactionTest, setShowReactionTest] = useState(false);
  const [testStarted, setTestStarted] = useState(false);

  const startGameSession = async (userId: number) => {
    try {
      const response = await fetch(`https://localhost:7028/api/reflextest/start?userId=${userId}`, {
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

  const handleReactionTestComplete = (reactionTime: number) => {
    setReactionTime(reactionTime);
    setShowReactionTest(false);
  };

  return (
    <section className={styles.reflex}>
      <div className={"wrapper"}>
        <h2>Reflex Test Game</h2>

        {!testStarted && <StartGame startGameSession={startGameSession}/>}
        {showReactionTest && <ReactionTest onTestComplete={handleReactionTestComplete}/>}
      </div>
    </section>
  )
}

export default ReflexTest