import React, { useState } from "react";

import styles from "./ConfigForm.module.css";
import Button from "../Button/Button";

const ConfigForm = () => {
  const [gameName, setGameName] = useState("");
  const [duration, setDuration] = useState("");
  const [targets, setTargets] = useState("");
  const [difficulty, setDifficulty] = useState("");
  const [speed, setSpeed] = useState("");
  const [type, setType] = useState("");

  return (
    <form className={styles.form}>
      <h2>New Game</h2>
      <p>Please specify required configuration to create a game.</p>

      <div className={styles.formContent}>
        <div className={styles.inputItem}>
          <label htmlFor="gameName">Game Name</label>
          <input
            type="text"
            id="gameName"
            placeholder="Enter a name"
            value={gameName}
            onChange={(e) => setGameName(e.target.value)}
            className={styles.input}
            minLength={2}
            maxLength={50}
            required
          />
        </div>

        <div className={styles.inputItem}>
          <label htmlFor="type">Game type</label>
          <select
            id="type"
            value={type}
            onChange={(e) => setType(e.target.value)}
            className={styles.input}
            required
          >
            <option value="">Choose a game type</option>
            <option value="MovingTargets">Moving Targets</option>
            <option value="ReflexTest">Reflextest</option>
            <option value="CustomChallenge">Custom Challenge</option>
          </select>
        </div>

        <div className={styles.inputItem}>
          <label htmlFor="duration">Duration (s)</label>
          <input
            type="number"
            id="duration"
            placeholder="Enter a duration"
            value={duration}
            onChange={(e) => setDuration(e.target.value)}
            className={styles.input}
            min={30}
            max={300}
            required
          />
        </div>

        <div className={styles.inputItem}>
          <label htmlFor="targets"># of Targets</label>
          <input
            type="number"
            id="targets"
            placeholder="Enter the number of targets"
            value={targets}
            onChange={(e) => setTargets(e.target.value)}
            className={styles.input}
            min={5}
            max={200}
            required
          />
        </div>

        <div className={styles.inputItem}>
          <label htmlFor="speed">Speed of Targets</label>
          <input
            type="number"
            id="speed"
            placeholder="Enter the speed of targets"
            value={speed}
            onChange={(e) => setSpeed(e.target.value)}
            className={styles.input}
            min={1}
            max={50}
            required
          />
        </div>

        <div className={styles.inputItem}>
          <label htmlFor="difficulty">Difficulty Level</label>
          <select
            id="difficulty"
            value={difficulty}
            onChange={(e) => setDifficulty(e.target.value)}
            className={styles.input}
            required
          >
            <option value="">Choose a difficulty level</option>
            <option value="easy">Easy</option>
            <option value="medium">Medium</option>
            <option value="hard">Hard</option>
          </select>
        </div>
      </div>

      <Button label="Create Game" variant="secondary" />
    </form>
  );
};

export default ConfigForm;
