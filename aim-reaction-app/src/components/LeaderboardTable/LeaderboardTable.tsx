import styles from './LeaderboardTable.module.css'

const LeaderboardTable = () => {
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
                    <tr>
                        <td>1</td>
                        <td>Test</td>
                        <td>Moving targets</td>
                        <td>Hard</td>
                        <td>100</td>
                        <td>2024/09/27</td>
                    </tr>
                </tbody>
            </table>
        </section>
    );
}

export default LeaderboardTable  