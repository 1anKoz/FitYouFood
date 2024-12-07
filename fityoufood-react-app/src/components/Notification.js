import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

const Notification = () => {
    const [permission, setPermission] = useState(Notification.permission);

    useEffect(() => {
        if (Notification.requestPermission) {
            Notification.requestPermission().then((permission) => {
                setPermission(permission);
            });
        }
    }, []);

    const showNotification = () => {
        if (permission === 'granted') {
            new Notification('New Message!', { body: 'You have a new notification.' });
        } else {
            alert('Notification permission not granted.');
        }
    };

    return (
        <div>
            <button onClick={showNotification}>Show Notification</button>
            <div>
                <Link to='/'>
                    <button>Back to main page</button>
                </Link>
            </div>
        </div>
    );
};

export default Notification;
