import styles from './Hero.module.css'

const Hero = () => {
  return (
    <section className={styles.hero}>
      <div className={styles.overlay}></div>
      <div className={styles.content}>
        <h1>Train your aim</h1>
        <p>Improve your accuracy and reaction time with aim training games</p>

      </div>
    </section>
  )
}

export default Hero