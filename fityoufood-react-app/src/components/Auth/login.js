import React, { useState, useContext } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import './Auth.css';
import { AuthContext } from '../context/AuthContext'; 

const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const { login } = useContext(AuthContext); 

    const loginToTheApp = async () => {
        try {
            const response = await axios.post(
                'http://localhost:8080/AccountControler/login',
                { userName: username, password: password },
                { headers: { 'Content-Type': 'application/json' } }
            );
            console.log(response.data);

            login({ username: response.data.userName, token: response.data.token });
        } catch (error) {
            console.error('Error during login:', error);
        }
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        loginToTheApp();
    };

    return (
        <div>
            <h2>Login Page</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="username">Username</label>
                    <input
                        className="auth-input"
                        type="text"
                        id="username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        placeholder="Enter username"
                    />
                </div>
                <div>
                    <label htmlFor="password">Password</label>
                    <input
                        className="auth-input"
                        type="password"
                        id="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        placeholder="Enter password"
                    />
                </div>
                <div>
                    <button className="auth-button" type="submit">
                        Login
                    </button>
                </div>
                <div>
                    <Link to="/">
                        <button className="auth-button">Back to main page</button>
                    </Link>
                </div>
            </form>
        </div>
    );
};

export default Login;
