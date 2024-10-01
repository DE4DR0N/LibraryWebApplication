import axios from 'axios';

const API_URL = '/api/Authors/';

const getAuthHeaders = () => {
    const user = JSON.parse(localStorage.getItem('user'));
    return {
        headers: {
            'Authorization': `Bearer ${user.token}`
        }
    };
};

const getAllAuthors = () => {
    return axios.get(API_URL, getAuthHeaders());
};

const getAuthorById = (id) => {
    return axios.get(`${API_URL}${id}`, getAuthHeaders());
};

const addAuthor = (author) => {
    return axios.post(`${API_URL}addAuthor`, author, getAuthHeaders());
};

const updateAuthor = (id, author) => {
    return axios.put(`${API_URL}updateAuthor/${id}`, author, getAuthHeaders());
};

const deleteAuthor = (id) => {
    return axios.delete(`${API_URL}deleteAuthor/${id}`, getAuthHeaders());
};

export default { getAllAuthors, getAuthorById, addAuthor, updateAuthor, deleteAuthor };