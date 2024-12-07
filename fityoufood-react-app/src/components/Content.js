import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';

const Content = () => {
    const [exerciseData, setExerciseData] = useState([]);
    const [loading, setLoading] = useState(true); 

    useEffect(() => {
        fetchExerciseData();
    }, []); 

    const fetchExerciseData = async () => {
        try {
            const response = await axios.get('http://localhost:8080/Exercise');
            setExerciseData(response.data); 
            console.log(response.data);
            setLoading(false); 
        } catch (error) {
            console.error("Error fetching exercise data:", error);
            setLoading(false); 
        }
    };


    return (
        <div>
            <h2>Home Page</h2>
            <button onClick={() => alert('Button clicked!')}>Click Me</button>

            <div className="main-content">
                {/* Conditionally render loading state or exercise data */}
                <h3>Exercise List</h3>
                {loading ? (
                    <p>Loading exercise data...</p> 
                ) : (
                    <ul>
                        {exerciseData.length > 0 ? (
                            exerciseData.map((exercise) => (
                                <li key={exercise.id}>
                                    <h4>{exercise.name}</h4>
                                    <p><strong>Description:</strong> {exercise.description}</p>
                                    <p><strong>Target Muscle Group:</strong> {exercise.target}</p>
                                    <p><strong>Rating:</strong> {exercise.rating}</p>
                                    <p><strong>Official Exercise:</strong> {exercise.isOfficial ? "Yes" : "No"}</p>
                                    <p><strong>Visualisation URL:</strong> 
                                        <a href={exercise.visualisationUrl} target="_blank" rel="noopener noreferrer">View Visualisation</a>
                                    </p>
                                </li>
                            ))
                        ) : (
                            <p>No exercises available.</p>
                        )}
                    </ul>
                )}
            </div>

            <div>
                <Link to='/'>
                    <button>Back to main page</button>
                </Link>
            </div>
        </div>
    );
};

export default Content;
