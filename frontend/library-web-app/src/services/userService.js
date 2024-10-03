import axios from 'axios';
import Cookies from 'js-cookie';

const API_URL = 'http://localhost:5059/User/';

const getAuthHeaders = () => {
    const user = JSON.parse(Cookies.get('user'));
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
