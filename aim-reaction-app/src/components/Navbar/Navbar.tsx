import { useState } from 'react';
import styles from './Navbar.module.css';
import { Link } from 'react-router-dom';

import logo from '../../assets/logo.svg';
import burgerIcon from '../../assets/burger-icon.svg';
import Button from '../Button/Button';

const Navbar = () => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  const toggleMenu = () => {
    setIsMenuOpen(!isMenuOpen);
  };

  return (
    <nav className={styles.navbar}>
      <div className={styles.logo}>
        <img src={logo} alt='logo' className={styles.logoIcon} />
        <span>Human Benchmark</span>
      </div>

      {/* Burger Icon for Mobile */}
      <img
        src={burgerIcon}
        alt="Menu"
        className={styles.burgerIcon}
        onClick={toggleMenu}
      />

      {/* Navigation Links for Desktop */}
      <ul className={styles.navbarLinks}>
        <li className={styles.navbarItem}>
          <Link to="/" className={styles.navbarLink}>Home</Link>
        </li>
        <li className={styles.navbarItem}>
          <Link to="/games" className={styles.navbarLink}>Games</Link>
        </li>
        <li className={styles.navbarItem}>
          <Link to="/leaderboards" className={styles.navbarLink}>Leaderboards</Link>
        </li>
        <li className={styles.navbarItem}>
          <Link to="/create-game" className={styles.navbarLink}>Create Game</Link>
        </li>

        <Button label={"Play Now"} variant='primary' />
      </ul>

      {/* Mobile Navigation Box */}
      {isMenuOpen && (
        <div className={styles.mobileMenu}>
          <ul className={styles.mobileLinks}>
            <li>
              <Link to="/" onClick={toggleMenu}>Home</Link>
            </li>
            <li>
              <Link to="/games" onClick={toggleMenu}>Games</Link>
            </li>
            <li>
              <Link to="/leaderboards" onClick={toggleMenu}>Leaderboards</Link>
            </li>
            <li>
              <Link to="/create-game" onClick={toggleMenu}>Create Game</Link>
            </li>
          </ul>
        </div>
      )}
    </nav>
  );
};

export default Navbar;
