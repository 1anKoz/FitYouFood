import React, { useState, useContext } from 'react';
import axios from 'axios';
import { AuthContext } from './context/AuthContext';
import './Components.css';

const DeleteIngredient = () => {
    const { user } = useContext(AuthContext);
    const [ingredientId, setIngredientId] = useState('');
    const [message, setMessage] = useState('');

    const handleDelete = async () => {
        if (!ingredientId) {
            alert('Please enter a valid ingredient ID.');
            return;
        }

        try {
            const response = await axios.delete(`http://localhost:8080/Ingredient/${ingredientId}`, {
                headers: {
                    'Authorization': `Bearer ${user.token}`,
                    'Content-Type': 'application/json',
                },
            });

            console.log(response.data);
            setMessage(`Ingredient with ID ${ingredientId} deleted successfully!`);
        } catch (error) {
            console.error('Error deleting ingredient:', error);
            setMessage('Failed to delete ingredient.');
        }
    };

    return (
        <div className="form-container">
            <h3>Delete Ingredient</h3>
            <input
                type="text"
                placeholder="Enter Ingredient ID"
                value={ingredientId}
                onChange={(e) => setIngredientId(e.target.value)}
            />
            <button onClick={handleDelete} className="component-button">Delete Ingredient</button>
            {message && <p>{message}</p>}
        </div>
    );
};

export default DeleteIngredient;