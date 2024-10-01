import axios from 'axios';

const API_URL = '/api/Auth/';

const register = (username, password, confirmPassword) => {
    return axios.post(`${API_URL}register`, { username, password, confirmPassword });
};

const login = async (username, password) => {
    const response = await axios.post(`${API_URL}login`, { username, password });
    if (response.data.token) {
        localStorage.setItem('user', JSON.stringify(response.data));
    }
    return response.data;
};

const refreshToken = (token, refreshToken) => {
    return axios.post(`${API_URL}refreshToken`, { token, refreshToken });
};

const getCurrentUser = () => {
    const user = JSON.parse(localStorage.getItem('user'));
    return user ? { username: user.username, userId: user.userId } : null;
};

const logout = () => {
    localStorage.removeItem('user');
};

export default { register, login, refreshToken, getCurrentUser, logout };
