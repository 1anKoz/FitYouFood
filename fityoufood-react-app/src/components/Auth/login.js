import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import './Auth.css'

const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();
        alert(`Logged in with username: ${username} and password: ${password}`);
    };

    return (
        <div>
            <h2>Login Page</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="username">Username</label>
                    <input class="auth-input"
                        type="text"
                        id="username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        placeholder="Enter username"
                    />
                </div>
                <div>
                    <label htmlFor="password">Password</label>
                    <input class="auth-input"
                        type="password"
                        id="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        placeholder="Enter password"
                    />
                </div>
                <div>
                    <button class="auth-button" type="submit">Login</button>
                </div>
                <div>
                    <Link to='/'>
                        <button>Back to main page</button>
                    </Link>
                </div>
            </form>
        </div>
    );
};

export default Login;
