import React, { useState } from 'react';
import { Link, useNavigate  } from 'react-router-dom';
import './Auth.css'
import axios from 'axios';

const Register = () => {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const navigate = useNavigate(); 

    const registerInTheApp = async () => {
        try {
            const response = await axios.post('http://localhost:8080/AccountControler/register',
                 {"userName": username, 
                    "email": email , 
                    "password": password
                }, {headers: {'Content-Type': 'application/json'}})

            console.log(response.data)

            navigate('/');
            
        } catch (error) {
            console.error("Error during registering phase:", error)
        }
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        if (password !== confirmPassword) {
            alert('Passwords do not match!');
            return;
        }

        alert(`Registered with username: ${username}, email: ${email}`);
        registerInTheApp();
    };

    return (
        <div>
            <h2>Register Page</h2>
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
                    <label htmlFor="email">Email</label>
                    <input class="auth-input"
                        type="email"
                        id="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        placeholder="Enter email"
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
                    <label htmlFor="confirmPassword">Confirm Password</label>
                    <input class="auth-input"
                        type="password"
                        id="confirmPassword"
                        value={confirmPassword}
                        onChange={(e) => setConfirmPassword(e.target.value)}
                        placeholder="Confirm your password"
                    />
                </div>
                <div>
                    <button class="auth-button" type="submit">Register</button>
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

export default Register;
