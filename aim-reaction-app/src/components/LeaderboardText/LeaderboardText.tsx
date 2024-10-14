import styles from './LeaderboardText.module.css'

const LeaderboardText = () => {
    return (
        <section>
            <div className={styles.LeaderboardText}>
            <h1>Leaderboard</h1>
            <p>Top users</p>
          </div>
        </section>
    )
}

export default LeaderboardText