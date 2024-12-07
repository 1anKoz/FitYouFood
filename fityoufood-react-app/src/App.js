import React, { useState, useEffect } from 'react';
import './App.css';
import logo from './logo.png';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';

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
        {/* Display loading screen for 3 seconds */}
        {loading ? (
          <div className="App-header">
            <img src={logo} className="App-logo" alt="logo" />
            <p>Loading...</p>
          </div>
        ) : (
          <div className="App-header">
            {/* Main content after loading */}
            <img src={logo} className="App-logo" alt="logo" />

            {/* Navigation Button (using React Router Link for navigation) */}
            <nav>
              <Link to="/"><button class="menu-buttons">Main page</button></Link> 
              <Link to="/content"><button class="menu-buttons">Content</button></Link> 
              <Link to="/login"><button class="menu-buttons">Login</button></Link> 
              <Link to="/register"><button class="menu-buttons">Register</button></Link> 
              <Link to="/notifications"><button class="menu-buttons">Notifications</button></Link>
            </nav>

            {/* Routes setup */}
            <Routes>
              <Route path="/content" element={<Content />} />
              <Route path="/login" element={<Login />} />
              <Route path="/register" element={<Register />} />
              <Route path="/notifications" element={<Notification />} />
            </Routes>
          </div>
        )}
      </div>
    </Router>
  );
}

export default App;
