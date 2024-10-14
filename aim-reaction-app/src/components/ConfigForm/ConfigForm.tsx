import React, { useState } from "react";

import styles from "./ConfigForm.module.css";
import Button from "../Button/Button";
import { GameType } from "../GameType/GameType";

const ConfigForm = () => {
  const [name, setName] = useState("");
  const [descr, setDescr] = useState("");
  const [duration, setDuration] = useState("");
  const [targets, setTargets] = useState("");
  const [difficulty, setDifficulty] = useState("");
  const [speed, setSpeed] = useState("");
  const [type, setType] = useState("");



 const gameTypeMap: Record<string, GameType> = {
    MovingTargets: GameType.MovingTargets,
    ReflexTest: GameType.ReflexTest,
    CustomChallenge: GameType.CustomChallenge,
    ReactionTest: GameType.ReactionTimeChallenge
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const gameConfigDto = {
      name: name,
      description: descr,
      difficultyLevel: difficulty,
      targetSpeed: parseInt(speed),
      maxTargets: parseInt(targets),
      gameDuration: parseInt(duration),
      gameType: gameTypeMap[type] || 2,
    };

    const apiUrl = import.meta.env.VITE_API_URL;

    try {
      const response = await fetch(`${apiUrl}/GameConfig/upload`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(gameConfigDto),
      });

      if (response.ok) {
        alert("Game configuration created successfully!");

        setName("");
        setDuration("");
        setTargets("");
        setDifficulty("");
        setSpeed("");
        setType("");
      } else {
        const errorData = await response.json();
        alert("Error creating game configuration: " + errorData.message);
      }
    } catch (error) {
      if (error instanceof Error) {
        alert("An error occurred: " + error.message);
      } else {
        alert("An unexpected error occurred.");
      }
    }
  }

  return (
    <form className={styles.form} onSubmit={handleSubmit}>
      <h2>New Game</h2>
      <p>Please specify required configuration to create a game.</p>

      <div className={styles.formContent}>
        <div className={styles.inputItem}>
          <label htmlFor="name">Game Name</label>
          <input
            type="text"
            id="gameNameName"
            placeholder="Enter a name"
            value={name}
            onChange={(e) => setName(e.target.value)}
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
            <option value="MovingTargets">MovingTargets</option>
            <option value="ReflexTest">ReflexTest</option>
            <option value="CustomChallenge">CustomChallenge</option>
            <option value="ReactionTest">ReactionTest</option>
          </select>
        </div>

        <div className={styles.inputItem}>
          <label htmlFor="descr">Description</label>
          <textarea
            id="descr"
            placeholder="Enter a description"
            value={descr}
            onChange={(e) => setDescr(e.target.value)}
            className={styles.input}
            required
          />
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
