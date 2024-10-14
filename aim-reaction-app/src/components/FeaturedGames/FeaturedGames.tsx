import React, { useEffect, useState } from "react";

import styles from "./FeaturedGames.module.css";
import GameCard from "../GameCard/GameCard";

import { Game } from "../../types/props";
import Button from "../Button/Button";
import { useNavigate } from "react-router-dom";

const FeaturedGames = () => {
  const [games, setGames] = useState<Game[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  const navigate = useNavigate();

  const apiUrl = import.meta.env.VITE_API_URL;

  useEffect(() => {
    const fetchGames = async () => {
      try {
        setLoading(true);
        setError("");
        const response = await fetch(`${apiUrl}/Game`);

        if (!response.ok) {
          throw new Error(`Network response was not ok`);
        }

        const data = await response.json();
        console.log(data);
        setGames(data);
      } catch (error) {
        setError("Failed to load games.");
      } finally {
        setLoading(false);
      }
    };

    fetchGames();
  }, []);

  return (
    <section className={styles.games}>
      <div className={styles.available}>
        <h2>Featured Games</h2>

        {/* Conditionally render based on the state */}
        {loading && <p>Loading games...</p>}
        {error && <p>{error}</p>}
        {!loading && !error && games.length === 0 && <p>No games for now :(</p>}
        {!loading && !error && games.length > 0 && (
          <div className={styles.gamesGrid}>
            {games.map((game) => (
              <GameCard key={game.gameId} game={game} />
            ))}
          </div>
        )}
      </div>

      <div className="reflex">
        <h2>Play Reflex Test</h2>
        <Button label="Play Now" variant="primary" onClick={() => { navigate('/reflex-test') }}/>
      </div>
    </section>
  );
};

export default FeaturedGames;
