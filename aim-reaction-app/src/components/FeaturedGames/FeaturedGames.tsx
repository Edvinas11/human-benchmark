import { useEffect, useState } from "react";

import styles from "./FeaturedGames.module.css";
import GameCard from "../GameCard/GameCard";

import { Game } from "../../types/props";

const FeaturedGames = () => {
  const [games, setGames] = useState<Game[]>([]);
  const [activeUserCount, setActiveUserCount] = useState<number | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  const apiUrl = import.meta.env.VITE_API_URL;

  useEffect(() => {
    const fetchGames = async () => {
      try {
        setLoading(true);
        setError("");
        const response = await fetch(`${apiUrl}/GenericGame/games`);

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

  useEffect(() => {
    const fetchActiveUserCount = async () => {
      try {
        const response = await fetch(`${apiUrl}/GenericGame/active`, {
          method: "GET",
      });

        if (!response.ok) {
          throw new Error("Failed to fetch active user count.");
        }

        const data = await response.json();
        setActiveUserCount(data.activeSessions);
      } catch (error) {
        console.error("Error fetching active user count:", error);
      }
    };

    fetchActiveUserCount();
  }, []);

  return (
    <section className={styles.games}>
      <div className={styles.available}>
      <h2 className={styles.featuredTitle}>Featured Games</h2>
        {activeUserCount !== null && (
          <span className={styles.activeUsers}>Active users: {activeUserCount}</span>
        )}

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
    </section>
  );
};



export default FeaturedGames;
