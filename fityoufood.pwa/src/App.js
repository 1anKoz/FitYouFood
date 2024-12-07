import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import SplashScreen from './components/SplashScreen';
import Login from './components/Auth/Login';
import Register from './components/Auth/Register';
import Content from './components/Content';
import Notification from './components/Notification';
import { AuthProvider } from './context/AuthContext';

function App() {
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        setTimeout(() => setLoading(false), 3000); // Splash screen przez 3 sekundy
    }, []);

    return (
        <AuthProvider>
            <Router>
                {loading ? (
                    <SplashScreen />
                ) : (
                    <Routes>
                        <Route path="/" element={<Content />} />
                        <Route path="/login" element={<Login />} />
                        <Route path="/register" element={<Register />} />
                        <Route path="/notifications" element={<Notification />} />
                    </Routes>
                )}
            </Router>
        </AuthProvider>
    );
}

export default App;
