import React, { useEffect } from 'react';

const Notification = () => {
    useEffect(() => {
        Notification.requestPermission();
    }, []);

    const showNotification = () => {
        new Notification('New Message!', { body: 'You have a new notification.' });
    };

    return <button onClick={showNotification}>Show Notification</button>;
};

export default Notification;
