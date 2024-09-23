import React from 'react'
import { ButtonProps } from '../../types/props'
import styles from './Button.module.css'

const Button: React.FC<ButtonProps> = ({ label, variant, onClick }) => {
  return (
    <button className={`${styles.button} ${variant === "secondary" ? styles.secondary : styles.primary}`} onClick={onClick}>
        {label}
    </button>
  )
}

export default Button