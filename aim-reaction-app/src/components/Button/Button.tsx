import React from 'react';
import { ButtonProps } from '../../types/props';
import styles from './Button.module.css';

const Button: React.FC<ButtonProps> = ({ label, variant = "primary", onClick }) => {
    const getVariantClass = (variant: string) => {
        switch (variant) {
            case "primary":
                return styles.primary;
            case "secondary":
                return styles.secondary;
            case "third":
                return styles.third;
            case "fourth":
                return styles.fourth;
            default:
                return styles.primary; 
        }
    };

    return (
        <button className={`${styles.button} ${getVariantClass(variant)}`} onClick={onClick}>
            {label}
        </button>
    );
}

export default Button;
