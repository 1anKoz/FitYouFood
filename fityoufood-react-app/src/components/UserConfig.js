import React, { useState, useEffect, useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import './Components.css';
import { AuthContext } from './context/AuthContext';

const CACHE_NAME = 'fityoufood-user-cache-v2';

const UserConfig = () => {
    const [loadingUserData, setLoadingUserData] = useState(true);
    const [userData, setUserData] = useState(null);
    const { user, logout } = useContext(AuthContext);
    const navigate = useNavigate(); 

    useEffect(() => {
        const fetchData = async () => {
            await fetchUserData();
        };

        fetchData();
    }, []); 

    const fetchUserData = async () => {
        if (!user || !user.token) {
            console.error('User is not logged in or token is missing.');
            setLoadingUserData(false);
            return;
        }

        try {
            // First, try to fetch from the network (API)
            const response = await axios.get('http://localhost:8080/AccountControler', {
                headers: {
                    'Authorization': `Bearer ${user.token}`,
                    'Content-Type': 'application/json',
                },
            });

            console.log(response.data);
            setUserData(response.data);

            // Cache the response for future use
            const cache = await caches.open(CACHE_NAME);
            cache.put('/AccountControler', new Response(JSON.stringify(response.data)));
        } catch (error) {
            console.error('Error while fetching user data:', error);

            // If the network request fails, check the cache for data
            if ('caches' in window) {
                const cachedResponse = await caches.match('/AccountControler');
                if (cachedResponse) {
                    const cachedData = await cachedResponse.json();
                    setUserData(cachedData);
                    console.log('Loaded user data from cache:', cachedData);
                } else {
                    console.error('No data available in cache either.');
                }
            }
        } finally {
            setLoadingUserData(false);
        }
    };

    const deleteAccount = async () => {
        try {
            const response = await axios.delete('http://localhost:8080/AccountControler', {
                headers: {
                    'Authorization': `Bearer ${user.token}`,
                    'Content-Type': 'application/json',
                },
            });

            console.log(response.data);

            alert('Account deleted successfully.');
            logout();
            navigate('/');
        } catch (error) {
            console.error('Error while deleting account:', error);
            alert('Failed to delete account.');
        }
    };

    return (
        <div>
            <div>
                {loadingUserData ? (
                    <p>Loading user data...</p>
                ) : userData ? (
                    <div>
                        <h3>User Information</h3>
                        <p><strong>Username:</strong> {userData.userName}</p>
                        <p><strong>Email:</strong> {userData.email}</p>
                        <p><strong>Height:</strong> {userData.height} cm</p>
                        <p><strong>Weight:</strong> {userData.weight} kg</p>
                        <p><strong>Sex:</strong> {userData.sex === 0 ? 'Not Specified' : userData.sex === 1 ? 'Male' : 'Female'}</p>
                        <p><strong>Age:</strong> {userData.age}</p>
                        <p><strong>Lifestyle:</strong> {getLifestyleDescription(userData.lifestyle)}</p>
                    </div>
                ) : (
                    <p>No user data available.</p>
                )}
            </div>
            <div>
                <button onClick={deleteAccount} className="component-button">Delete account</button>
                <button onClick={logout} className="component-button">Log out</button>
            </div>
            <div>
                <Link to='/'>
                    <button className="component-button">Back to main page</button>
                </Link>
            </div>
        </div>
    );
};

const getLifestyleDescription = (lifestyle) => {
    switch (lifestyle) {
        case 0:
            return 'Sedentary';
        case 1:
            return 'Lightly Active';
        case 2:
            return 'Moderately Active';
        case 3:
            return 'Very Active';
        default:
            return 'Unknown';
    }
};

export default UserConfig;
