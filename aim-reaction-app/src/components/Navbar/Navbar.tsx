import { useState, useEffect, useRef } from 'react';
import styles from './Navbar.module.css';
import { Link, useNavigate } from 'react-router-dom';

import logo from '../../assets/logo.svg';
import burgerIcon from '../../assets/burger-icon.svg';
import Button from '../Button/Button';
import { useAuth } from '../../contexts/AuthContext';

const Navbar = () => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const { logout } = useAuth();
  const navigate = useNavigate();
  
  const mobileMenuRef = useRef<HTMLDivElement | null>(null); // Reference to the mobile menu
  const burgerIconRef = useRef<HTMLImageElement | null>(null); // Reference to the burger icon

  const toggleMenu = () => {
      setIsMenuOpen(!isMenuOpen);

  };

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  // Close the menu if clicking outside
  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (mobileMenuRef.current && !mobileMenuRef.current.contains(event.target as Node) &&
          burgerIconRef.current && !burgerIconRef.current.contains(event.target as Node)) {
        setIsMenuOpen(false); // Close the menu when clicked outside
      }
    };

    // Attach the event listener for clicking outside
    document.addEventListener('mousedown', handleClickOutside);

    // Cleanup the event listener when the component unmounts
    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, [mobileMenuRef]);

  useEffect(() => {
    const handleResize = () => {
      if (window.innerWidth > 1000) {
        setIsMenuOpen(false); 
      }
    };

    window.addEventListener('resize', handleResize);
    handleResize();
    return () => {
      window.removeEventListener('resize', handleResize);
    };
  }, []);


  return (
    <nav className={styles.navbar}>
      <Link to={"/"} className={styles.logo}>
        <img src={logo} alt='logo' className={styles.logoIcon} />
        <span>Human Benchmark</span>
      </Link>

      {/* Burger Icon for Mobile */}
      <img
        src={burgerIcon}
        alt="Menu"
        className={styles.burgerIcon}
        onClick={toggleMenu}
        ref={burgerIconRef} 
      />

      {/* Navigation Links for Desktop */}
      <ul className={styles.navbarLinks}>
        <li className={styles.navbarItem}>
          <Link to="/games" className={styles.navbarLink}>Games</Link>
        </li>
        <li className={styles.navbarItem}>
          <Link to="/leaderboards" className={styles.navbarLink}>Leaderboards</Link>
        </li>
        <li className={styles.navbarItem}>
          <Link to="/create-game" className={styles.navbarLink}>Create Game</Link>
        </li>
    
        <Button label={"Logout"} variant='primary' onClick={handleLogout} />
      </ul>

      {/* Mobile Navigation Box */}
      {isMenuOpen && (
        <div ref={mobileMenuRef} className={styles.mobileMenu}>
          <ul className={styles.mobileLinks}>
            <li>
              <Link to="/games" onClick={toggleMenu}>Games</Link>
            </li>
            <li>
              <Link to="/leaderboards" onClick={toggleMenu}>Leaderboards</Link>
            </li>
            <li>
              <Link to="/create-game" onClick={toggleMenu}>Create Game</Link>
            </li>   

            <Button label={"Logout"} variant='primary' onClick={handleLogout}/>         
          </ul>
        </div>
      )}
    </nav>
  );
};

export default Navbar;
