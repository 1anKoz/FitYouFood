import React, { useEffect, useState, useContext } from 'react';
import { Link } from 'react-router-dom';
import './Components.css';
import { AuthContext } from './context/AuthContext';

const Notification = () => {
    const [permission, setPermission] = useState(Notification.permission);
    const { user, login, logout } = useContext(AuthContext);

    useEffect(() => {
        if (Notification.requestPermission) {
            Notification.requestPermission().then((permission) => {
                setPermission(permission);
            });
        }
    }, []);

    const showNotification = () => {
        if (permission === 'granted') {
            new Notification('New Message!', { body: 'You have to complete your daily workout!' });
        } else {
            alert('Notification permission not granted.');
        }
    };

    return (
        <div>
            <button className="component-button" onClick={showNotification}>Show Notification</button>
            <div>
                <Link to='/'>
                    <button className="component-button">Back to main page</button>
                </Link>
            </div>
        </div>
    );
};

export default Notification;
