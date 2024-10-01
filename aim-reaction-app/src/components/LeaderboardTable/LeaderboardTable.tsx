import React, { useEffect, useState } from "react";
import styles from './LeaderboardTable.module.css'
import { Score } from '../../types/props'

const LeaderboardTable = () => {
    const [scores, setScores] = useState<Score[]>([])
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        const fetchScores = async () => {
            try{
                setLoading(true);
                setError("");

                const response = await fetch("https"); // api call to get all user scores

                if (!response.ok) {
                    throw new Error(`Network response was not ok`);
                }
                const data = await response.json();
                setScores(data);
            }catch(error){
                setError("Failed to load the scores");
            }finally{
                setLoading(false);
            }
        };
        fetchScores();
    }, [])

    if (loading) {
        return <div>Loading scores...</div>
    }
    if (error) {
        return <div>{error}</div>
    }

    return (
        <section>
            <table className={styles.LeaderboardTable}>
                <thead>
                    <tr>
                        <th>Rank</th>
                        <th>User</th>
                        <th>Game</th>
                        <th>Game difficulty</th>
                        <th>Score</th>
                        <th>Date</th>
                    </tr>
                </thead>
                <tbody>
                    {scores.map((score, index) => (
                        <tr key={score.UserId}> {/* Assuming Score has a unique id */}
                            <td>{index + 1}</td>
                            <td>{score.UserName}</td>
                            <td>{score.game}</td>
                            <td>{score.difficulty}</td>
                            <td>{score.Score}</td>
                            <td>{new Date(score.date).toLocaleDateString()}</td>
                        </tr>
                </tbody>
            </table>
        </section>
    );
}

export default LeaderboardTable  