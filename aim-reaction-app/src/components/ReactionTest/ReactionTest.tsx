import React, { useEffect, useState } from 'react'
import Button from '../Button/Button';

const ReactionTest: React.FC<any> = ({ onTestComplete }) => {
    const [waitingForClick, setWaitingForClick] = useState(false);
    const [startTime, setStartTime] = useState<number | null>(null);

    useEffect(() => {
        const randomDelay = Math.random() * 5000 + 2000; // 2-7s delay

        const timeout = setTimeout(() => {
            setWaitingForClick(true);
            setStartTime(Date.now());
        }, randomDelay);

        return () => clearTimeout(timeout);
    }, []);

    const handleClick = () => {
        if (startTime !== null) {
            const reactionTime = Date.now() - startTime;
            onTestComplete(reactionTime);
        }
    }

  return (
    <div>
        {waitingForClick ? (
            <Button label='Click Now!' variant='secondary' onClick={handleClick}/>
        ) : (
            <p>Wait for it...</p>
        )}
    </div>
  )
}

export default ReactionTest