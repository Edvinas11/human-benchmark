import FeaturedGames from '../../components/FeaturedGames/FeaturedGames';
import Hero from '../../components/Hero/Hero';

import styles from './Home.module.css'

const Home = () => {
  return (
    <section className={styles.home}>
      <div className={"wrapper"}>
        <Hero />
        <FeaturedGames />
      </div>
    </section>
  )
}

export default Home