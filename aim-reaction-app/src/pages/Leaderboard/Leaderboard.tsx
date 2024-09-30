import styles from './Leaderboard.module.css'
import LeaderboardText from '../../components/LeaderboardText/LeaderboardText';
import LeaderbordTable from '../../components/LeaderboardTable/LeaderboardTable';

const Leaderboard = () => {
    return (
      <section>
         <div className ='wrapper'>
                <LeaderboardText />
                <LeaderbordTable />
         </div>
      </section>  
    )
}

export default Leaderboard