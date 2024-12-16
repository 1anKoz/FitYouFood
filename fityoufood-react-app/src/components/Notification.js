import React, { useEffect, useState, useContext } from 'react';
import { Link } from 'react-router-dom';
import './Components.css';
import { AuthContext } from './context/AuthContext';

const Notification = () => {
    const [permission, setPermission] = useState('default');  
    const { user, login, logout } = useContext(AuthContext);

    useEffect(() => {
        if (!("Notification" in window)) {
            alert("This browser does not support desktop notifications.");
            return;
        }

        if (Notification.permission === 'default') {
            Notification.requestPermission()
                .then((permission) => setPermission(permission))
                .catch((error) => console.error("Notification permission error:", error));
        } else {
            setPermission(Notification.permission);  
        }
    }, []);  

    const showNotification = () => {
        if (!("Notification" in window)) {
            alert("This browser does not support desktop notifications.");
            return;
        }

        if (permission === 'granted') {
            try {
                new Notification('New Message!', {
                    body: 'You have to complete your daily workout!',
                    icon: 'favicon.ico', 
                });
            } catch (error) {
                console.error("Error displaying notification:", error);
            }
        } else if (permission === 'denied') {
            alert("You have denied notifications. Please enable them in browser settings.");
        } else {
            alert(`Notification permission not granted. Current permission: ${permission}`);
        }
    };

    return (
        <div>
            <button className="component-button" onClick={showNotification}>
                Show Notification
            </button>
            <div>
                <Link to='/'>
                    <button className="component-button">Back to main page</button>
                </Link>
            </div>
        </div>
    );
};

export default Notification;
