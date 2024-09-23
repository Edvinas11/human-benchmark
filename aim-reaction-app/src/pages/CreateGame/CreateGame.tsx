import React from 'react'

import styles from './CreateGame.module.css'
import ConfigForm from '../../components/ConfigForm/ConfigForm'

const CreateGame = () => {
  return (
    <section className={styles.config}>
        <div className='wrapper'>
            <ConfigForm />
        </div>
    </section>
  )
}

export default CreateGame