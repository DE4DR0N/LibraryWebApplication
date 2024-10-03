import axios from 'axios';
import Cookies from 'js-cookie';

const API_URL = 'http://localhost:5059/Auth/';

const register = async (username, password) => {
    const response = await axios.post(`${API_URL}register`, { username, password })
    if (response.data.token) {
        Cookies.set('user', JSON.stringify(response.data.token), { expires: 7 });
    }
    return response.data;
}

const login = async (username, password) => {
    const response = await axios.post(`${API_URL}login`, { username, password });
    if (response.data.token) {
        Cookies.set('user', JSON.stringify(response.data.token), { expires: 7 });
    }
    return response.data;
};

const logout = () => {
    Cookies.remove('user');
};

const getCurrentUser = () => {
    const user = Cookies.get('user');
    return user ? JSON.parse(user) : null;
};

const refreshToken = async () => {
    const user = getCurrentUser();
    const response = await axios.post(`${API_URL}refresh`, {
        token: user.refreshToken
    });
    user.token = response.data.token;
    Cookies.set('user', JSON.stringify(user), { expires: 7 });
    return user.token;
};

const getAuthHeaders = () => {
    const user = getCurrentUser();
    return {
        headers: {
            'Authorization': `Bearer ${user.accessToken}`
        }
    };
};

export default {
    register,
    login,
    logout,
    getCurrentUser,
    refreshToken,
    getAuthHeaders
};
