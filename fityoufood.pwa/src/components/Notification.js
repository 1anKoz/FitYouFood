import React, { useEffect } from 'react';

const Notification = () => {
    useEffect(() => {
        Notification.requestPermission();
    }, []);

    const showNotification = () => {
        new Notification('Nowa wiadomość!', { body: 'Otrzymałeś nową wiadomość.' });
    };

    return <button onClick={showNotification}>Wyślij Powiadomienie</button>;
};

export default Notification;
