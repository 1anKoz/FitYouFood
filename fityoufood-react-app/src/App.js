import React, { useState, useEffect } from 'react';
import './App.css';
import logo from './logo.svg';  // If you want to keep the logo

function App() {
  const [loading, setLoading] = useState(true);

  // Simulate loading screen for 3 seconds
  useEffect(() => {
    setTimeout(() => setLoading(false), 3000);
  }, []);

  return (
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
          <h1>App is working perfectly!</h1>
          <p>Welcome to FitYouFood</p>
          <button onClick={() => alert('Hello my dear user!')}>Click me</button>
        </div>
      )}
    </div>
  );
}

export default App;
