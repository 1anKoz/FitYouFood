import React, { useState, useEffect } from 'react';
import './App.css';
import logo from './logo.png';
import { BrowserRouter as Router, Routes, Route, Link, useLocation } from 'react-router-dom';

import Content from './components/Content';
import Login from './components/Auth/Login';
import Register from './components/Auth/Register';
import Notification from './components/Notification';

function App() {
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    setTimeout(() => setLoading(false), 3000);
  }, []);

  return (
    <Router>
      <div className="App">
        {loading ? (
          <div className="App-header">
            <img src={logo} className="App-logo" alt="logo" />
            <p>Please wait while the app is loading...</p>
          </div>
        ) : (
          <div>
            <div className="App-nav-bar">
                <nav>
                  <Link to="/"><button class="menu-buttons">Main page</button></Link> 
                  <Link to="/content"><button class="menu-buttons">Content</button></Link> 
                  <Link to="/login"><button class="menu-buttons">Login</button></Link> 
                  <Link to="/register"><button class="menu-buttons">Register</button></Link> 
                  <Link to="/notifications"><button class="menu-buttons">Notifications</button></Link>
                </nav>
                
            </div>
            <div className="App-header">
                <div className="App-main-content">
                    <WelcomeText/>
                    <Routes>
                      <Route path="/content" element={<Content />} />
                      <Route path="/login" element={<Login />} />
                      <Route path="/register" element={<Register />} />
                      <Route path="/notifications" element={<Notification />} />
                  </Routes>
                </div>
            </div>
          </div>
        )}
      </div>
    </Router>
  );
  function WelcomeText() {
    const location = useLocation(); 
  
    if (location.pathname === "/") {
      return (
        <div>
          <img src={logo} className="App-logo" alt="logo" />
          <h1>Welcome to the FitYouFood!</h1>
          <p>The best training and diet plan app on the planet.</p>
        </div>
      );
    }
  
    return null; 
  }
}

export default App;
