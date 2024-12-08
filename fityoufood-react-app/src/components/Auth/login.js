import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import './Auth.css'

const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const loginToTheApp = async () => {
        try {
            const response = await axios.post('http://localhost:8080/AccountControler/login', {"userName": username, "password": password}, {headers: {'Content-Type': 'application/json'}})
            console.log(response.data)
        } catch (error) {
            console.error("Error during logging in phase:", error)
        }
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        alert(`Logged in with username: ${username}`);
        loginToTheApp();
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
                        <button class="auth-button">Back to main page</button>
                    </Link>
                </div>
            </form>
        </div>
    );
};

export default Login;
