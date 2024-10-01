import axios from 'axios';

const API_URL = '/api/User/';

const getAuthHeaders = () => {
    const user = JSON.parse(localStorage.getItem('user'));
    return {
        headers: {
            'Authorization': `Bearer ${user.token}`
        }
    };
};

const getUserBorrowedBooks = (userId) => {
    return axios.get(`${API_URL}${userId}/books`, getAuthHeaders());
};

const getUserByUsername = (username) => {
    return axios.get(`${API_URL}${username}`, getAuthHeaders())
}

export default { getUserBorrowedBooks, getUserByUsername };
