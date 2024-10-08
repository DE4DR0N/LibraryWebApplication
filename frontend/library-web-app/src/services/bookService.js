import axios from 'axios';
import Cookies from 'js-cookie';

const API_URL = 'http://localhost:5059/Books/';

const getAuthHeaders = () => {
    const user = JSON.parse(Cookies.get('user'));
    return {
        headers: {
            'Authorization': `Bearer ${user.token}`
        }
    };
};

const getAllBooks = (pageNumber, pageSize) => {
    return axios.get(API_URL, { 
        params: { pageNumber, pageSize },
        ...getAuthHeaders()
    });
};

const getBookById = (id) => {
    return axios.get(`${API_URL}${id}`, getAuthHeaders());
};

const addBook = (book) => {
    return axios.post(`${API_URL}addBook`, book, getAuthHeaders());
};

const updateBook = (id, book) => {
    return axios.put(`${API_URL}updateBook/${id}`, book, getAuthHeaders());
};

const deleteBook = (id) => {
    return axios.delete(`${API_URL}deleteBook/${id}`, getAuthHeaders());
};

const issueBookToUser = (bookId, userId, returnDate) => {
    return axios.post(`${API_URL}issue`, { bookId, userId, returnDate }, getAuthHeaders());
};

const returnBook = (bookId) => {
    return axios.post(`${API_URL}return`, { bookId }, getAuthHeaders());
};

export default { getAllBooks, getBookById, addBook, updateBook, deleteBook, issueBookToUser, returnBook };