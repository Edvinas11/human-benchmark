import { useState } from "react";
import styles from "./LeaderboardTable.module.css";
import { Score } from "../../types/props";
import Button from "../Button/Button";
import { GameType } from "../GameType/GameType";

const LeaderboardTable = () => {
    const [scores, setScores] = useState<Score[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
    const [gameType, setGameType] = useState<GameType | null>(null); // Start with no gameType selected

    const apiUrl = import.meta.env.VITE_API_URL;
    const topCount = 100;

    // Fetch leaderboard scores based on gameType
    const fetchScores = async (selectedGameType: GameType) => {
        try {
            setLoading(true);
            setError("");
            setScores([]);

            const response = await fetch(
                `${apiUrl}/Leaderboard/top-scores/${topCount}?gameType=${selectedGameType}`
            );

            if (!response.ok) {
                throw new Error("Network response was not ok");
            }

            const data = await response.json();
            setScores(data);
        } catch (error) {
            console.error("Error fetching scores:", error);
            setError("Failed to load the scores. Please try again.");
        } finally {
            setLoading(false);
        }
    };

    const handleGameTypeChange = (selectedGameType: GameType) => {
        if (selectedGameType !== gameType) {
            setGameType(selectedGameType);
            fetchScores(selectedGameType); // Fetch scores only after selecting a game type
        }
    };

    const sortedScores = scores.sort((a, b) => {
        if (gameType === GameType.ReactionTimeChallenge) {
            return a.score - b.score;
        } else {
            
            return b.score - a.score; 
        }
    });

    return (
        <div>
            <div className={styles.LeaderboardWrapper}>
                <div className={styles.buttonContainer}>
                    <Button
                        label="Reflex Test"
                        variant={gameType === GameType.ReflexTest ? "third" : "primary"}
                        onClick={() => handleGameTypeChange(GameType.ReflexTest)}
                    />
                    <Button
                        label="Reaction Time"
                        variant={
                            gameType === GameType.ReactionTimeChallenge ? "third" : "primary"
                        }
                        onClick={() => handleGameTypeChange(GameType.ReactionTimeChallenge)}
                    />
                </div>

                {!gameType && <p>Please select a game type to view the leaderboard.</p>}

                {loading && <p>Loading scores...</p>}
                {error && <p>{error}</p>}
                {!loading && !error && scores.length === 0 && gameType && (
                    <p>No scores are available for the selected game type.</p>
                )}
                {!loading && !error && scores.length > 0 && (
                    <div className={styles.LeaderboardTableContainer}>
                        <table className={styles.LeaderboardTable}>
                            <thead>
                                <tr>
                                    <th>Rank</th>
                                    <th>User</th>
                                    <th>Score</th>
                                    <th>Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                {sortedScores.map((score, index) => (
                                    <tr key={`${score.userId}-${score.dateAchieved}`}>
                                        <td>{index + 1}</td>
                                        <td>{score.userName}</td>
                                        <td>{score.score}</td>
                                        <td>
                                            {new Date(score.dateAchieved).toLocaleDateString(
                                                "en-CA"
                                            )}
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                )}
            </div>
        </div>
    );
};

export default LeaderboardTable;
