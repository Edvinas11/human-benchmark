import styles from './SearchBar.module.css'
import Button from '../Button/Button'

import searchImg from '../../assets/search.svg'

const SearchBar = () => {
  return (
    <div className={styles.searchBar}>
        <img src={searchImg} alt='search' className={styles.icon}/>
        <input type="text" placeholder="Search for a game" className={styles.searchInput} />
        <Button label={"Search"} variant='secondary'/>
    </div>
  )
}

export default SearchBar