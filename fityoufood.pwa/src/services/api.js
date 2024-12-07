import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:80/api',
});

export const loginUser = (credentials) => api.post('/auth/login', credentials);
export const registerUser = (data) => api.post('/auth/register', data);
export const fetchContent = () => api.get('/content');
