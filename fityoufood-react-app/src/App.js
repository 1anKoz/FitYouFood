import React, { useState, useEffect, useContext } from 'react';
import './App.css';
import logo from './logo.png';
import { BrowserRouter as Router, Routes, Route, Link, useLocation } from 'react-router-dom';

import Content from './components/Content';
import Login from './components/Auth/login';
import Register from './components/Auth/register';
import Notification from './components/Notification';
import { AuthContext } from './components/context/AuthContext';
import UserConfig from './components/UserConfig';

function App() {
  const [loading, setLoading] = useState(true);
  const { user, login, logout } = useContext(AuthContext); // Access user, login, and logout from context

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
                <Link to="/"><button className="menu-buttons">Main page</button></Link>
                {user ? (
                  <>
                    <Link to="/content"><button className="menu-buttons">Content</button></Link>
                    <Link to="/settings/user"><button className="menu-buttons">{user.username}</button></Link>
                    <button className="menu-buttons" onClick={logout}>Logout</button>
                  </>
                ) : (
                  <>
                    <Link to="/login"><button className="menu-buttons">Login</button></Link>
                    <Link to="/register"><button className="menu-buttons">Register</button></Link>
                  </>
                )}
                <Link to="/notifications"><button className="menu-buttons">Notifications</button></Link>
              </nav>
            </div>

            <div className="App-header">
              <div className="App-main-content">
                <WelcomeText />
                <Routes>
                  <Route path="/content" element={<Content />} />
                  <Route path="/login" element={<Login />} />
                  <Route path="/register" element={<Register />} />
                  <Route path="/notifications" element={<Notification />} />
                  <Route path="/settings/user" element={<UserConfig />} />
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
          <h1>Welcome</h1>
          {user && <h2>{user.username}</h2>}
          <h1>to the FitYouFood!</h1>
          <p>The best training and diet plan app on the planet.</p>
        </div>
      );
    }

    return null;
  }
}

export default App;
