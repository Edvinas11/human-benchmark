import styles from './Navbar.module.css';
import { Link } from 'react-router-dom';

import logo from '../../assets/logo.svg';
import Button from '../Button/Button';

const Navbar = () => {
  return (
    <nav className={styles.navbar}>
      <div className={styles.logo}>
        <img src={logo} alt='logo' className={styles.logoIcon}/>
        <span>Human Benchmark</span>
      </div>

      <ul className={styles.navbarLinks}>
        <li className={styles.navbarItem}>
          <Link to="/" className={styles.navbarLink}>Home</Link>
        </li>
        <li className={styles.navbarItem}>
          <Link to="/" className={styles.navbarLink}>Games</Link>
        </li>
        <li className={styles.navbarItem}>
          <Link to="/" className={styles.navbarLink}>Leaderboards</Link>
        </li>
        <li className={styles.navbarItem}>
          <Link to="/create-game" className={styles.navbarLink}>Create Game</Link>
        </li>

        <Button label={"Play Now"} variant='primary'/>
      </ul>
    </nav>
  )
}

export default Navbar