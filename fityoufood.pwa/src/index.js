// src/index.js
import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';

// Service Worker Registration
if ('serviceWorker' in navigator) {
    window.addEventListener('load', () => {
        navigator.serviceWorker
            .register('/service-worker.js')
            .then((registration) => {
                console.log('Service Worker registered: ', registration);
            })
            .catch((registrationError) => {
                console.log('Service Worker registration failed: ', registrationError);
            });
    });
}

ReactDOM.render(<App />, document.getElementById('root'));
