import React, { useEffect, useState, useContext } from 'react';
import { Link } from 'react-router-dom';
import './Components.css';
import { AuthContext } from './context/AuthContext';

const Notification = () => {
    const [permission, setPermission] = useState('default');  // Default notification permission state  
    const { user, login, logout } = useContext(AuthContext);

    // Set the theme-related notification title and body
    const getWorkoutNotification = () => {
        return {
            title: "Time to Workout!",
            body: "Don't forget to complete your daily workout and stay healthy!",
            icon: '/favicon.ico',
        };
    };

    useEffect(() => {
        // Check if Notification API is supported by the browser
        if (!("Notification" in window)) {
            alert("This browser does not support desktop notifications.");
            return;
        }
    
        // Check if Notification.requestPermission is available
        if (typeof Notification.requestPermission !== 'function') {
            console.error("Notification.requestPermission is not a function. This might indicate an issue with the environment.");
            return;
        }
    
        // Get current permission state and request permission if necessary
        const currentPermission = Notification.permission || 'default';
    
        if (currentPermission === 'default') {
            Notification.requestPermission()
                .then((permission) => {
                    setPermission(permission);  // Update state based on user response
                })
                .catch((error) => {
                    console.error("Notification permission error:", error);
                    setPermission('denied');  // Fallback for errors
                });
        } else {
            setPermission(currentPermission);  // If already granted or denied, update state
        }
    }, []);  // Only runs once when the component mounts

    // Function to show a notification when permission is granted
    const showNotification = () => {
        if (!("Notification" in window)) {
            alert("This browser does not support desktop notifications.");
            return;
        }

        if (permission === 'granted') {
            try {
                const { title, body, icon } = getWorkoutNotification();  // Get workout-related notification data
                new Notification(title, {
                    body,
                    icon,
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
                Show Workout Notification
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
