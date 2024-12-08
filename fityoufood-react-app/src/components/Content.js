import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import './Components.css';

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
                    <button className="component-button">Back to main page</button>
                </Link>
                <button className="component-button"onClick={saveFile}>Save exercise</button>
            </div>
        </div>
    );

    async function saveFile() {
        try {
          const data = exerciseData.map(exercise => {
            return `
            Exercise Name: ${exercise.name}
            Description: ${exercise.description}
            Target Muscle Group: ${exercise.target}
            Rating: ${exercise.rating}
            Official Exercise: ${exercise.isOfficial ? "Yes" : "No"}
            Visualisation URL: ${exercise.visualisationUrl}
            ------------------------
            `;
          }).join('\n');

          console.log(data)
    
          const fileHandle = await window.showSaveFilePicker({
            suggestedName: 'FitYouFoodExercises.txt',
            types: [
              {
                description: 'Text Files',
                accept: { 'text/plain': ['.txt'] },
              },
            ],
          });
    
          const writable = await fileHandle.createWritable();
          await writable.write(data);
          await writable.close();
    
          alert('File saved successfully!');
        } catch (error) {
          if (error.name === 'AbortError') {
            console.log('User canceled the save file picker.');
            alert('File save operation was canceled.');
          } else {
            console.error('Error saving the file:', error);
            alert('An error occurred while saving the file.');
          }
        }
      }
};

export default Content;
