import React, { useState, useContext } from 'react';
import axios from 'axios';
import { AuthContext } from './context/AuthContext';
import './Components.css';

const ModifyIngredient = () => {
    const { user } = useContext(AuthContext);
    const [ingredient, setIngredient] = useState({
        id: 0,
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

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await axios.put(
                `http://localhost:8080/Ingredient/${ingredient.id}`,
                {
                    id: Number(ingredient.id),
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

            console.log(response.data);
            setMessage(`Ingredient "${response.data.name}" updated successfully!`);
        } catch (error) {
            console.error('Error modifying ingredient:', error);
            setMessage('Failed to update ingredient.');
        }
    };

    return (
        <div className="form-container">
            <h3>Modify Ingredient</h3>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="id">ID:</label>
                    <input
                        type="number"
                        id="id"
                        name="id"
                        value={ingredient.id}
                        onChange={handleChange}
                        required
                    />
                </div>
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
                <button type="submit" className="component-button">Modify Ingredient</button>
            </form>
            {message && <p>{message}</p>}
        </div>
    );
};

export default ModifyIngredient;