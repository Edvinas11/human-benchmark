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

                const response = await fetch("https://localhost:7028/api/Leaderboard/all-users"); // api call to get all user scores

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
                    <table className={styles.LeaderboardTable}>
                        <thead>
                            <tr>
                                <th>Rank</th>
                                <th>User</th>
                                <th>Email</th>
                                <th>Score</th>
                            </tr>
                        </thead>
                        <tbody>
                            {scores.map((score, index) => (
                                <tr key={score.userId}> {/* Assuming Score has a unique id */}
                                    <td>{index + 1}</td>
                                    <td>{score.userName}</td>
                                    <td>{score.userEmail}</td>
                                    <td>{score.score}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </section>
            )}
        </div>
    );
};

export default LeaderboardTable  