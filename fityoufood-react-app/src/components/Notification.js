import React, { useEffect, useState, useContext } from 'react';
import { Link } from 'react-router-dom';
import './Components.css';
import { AuthContext } from './context/AuthContext';

const Notification = () => {
    const { user, login, logout } = useContext(AuthContext);

    const showWorkoutNotification = () => {
        if (!("Notification" in window)) {
            alert("This browser does not support desktop notifications.");
            return;
        }

        try {
            new Notification('Time to Workout!', {
                body: 'Don\'t forget to complete your daily workout!',
                icon: '/favicon.ico',
                tag: 'workout-reminder',
                data: {
                    url: '/workout', // Custom data to handle click actions if needed
                }
            });
        } catch (error) {
            console.error("Error displaying workout notification:", error);
            alert("Notification failed. Please check your browser's notification settings.");
        }
    };

    const showTrainingStatusNotification = () => {
        if (!("Notification" in window)) {
            alert("This browser does not support desktop notifications.");
            return;
        }

        try {
            new Notification('Training Status Update!', {
                body: 'You have a new training update. Check your training schedule!',
                icon: '/favicon.ico',
                tag: 'training-status',
                data: {
                    url: '/training', // Link to the training page
                }
            });
        } catch (error) {
            console.error("Error displaying training status notification:", error);
            alert("Notification failed. Please check your browser's notification settings.");
        }
    };

    const showUserAccountNotification = () => {
        if (!("Notification" in window)) {
            alert("This browser does not support desktop notifications.");
            return;
        }

        try {
            new Notification('Account Update!', {
                body: 'Your account has been updated successfully.',
                icon: '/favicon.ico',
                tag: 'account-update',
                data: {
                    url: '/user-config', // Custom data for the user config page
                }
            });
        } catch (error) {
            console.error("Error displaying account notification:", error);
            alert("Notification failed. Please check your browser's notification settings.");
        }
    };

    const showGeneralReminderNotification = () => {
        if (!("Notification" in window)) {
            alert("This browser does not support desktop notifications.");
            return;
        }

        try {
            new Notification('Reminder!', {
                body: 'Don\'t forget to log your meals today!',
                icon: '/favicon.ico',
                tag: 'general-reminder',
                data: {
                    url: '/log-meal', // Directs user to meal logging page
                }
            });
        } catch (error) {
            console.error("Error displaying general reminder notification:", error);
            alert("Notification failed. Please check your browser's notification settings.");
        }
    };

    return (
        <div>
            <button className="component-button" onClick={showWorkoutNotification}>
                Show Workout Notification
            </button>
            <button className="component-button" onClick={showTrainingStatusNotification}>
                Show Training Status Notification
            </button>
            <button className="component-button" onClick={showUserAccountNotification}>
                Show Account Update Notification
            </button>
            <button className="component-button" onClick={showGeneralReminderNotification}>
                Show General Reminder Notification
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