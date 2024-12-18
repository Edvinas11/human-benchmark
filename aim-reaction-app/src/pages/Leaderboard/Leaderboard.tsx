﻿import styles from './Leaderboard.module.css'
import LeaderboardText from '../../components/LeaderboardText/LeaderboardText';
import LeaderboardTable from '../../components/LeaderboardTable/LeaderboardTable';


const Leaderboard = () => {
    return (
      <section>
            <div className={styles.leaderboard}>
                <LeaderboardText />
                <LeaderboardTable />
         </div>
      </section>  
    )
}

export default Leaderboard