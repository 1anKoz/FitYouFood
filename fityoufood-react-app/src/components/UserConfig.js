import React, { useState, useEffect, useContext } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import './Components.css';
import { AuthContext } from './context/AuthContext';

const UserConfig = () => {
    const [loadingUserData, setLoadingUserData] = useState(true); 
    const { user, login, logout } = useContext(AuthContext);

    useEffect(() => {
        const fetchData = async () => {
          await fetchUserData();
        };
    
        fetchData();
    });

    const fetchUserData = async () => {
        try {
            const response = await axios.get('http://localhost:8080/Exercise');
            setExerciseData(response.data); 
            console.log(response.data);
            setLoadingExercise(false); 
        } catch (error) {
            console.error("Error fetching exercise data:", error);
            setLoadingExercise(false); 
        }
    };

    return (
        <div>
            <div>
                <Link to='/'>
                    <button className="component-button">Back to main page</button>
                </Link>
            </div>
        </div>
    );
};

export default UserConfig;
