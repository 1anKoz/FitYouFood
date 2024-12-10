import React, { useState, useEffect, useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import './Components.css';
import { AuthContext } from '../context/AuthContext';
import  AddIngredient  from './Content/AddIngredient'
import DeleteIngredient from './Content/DeleteIngredient'
import ModifyIngredient from './Content/ModifyIngredient'

const AdminPanel = () => {
    const { user, logout } = useContext(AuthContext);

    return (
        <div>
            <div>
                <h2>Hello admin user {user}!</h2>
                <p>You can add, modify or delete ingredients, exercises and meals.</p>
                <div className="ingredient-menu">
                    <Link to="/settings/config/ingredients/add">
                        <button className="component-button" onClick={addIngredient}>Add</button>
                    </Link>
                    <Link to="/settings/config/ingredients/delete">
                        <button className="component-button" onClick={deleteIngredient}>Delete</button>
                    </Link>
                    <Link to="/settings/config/ingredients/modify">
                        <button className="component-button" onClick={modifyIngredient}>Modify</button>
                    </Link>
                </div>
                <div className="meal-menu">

                </div>
                <div className="exercise-menu">

                </div>
            </div>
            <div>
                <Link to='/'>
                    <button className="component-button">Back to main page</button>
                </Link>
            </div>
            <Routes>
                  <Route path="/settings/config/ingredients/add" element={<AddIngredient />} />
                  <Route path="/settings/config/ingredients/delete" element={<DeleteIngredient />} />
                  <Route path="/settings/config/ingredients/modify" element={<ModifyIngredient />} />
            </Routes>
        </div>
    );
};

export default AdminPanel;