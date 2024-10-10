import { useEffect, useState } from "react";
import styles from './LeaderboardTable.module.css'
import { Score } from '../../types/props'

const LeaderboardTable = () => {
    const [scores, setScores] = useState<Score[]>([])
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    const apiUrl = import.meta.env.VITE_API_URL;

    useEffect(() => {
        const fetchScores = async () => {
            try {
                setLoading(true);
                setError("");

                const response = await fetch(`${apiUrl}/Leaderboard/top-scores/100`); // api call to get top 100 user scores

                console.log("Response: ", response);

                if (!response.ok) {
                    throw new Error(`Network response was not ok`);
                }
                const data = await response.json();
                console.log("Fetched data:", data);
                setScores(data);
            } catch (error) {
                console.error("Error fetching scores:", error);
                setError("Failed to load the scores");
            } finally {
                setLoading(false);
            }
        };
        fetchScores();
    }, [])

    return (
        <div>
            {loading && <p>Loading scores...</p>}
            {error && <p>{error}</p>}
            {!loading && !error && scores.length === 0 && <p>No scores are available</p>}
            {!loading && !error && scores.length > 0 && (
                <section>
                    <div className={styles.LeaderboardTableContainer}>
                        <table className={styles.LeaderboardTable}>
                            <thead>
                                <tr>
                                    <th>Rank</th>
                                    <th>User</th>
                                    <th>Game</th>
                                    <th>Score</th>
                                    <th>Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                {scores.map((score, index) => (
                                    <tr key={score.userId}> {/* Assuming Score has a unique id */}
                                        <td>{index + 1}</td>
                                        <td>{score.userName}</td>
                                        <td>{score.gameType}</td>
                                        <td>{score.score}</td>
                                        <td>{new Date(score.dateAchieved).toLocaleDateString("en-CA")}</td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                </section>
            )}
        </div>
    );
};

export default LeaderboardTable  