import styles from './LeaderboardTable.module.css'

const LeaderbordTable = () => {
    return (
        <section>
            <table className={styles.LeaderboardTable}>
                <tr>
                    <th>Rank</th>
                    <th>User</th>
                    <th>Game</th>
                    <th>Game difficulty</th>
                    <th>Score</th>
                    <th>Date</th>
                </tr>
                <tr>
                    <td>1</td>
                    <td>Test</td>
                    <td>Moving targets</td>
                    <td>Hard</td>
                    <td>100</td>
                    <td>2024/09/27</td>                </tr>
            </table>
        </section>
    )
}

export default LeaderbordTable