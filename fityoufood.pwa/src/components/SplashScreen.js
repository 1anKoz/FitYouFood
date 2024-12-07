import React from 'react';
import logo from '../assets/logo.png';

const SplashScreen = () => (
    <div style={{ textAlign: 'center', padding: '50px' }}>
        <img src={logo} alt="Logo" style={{ width: '150px', height: '150px' }} />
        <h1>Loading...</h1>
    </div>
);

export default SplashScreen;
