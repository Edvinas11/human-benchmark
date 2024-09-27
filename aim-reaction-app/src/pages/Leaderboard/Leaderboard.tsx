import styles from './Leaderboard.module.css'
import LeaderboardText from '../../components/LeaderboardText/LeaderboardText';

const Leaderboard = () => {
    return (
        <section>
            <div className='wrapper'>
                <LeaderboardText />
            </div>
    </section>  
    )
}

export default Leaderboard