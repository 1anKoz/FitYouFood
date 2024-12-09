import React, { useState, useContext } from 'react';
import axios from 'axios';
import { AuthContext } from './context/AuthContext';
import './Components.css';

const AddIngredient = () => {
    const { user } = useContext(AuthContext);
    const [ingredient, setIngredient] = useState({
        name: '',
        protein: 0,
        fat: 0,
        carbs: 0,
        isOfficial: true,
    });
    const [message, setMessage] = useState('');

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setIngredient((prev) => ({
            ...prev,
            [name]: type === 'checkbox' ? checked : value,
        }));
    };

    const addIngredient = async () => {
        try {
            const response = await axios.post(
                'http://localhost:8080/Ingredient',
                {
                    id: 0,
                    name: ingredient.name,
                    protein: Number(ingredient.protein),
                    fat: Number(ingredient.fat),
                    carbs: Number(ingredient.carbs),
                    isOfficial: ingredient.isOfficial,
                },
                {
                    headers: {
                        'Authorization': `Bearer ${user.token}`,
                        'Content-Type': 'application/json',
                    },
                }
            );

            setMessage(`Ingredient "${response.data.name}" created successfully!`);
            setIngredient({ name: '', protein: 0, fat: 0, carbs: 0, isOfficial: true }); // Reset the form
        } catch (error) {
            console.error('Error during ingredient creation process:', error);
            setMessage('Failed to create ingredient.');
        }
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        addIngredient();
    };

    return (
        <div className="form-container">
            <h3>Add New Ingredient</h3>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="name">Name:</label>
                    <input
                        type="text"
                        id="name"
                        name="name"
                        value={ingredient.name}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="protein">Protein:</label>
                    <input
                        type="number"
                        id="protein"
                        name="protein"
                        value={ingredient.protein}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="fat">Fat:</label>
                    <input
                        type="number"
                        id="fat"
                        name="fat"
                        value={ingredient.fat}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="carbs">Carbs:</label>
                    <input
                        type="number"
                        id="carbs"
                        name="carbs"
                        value={ingredient.carbs}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="isOfficial">Is Official:</label>
                    <input
                        type="checkbox"
                        id="isOfficial"
                        name="isOfficial"
                        checked={ingredient.isOfficial}
                        onChange={handleChange}
                    />
                </div>
                <button type="submit" className="component-button">Add Ingredient</button>
            </form>
            {message && <p>{message}</p>}
        </div>
    );
};

export default AddIngredient;